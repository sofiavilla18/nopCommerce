using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Nop.Playwright.Tests.Tests
{
    [TestFixture]
    public class CP_A02_LoginTests : PageTest
    {
        private const string BaseUrl = "https://localhost:45552";

        [Test]
        public async Task CP_A02_InicioSesionConDatosValidos()
        {
            string correo = "prueba2.qa.5e8d4a7f3ced4fcfba9468adf8803f8d@gmail.com";
            string password = "PruebaQA2026!";

            await Page.GotoAsync($"{BaseUrl}/login");

            await Expect(Page).ToHaveURLAsync(new Regex(".*login.*"));

            await Page.Locator("#Email").FillAsync(correo);
            await Page.Locator("#Password").FillAsync(password);

            await Page.Locator("button.login-button").ClickAsync();

            await Expect(Page.Locator(".ico-account")).ToBeVisibleAsync();
            await Expect(Page.Locator(".ico-logout")).ToBeVisibleAsync();

            TestContext.Out.WriteLine("CP-A02: Inicio de sesión completado correctamente.");
            TestContext.Out.WriteLine($"Usuario autenticado: {correo}");
        }
    }
}
