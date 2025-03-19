namespace Shopping.Web.Pages;

public class OrderListModel(IOrderingService orderingService, ILogger<OrderListModel> logger) : PageModel
{
    public IEnumerable<OrderModel> Orders { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync()
    {
        // Assumption customerId is passed in from the UI authenticated user (Steven)
        var customerId = new Guid("b8804908-4118-4b04-8913-8dbdba7d70c2");

        var response = await orderingService.GetOrdersByCustomer(customerId);
        Orders = response.Orders;

        return Page();
    }
}
