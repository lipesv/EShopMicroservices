using BuildingBlocks.Messaging.Events;
using MassTransit;
using Ordering.Application.Orders.Commands.CreateOrder;
using Ordering.Domain.Enums;

namespace Ordering.Application.Orders.EventHandlers.Integration;

public class BasketCheckoutEventHandler(ISender sender, ILogger<BasketCheckoutEventHandler> logger) : IConsumer<BasketCheckoutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        // TODO: Create new order and start the order fullfilment process

        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        var command = MapToCreateOrderCommand(context.Message);
        await sender.Send(command);

        throw new NotImplementedException();
    }

    private CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent message)
    {
        // Create full order with incoming event data

        var addressDto = new AddressDto(message.FirstName,
                                        LastName: message.LastName,
                                        EmailAddress: message.EmailAddress,
                                        AddressLine: message.AddressLine,
                                        Country: message.Country,
                                        State: message.State,
                                        ZipCode: message.ZipCode);

        var paymentDto = new PaymentDto(message.CardName,
                                        message.CardNumber,
                                        message.Expiration,
                                        message.CVV,
                                        message.PaymentMethod);

        var orderId = Guid.NewGuid();

        var orderDto = new OrderDto(Id: orderId,
                                    CustomerId: message.CustomerId,
                                    OrderName: message.UserName,
                                    ShippingAddress: addressDto,
                                    BillingAddress: addressDto,
                                    Payment: paymentDto,
                                    Status: OrderStatus.Pending,
                                    OrderItems:
                                    [
                                        new OrderItemDto(orderId, new Guid("ca13159c-4e44-43a6-8e1d-8446350effea"), 2, 500),
                                        new OrderItemDto(orderId, new Guid("380b65b9-5a21-4c04-abd9-be217453e961"), 1, 400)
                                    ]);

        return new CreateOrderCommand(orderDto);
    }
}