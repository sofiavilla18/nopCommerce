using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Nop.Playwright.Tests.Tests
{
    [TestFixture]
    public class CP_A01_RegistroTests : PageTest
    {
        private const string BaseUrl = "https://localhost:45552";

        [Test]
        public async Task CP_A01_RegistroClienteConDatosValidos()
        {
            string correo = $"prueba.qa.{Guid.NewGuid():N}@gmail.com";
            string password = "PruebaQA2026!";

            await Page.GotoAsync($"{BaseUrl}/register");
            await Expect(Page).ToHaveURLAsync(new Regex(".*register*."));
            await Page.Locator("#gender-female").CheckAsync(); 
            await Page.Locator("#FirstName").FillAsync("Carolina");
            await Page.Locator("#LastName").FillAsync("PruebaQA");
            await Page.Locator("#Email").FillAsync(correo);
            await Page.Locator("#NewsLetterSubscriptions_0__IsActive").CheckAsync();
            await Page.Locator("#Password").FillAsync(password);
            await Page.Locator("#ConfirmPassword").FillAsync(password);
            await Page.Locator("#register-button").ClickAsync();
            await Expect(Page.Locator(".result"))
                .ToContainTextAsync("Su registro se completó");

            TestContext.Out.WriteLine("CP-A01: Registro completado correctamente.");
            TestContext.Out.WriteLine($"Correo utilizado: {correo}");
        }
    }
}
