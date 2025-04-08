using Marten.Schema;

namespace Catalog.API.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();

            if (await session.Query<Product>().AnyAsync())
                return;

            session.Store<Product>(GetPreconfiguredProducts());
            await session.SaveChangesAsync();
        }

        private static IEnumerable<Product> GetPreconfiguredProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = new Guid(),
                    Name = "IPhone X",
                    Description = "This is a cool phone!",
                    ImageFile = "product1.png",
                    Price = 1120,
                    Category = new List<string>{ "mobile", "apple" }
                }
            };
        }
    }
}