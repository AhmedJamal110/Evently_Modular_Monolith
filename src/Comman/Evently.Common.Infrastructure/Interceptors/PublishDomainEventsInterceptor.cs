using Evently.Common.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace Evently.Common.Infrastructure.Interceptors;
public sealed class PublishDomainEventsInterceptor(
    IServiceScopeFactory _serviceScopeFactory) : SaveChangesInterceptor
{
    public override async ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,
        CancellationToken cancellationToken = default)
    {

        if (eventData.Context is not null)
        {
            await PublishDomainEventsAsync(eventData.Context);
        }

        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }


   private async Task PublishDomainEventsAsync(
       DbContext context)
    {
        List<IDomainEvent> domainEvents = context
           .ChangeTracker
           .Entries<BaseEntity>()
           .Select(entry => entry.Entity)
           .SelectMany(entity =>
           {
               IReadOnlyCollection<IDomainEvent> domainEvents = entity.DomainEvents;

               entity.ClearDomainEvents();


               return domainEvents;
           })
           .ToList();

        using IServiceScope serviceScope = _serviceScopeFactory.CreateScope();

        IPublisher publisher = serviceScope.ServiceProvider.GetRequiredService<IPublisher>();

        foreach (var item in domainEvents)
        {
            await publisher.Publish(item);
        }
    }

}
