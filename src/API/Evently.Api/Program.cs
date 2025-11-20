using Evently.Api.Extensions;
using Evently.Api.Middlewares;
using Evently.Common.Application;
using Evently.Common.Infrastructure;
using Evently.Modules.Events.Infrastructure;
using Evently.Modules.Ticketing.Infrastructure;
using Evently.Modules.Users.Infrastructure;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(( context, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration);
});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();


builder.Services.AddControllers();
builder.Services.AddOpenApi();


string? DatabaseConnectionString = builder.Configuration.GetConnectionString("Database");
string? redisConnectionString = builder.Configuration.GetConnectionString("RedisCaching");

builder.Services.AddApplication([
    Evently.Modules.Events.Application.AssemblyReference.Assembly,
    Evently.Modules.Users.Application.AssemblyReference.Assembly,
    Evently.Modules.Ticketing.Application.AssemblyReference.Assembly
]);

builder.Services.AddInfrastructure(
    [TicketingModule.ConfigureConsumers] ,
    builder.Configuration.GetConnectionString("RedisCaching")!);

builder.Configuration.AddModuleConfigration(["events" , "users", "tickting"]);

builder.Services.AddHealthChecks()
    .AddSqlServer(DatabaseConnectionString!)
    .AddRedis(redisConnectionString!);


builder.Services.AddEventsModule(builder.Configuration);
builder.Services.AddUsersModule(builder.Configuration);
builder.Services.AddTicketingModule(builder.Configuration);




var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    await app.ApplyMigrationsAsync();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health", new HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.UseSerilogRequestLogging();
app.UseExceptionHandler();

await app.RunAsync();
