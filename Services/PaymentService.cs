using Stripe;
using Stripe.Checkout;

namespace EcommerceAPI.Services;

public class PaymentService
{
    private readonly IConfiguration _config;

    public PaymentService(IConfiguration config)
    {
        _config = config;
        var key = _config["Stripe:SecretKey"];
        Console.WriteLine($"Stripe Key carregada: {key?.Substring(0, 10)}...");
        StripeConfiguration.ApiKey = _config["Stripe:SecretKey"];
    }

    public async Task<string> CreateCheckoutSession(List<(string nome, decimal preco, int quantidade)> items, string successUrl, string cancelUrl)
    {
        var lineItems = items.Select(item => new SessionLineItemOptions
        {
            PriceData = new SessionLineItemPriceDataOptions
            {
                Currency = "brl",
                UnitAmount = (long)(item.preco * 100),
                ProductData = new SessionLineItemPriceDataProductDataOptions
                {
                    Name = item.nome,
                },
            },
            Quantity = item.quantidade,
        }).ToList();

        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string> { "card" },
            LineItems = lineItems,
            Mode = "payment",
            SuccessUrl = successUrl,
            CancelUrl = cancelUrl,
        };

        var service = new SessionService();
        var session = await service.CreateAsync(options);
        return session.Url;
    }
}