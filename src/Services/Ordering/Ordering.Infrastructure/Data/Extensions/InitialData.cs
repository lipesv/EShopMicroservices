namespace Ordering.Infrastructure.Data.Extensions;

internal class InitialData
{
    public static IEnumerable<Customer> Customers =>
        new List<Customer>
        {
            Customer.Create(CustomerId.Of(new Guid("b8804908-4118-4b04-8913-8dbdba7d70c2")), "Steven", "stevenajohnson@jourrapide.com"),
            Customer.Create(CustomerId.Of(new Guid("8c02d60a-c58f-41bb-9007-f76626ef18b9")), "Lena", "lenashuster@rhyta.com")
        };

    public static IEnumerable<Product> Products =>
        new List<Product>
        {
            Product.Create(ProductId.Of(new Guid("ca13159c-4e44-43a6-8e1d-8446350effea")), "IPhone X", 500),
            Product.Create(ProductId.Of(new Guid("380b65b9-5a21-4c04-abd9-be217453e961")), "Samsung 10", 400),
            Product.Create(ProductId.Of(new Guid("2c357e1e-8342-4f07-ad17-4a27b559d282")), "Huawei Plus", 650),
            Product.Create(ProductId.Of(new Guid("a48aa87b-75af-4d00-a8d8-be2a01a08070")), "Xiaomi Mi", 450)
        };

    public static IEnumerable<Order> OrdersWithItems
    {
        get
        {
            var address1 = Address.Of("Steven", "Johnson", "stevenajohnson@jourrapide.com", "1973 Rosebud Avenue", "United States", "West Memphis", "72301");
            var address2 = Address.Of("Lena", "Shuster", "lenashuster@rhyta.com", "Holstenwall 65", "Germany", "Dittersbach", "01833");

            var payment1 = Payment.Of("Steven Johnson", "4716318765787026", "11/2028", "859", 1);
            var payment2 = Payment.Of("Lena Shuster", "5316775579675989", "01/2029", "539", 2);

            var order1 = Order.Create(OrderId.Of(Guid.NewGuid()),
                                      CustomerId.Of(new Guid("b8804908-4118-4b04-8913-8dbdba7d70c2")),
                                      OrderName.Of("ORD_1"),
                                      shippingAddress: address1,
                                      billingAddress: address2,
                                      payment1);

            order1.Add(ProductId.Of(new Guid("ca13159c-4e44-43a6-8e1d-8446350effea")), 500, 2);
            order1.Add(ProductId.Of(new Guid("380b65b9-5a21-4c04-abd9-be217453e961")), 400, 1);

            var order2 = Order.Create(OrderId.Of(Guid.NewGuid()),
                                      CustomerId.Of(new Guid("8c02d60a-c58f-41bb-9007-f76626ef18b9")),
                                      OrderName.Of("ORD_2"),
                                      shippingAddress: address2,
                                      billingAddress: address2,
                                      payment2);

            order2.Add(ProductId.Of(new Guid("2c357e1e-8342-4f07-ad17-4a27b559d282")), 650, 1);
            order2.Add(ProductId.Of(new Guid("a48aa87b-75af-4d00-a8d8-be2a01a08070")), 450, 2);

            return new List<Order> { order1, order2 };
        }
    }
}
