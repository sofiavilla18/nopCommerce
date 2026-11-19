using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Nop.Playwright.Tests.Tests
{
    [TestFixture]
    public class CP_A03_BusquedaTests : PageTest
    {
        private const string BaseUrl = "https://localhost:45552";

        [Test]
        public async Task CP_A03_BusquedaProductoExistente()
        {
            string producto = "Apple MacBook Pro";

            // 1. Abrir la tienda
            await Page.GotoAsync(BaseUrl);

            // 2. Introducir el término de búsqueda
            await Page.Locator("#small-searchterms").FillAsync(producto);

            // 3. Ejecutar la búsqueda
            await Page.Locator("button.search-box-button").ClickAsync();

            // 4. Verificar que cargue la página de búsqueda y aparezcan resultados
            await Expect(Page).ToHaveURLAsync(new Regex(".*search.*"));
            await Expect(Page.Locator(".search-results")).ToBeVisibleAsync();
            await Expect(Page.Locator(".product-item .product-title").First)
                .ToContainTextAsync(producto);

            TestContext.Out.WriteLine("CP-A03: Búsqueda de producto completada correctamente.");
            TestContext.Out.WriteLine($"Producto encontrado: {producto}");
        }
    }
}
