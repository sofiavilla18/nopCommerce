using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Nop.Playwright.Tests.Tests
{
    [TestFixture]
    public class CP_A04_CarritoTests : PageTest
    {
        private const string BaseUrl = "https://localhost:45552";

        [Test]
        public async Task CP_A04_AgregarProductoAlCarrito()
        {
            string producto = "Apple MacBook Pro";

            await Page.GotoAsync(BaseUrl);
            await Page.Locator("#small-searchterms").FillAsync(producto);
            await Page.Locator("button.search-box-button").ClickAsync();

            await Page.Locator(".product-item .product-title a").First.ClickAsync();
            await Page.Locator("#add-to-cart-button-4").ClickAsync();

            await Expect(Page.Locator(".bar-notification.success")).ToBeVisibleAsync();

            await Page.Locator(".bar-notification .close").ClickAsync();
            await Page.Locator("#topcartlink a.ico-cart").ClickAsync();

            await Expect(Page).ToHaveURLAsync(new Regex("/cart"));
            await Expect(Page.Locator(".cart .product-name")).ToContainTextAsync(producto);
            await Expect(Page.Locator(".cart .qty-input").First).ToBeVisibleAsync();

            TestContext.Out.WriteLine("CP-A04: Producto agregado y verificado en el carrito correctamente.");
            TestContext.Out.WriteLine($"Producto en carrito: {producto}");
        }
    }
}
