

using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Basket.API.Basket.Checkout;

public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto) : ICommand<CheckoutBasketResult>;

public record CheckoutBasketResult(bool IsSuccess);

public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketCommandValidator()
    {
        RuleFor(x => x.BasketCheckoutDto).NotNull().WithMessage("BasketCheckoutDto can't be null");
        RuleFor(x => x.BasketCheckoutDto.UserName).NotEmpty().WithMessage("UserName is required");
    }
}


public class CheckoutBasketCommandHandler(IBasketRepository repository,
                                          IPublishEndpoint publishEndpoint) : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        // TODO:
        // 1. Get existing basket with Total Price
        // 2. Set totalprice on basketcheckout event message
        // 3. Send basket checkout event to rabbitmq using masstransit
        // 4. Delete the basket

        var basket = await repository.GetBasketAsync(command.BasketCheckoutDto.UserName, cancellationToken);

        if (basket == null)
            return new CheckoutBasketResult(false);        

        var eventMessage = command.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();
        eventMessage.TotalPrice = basket.TotalPrice;        

        await publishEndpoint.Publish(eventMessage, cancellationToken);        

        await repository.DeleteBasketAsync(command.BasketCheckoutDto.UserName, cancellationToken);

        return new CheckoutBasketResult(true);
    }
}
