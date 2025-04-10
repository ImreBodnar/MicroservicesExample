﻿namespace Basket.API.Basket.StoreBasket
{
    public class StoreBasketCommandHandler 
        : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            var cart = command.Cart;

            return new StoreBasketResult("aaa");
        }
    }

    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;

    public record StoreBasketResult(string UserName);

    public class StoreBasketCommandValidator 
        : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null.");
            RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required.");
        }
    }
}