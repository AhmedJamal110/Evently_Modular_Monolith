using Evently.Api.Extensions;
using Evently.Common.Application;
using Evently.Common.Infrastructure;
using Evently.Modules.Events.Infrastructure;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(( context, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration);
});


builder.Services.AddControllers();
builder.Services.AddOpenApi();


builder.Services.AddApplication([Evently.Modules.Events.Application.AssemblyReference.Assembly]);
builder.Services.AddInfrastructure();

builder.Configuration.AddModuleConfigration(["events"]);

builder.Services.AddEventsModule(builder.Configuration);




var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    await app.ApplyMigrationsAsync();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseSerilogRequestLogging();

await app.RunAsync();
