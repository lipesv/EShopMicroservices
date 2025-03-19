namespace Shopping.Web.Pages;

public class CheckoutModel(IBasketService basketService, ILogger<CheckoutModel> logger) : PageModel
{
    [BindProperty]
    public BasketCheckoutModel Order { get; set; } = default!;

    public ShoppingCartModel Cart { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync()
    {
        Cart = await basketService.LoadUserBasket();

        return Page();
    }

    public async Task<IActionResult> OnPostCheckOutAsync()
    {
        logger.LogInformation("Checkout button clicked");

        Cart = await basketService.LoadUserBasket();

        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Assumption customerId is passed in from the UI authenticated user (Steven)
        Order.CustomerId = new Guid("b8804908-4118-4b04-8913-8dbdba7d70c2");
        Order.UserName = Cart.UserName;
        Order.TotalPrice = Cart.TotalPrice;

        await basketService.CheckoutBasket(new CheckoutBasketRequest(Order));

        return RedirectToPage("Confirmation", "OrderSubmitted");
    }
}
