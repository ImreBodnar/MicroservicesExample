﻿namespace Ordering.Domain.Models
{
    public class Order : Aggregate<OrderId>
    {
        public readonly List<OrderItem> _orderItems = new();

        public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

        public CustomerId CustomerId { get; private set; } = default!;

        public OrderName OrderName { get; private set; } = default!;

        public Address ShippingAddress { get; private set; } = default!;

        public Address BillingAddress { get; private set; } = default!;

        public Payment Payment { get; private set; } = default!;

        public OrderStatus Status { get; private set; } = OrderStatus.Pending;

        public decimal TotalPrice
        {
            get => OrderItems.Sum(x => x.Price * x.Quantity);
            private set { }
        }

        public static Order Create(
            OrderId id,
            CustomerId customerId,
            OrderName orderName,
            Address shippingAddress,
            Address billingAddress,
            Payment payment)
        {
            var newOrder = new Order
            {
                Id = id,
                CustomerId = customerId,
                OrderName = orderName,
                ShippingAddress = shippingAddress,
                BillingAddress = billingAddress,
                Payment = payment,
                Status = OrderStatus.Pending
            };

            newOrder.AddDomainEvent(new OrderCreatedEvent(newOrder));

            return newOrder;
        }

        public void Update(
            OrderName orderName,
            Address shippingAddress,
            Address billingAddress,
            Payment payment,
            OrderStatus status)
        {
            OrderName = orderName;
            ShippingAddress = shippingAddress;
            BillingAddress = billingAddress;
            Payment = payment;
            Status = status;

            AddDomainEvent(new OrderUpdatedEvent(this));
        }

        public void Add(ProductId productId, int quantity, decimal price)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

            var newOrderItem = new OrderItem(Id, productId, quantity, price);
            _orderItems.Add(newOrderItem);
        }

        public void Remove(ProductId productId)
        {
            var orderItem = _orderItems.FirstOrDefault(x => x.ProductId == productId);
            if (orderItem is not null)
                _orderItems.Remove(orderItem);
        }
    }
}