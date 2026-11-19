using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Nop.Playwright.Tests.Tests
{
    [TestFixture]
    public class CP_A05_EliminarCarritoTests : PageTest
    {
        private const string BaseUrl = "https://localhost:45552";

        [Test]
        public async Task CP_A05_EliminarProductoDelCarrito()
        {
            string producto = "Apple MacBook Pro";

            await Page.GotoAsync(BaseUrl);
            await Page.Locator("#small-searchterms").FillAsync(producto);
            await Page.Locator("button.search-box-button").ClickAsync();
            await Page.Locator(".product-item .product-title a").First.ClickAsync();
            await Page.Locator("#add-to-cart-button-4").ClickAsync();
            await Expect(Page.Locator(".bar-notification.success")).ToBeVisibleAsync();

            await Page.GotoAsync($"{BaseUrl}/cart");
            await Expect(Page).ToHaveURLAsync($"{BaseUrl}/cart");
            await Expect(Page.Locator(".cart .product-name")).ToContainTextAsync(producto);

            await Page.Locator("button.remove-btn").First.ClickAsync();

            await Expect(Page.Locator(".cart .product-name")).ToHaveCountAsync(0);
            await Expect(Page.Locator(".no-data")).ToBeVisibleAsync();

            TestContext.Out.WriteLine("CP-A05: Producto eliminado del carrito correctamente.");
            TestContext.Out.WriteLine($"Producto eliminado: {producto}");
        }
    }
}
