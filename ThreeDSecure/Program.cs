using System.Text;
using Stripe;
using Stripe.Checkout;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        StripeConfiguration.ApiKey = "sk_test_26PHem9AhJZvU623DfE1x4sd";

        try
        {
            var sessionOptions = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Товар",
                            },
                            UnitAmount = 1000,
                        },
                        Quantity = 1,
                    },
                },
                Mode = "payment",
                SuccessUrl = "https://example.com/success",
                CancelUrl = "https://example.com/cancel",
            };

            var sessionService = new SessionService();
            var session = sessionService.CreateAsync(sessionOptions).Result;

            Console.WriteLine(session.Id);

            Console.WriteLine($"URL для оплати: {session.Url}");
        }
        catch (StripeException e)
        {
            Console.WriteLine($"Помилка Stripe: {e.Message}");
        }
    }
}
