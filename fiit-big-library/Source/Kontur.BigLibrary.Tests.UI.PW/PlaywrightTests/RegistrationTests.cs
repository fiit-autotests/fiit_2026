using Kontur.BigLibrary.Tests.Core.Helpers.StringGenerator;
using Kontur.BigLibrary.Tests.UI.PW.PageObjects.Pages;
using Kontur.BigLibrary.Tests.UI.PW.PlaywrightCore;

namespace Kontur.BigLibrary.Tests.UI.PW.PlaywrightTests;

public class RegistrationTests : TestBase
{
    [Category(Constants.Flaky)]
    [Test]
    public async Task Registration_Test()
    {
        var email = StringGenerator.GetEmail();
        var password = StringGenerator.GetValidPassword();
        
        var page = await Navigation.GoToPageAsync<RegisterPage>();
        await page.EmailInput.FillAsync(email);
        await page.PasswordInput.FillAsync(password);
        await page.PasswordConfirmationInput.FillAsync(password);
        var booksPage = await page.SubmitButton.ClickAndOpenPageAsync<MainPage>();
    
        await booksPage.BookList.WaitVisibleAsync();
        await booksPage.BookList.ExpectAnyBookAsync();
    }
}