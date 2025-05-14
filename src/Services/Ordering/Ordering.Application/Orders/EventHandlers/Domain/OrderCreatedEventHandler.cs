using MassTransit;

namespace Ordering.Application.Orders.EventHandlers.Domain
{
    public class OrderCreatedEventHandler
        (IPublishEndpoint publishEndpoint, ILogger<OrderCreatedEventHandler> logger)
        : INotificationHandler<OrderCreatedEvent>
    {
        public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Domain Event handled: {domainEvent.GetType().Name}");

            var integrationEvent = domainEvent.Order.ToOrderDto();

            await publishEndpoint.Publish(integrationEvent, cancellationToken);
        }
    }
}