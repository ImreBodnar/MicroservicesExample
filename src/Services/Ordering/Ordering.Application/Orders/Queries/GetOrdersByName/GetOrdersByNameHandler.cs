namespace Ordering.Application.Orders.Queries.GetOrdersByName
{
    public class GetOrdersByNameHandler(IApplicationsDbContext dbContext)
        : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
    {
        public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
        {
            var orders = await dbContext.Orders
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .Where(o => o.OrderName.Value.Contains(query.Name))
                .OrderBy(o => o.OrderName)
                .ToListAsync(cancellationToken);

            var result = ProjectToOrderDto(orders);

            return new GetOrdersByNameResult(result);
        }

        private List<OrderDto> ProjectToOrderDto(List<Order> orders)
        {
            var result = new List<OrderDto>();

            foreach (var order in orders)
            {
                var orderDto = new OrderDto(
                    order.Id.Value,
                    order.CustomerId.Value,
                    order.OrderName.Value,
                    new AddressDto(
                        order.ShippingAddress.FirstName,
                        order.ShippingAddress.LastName,
                        order.ShippingAddress.EmailAddress,
                        order.ShippingAddress.AddressLine,
                        order.ShippingAddress.Country,
                        order.ShippingAddress.State,
                        order.ShippingAddress.ZipCode),
                    new AddressDto(
                        order.BillingAddress.FirstName,
                        order.BillingAddress.LastName,
                        order.BillingAddress.EmailAddress,
                        order.BillingAddress.AddressLine,
                        order.BillingAddress.Country,
                        order.BillingAddress.State,
                        order.BillingAddress.ZipCode),
                    new PaymentDto(
                        order.Payment.CardNumber,
                        order.Payment.CardNumber,
                        order.Payment.Expiration,
                        order.Payment.CVV,
                        order.Payment.PaymentMethod),
                    order.Status,
                    order.OrderItems
                    .Select(x => new OrderItemDto(
                        x.OrderId.Value,
                        x.ProductId.Value,
                        x.Quantity,
                        x.Price))
                    .ToList());

                result.Add(orderDto);
            }

            return result;
        }
    }
}