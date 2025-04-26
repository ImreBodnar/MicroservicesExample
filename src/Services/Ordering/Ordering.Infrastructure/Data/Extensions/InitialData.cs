namespace Ordering.Infrastructure.Data.Extensions
{
    internal class InitialData
    {
        public static IEnumerable<Customer> Customers =>
            new List<Customer>
            {
                Customer.Create(CustomerId.Of(new Guid("601b6197-ce5b-4b36-9f77-828a75b4cdef")), "john", "john@gmail.com"),
                Customer.Create(CustomerId.Of(new Guid("fc28e7c3-bb2a-4c45-938a-3f81b6bd514a")), "james", "james@gmail.com")
            };

        public static IEnumerable<Product> Products =>
            new List<Product>
            {
                Product.Create(ProductId.Of(new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61")), "iPhone X", 199),
                Product.Create(ProductId.Of(new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914")), "Samsung Galaxy 10", 399),
                Product.Create(ProductId.Of(new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8")), "Huawei Plus", 299),
                Product.Create(ProductId.Of(new Guid("6ec1297b-ec0a-4aa1-be25-6726e3b51a27")), "Xiaomi Mi 9", 249)
            };

        public static IEnumerable<Order> OrdersWithItems
        {
            get
            {
                var address1 = Address.Of("john", "smith", "john.smith@gmail.com", "Sunset Boulvard 1", "USA", "California", "1001");
                var address2 = Address.Of("james", "smith", "james.smith@gmail.com", "Sunset Boulvard 1", "USA", "California", "1001");

                var payment1 = Payment.Of("income", "5555555555554444", "12/28", "123", 1);
                var payment2 = Payment.Of("salary", "8885555555554444", "06/30", "456", 2);

                var order1 = Order.Create(
                    OrderId.Of(Guid.NewGuid()),
                    CustomerId.Of(new Guid("601b6197-ce5b-4b36-9f77-828a75b4cdef")),
                    OrderName.Of("ord_1"),
                    address1,
                    address2,
                    payment1);
                order1.Add(ProductId.Of(new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61")), 2, 199);
                order1.Add(ProductId.Of(new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914")), 1, 399);

                var order2 = Order.Create(
                    OrderId.Of(Guid.NewGuid()),
                    CustomerId.Of(new Guid("fc28e7c3-bb2a-4c45-938a-3f81b6bd514a")),
                    OrderName.Of("ord_2"),
                    address1,
                    address2,
                    payment2);
                order2.Add(ProductId.Of(new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61")), 1, 199);
                order2.Add(ProductId.Of(new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8")), 2, 299);

                return new List<Order>
                {
                    order1,
                    order2
                };
            }
        }
    }
}