using MassTransit;
using Microsoft.FeatureManagement;

namespace Ordering.Application.Orders.EventHandlers.Domain
{
    public class OrderCreatedEventHandler
        (IPublishEndpoint publishEndpoint, IFeatureManager featureManager, ILogger<OrderCreatedEventHandler> logger)
        : INotificationHandler<OrderCreatedEvent>
    {
        public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Domain Event handled: {domainEvent.GetType().Name}");

            if (await featureManager.IsEnabledAsync("OrderFullfiment"))
            {
                var integrationEvent = domainEvent.Order.ToOrderDto();

                await publishEndpoint.Publish(integrationEvent, cancellationToken);
            }
        }
    }
}