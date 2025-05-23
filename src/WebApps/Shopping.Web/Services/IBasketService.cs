using System.Net;

namespace Shopping.Web.Services
{
    public interface IBasketService
    {
        [Get("/basket-service/basket/{userName}")]
        Task<GetBasketResponse> GetBasket(string userName);

        [Post("/basket-service/basket")]
        Task<StoreBasketResponse> StoreBasket(StoreBasketRequest request);

        [Delete("/basket-service/basket/{userName}")]
        Task<GetBasketResponse> DeleteBasket(string userName);

        [Post("/basket-service/basket/checkout")]
        Task<CheckoutBasketResponse> CheckoutBasket(CheckoutBasketRequest request);

        public async Task<ShoppingCartModel> LoadUserBasket()
        {
            var userName = "aaa";
            ShoppingCartModel basket;

            try
            {
                var getBasketResponse = await GetBasket(userName);
                basket = getBasketResponse.Cart;
            }
            catch (ApiException apiException) when (apiException.StatusCode == HttpStatusCode.NotFound)
            {
                basket = new ShoppingCartModel
                {
                    UserName = userName,
                    Items = new List<ShoppingCartItemModel>()
                };
            }

            return basket;
        }
    }
}