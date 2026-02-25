using Kontur.BigLibrary.Tests.Core.Helpers.StringGenerator;
using Kontur.BigLibrary.Tests.UI.PW.PageObjects.Pages;

namespace Kontur.BigLibrary.Tests.UI.PW.PlaywrightTests;

public class SignInTests : TestBase
{
    [Test]
    public async Task SignInWithRegistredUser_Test()
    {
        var email = StringGenerator.GetEmail();
        var password = StringGenerator.GetValidPassword();
    
        await RegisterUser(email,password);
        //TestData.CreateUserAndGetToken(email, password);
    
        var loginPage = await Navigation.GoToPageAsync<LoginPage>();
        await loginPage.Email.FillAsync(email);
        await loginPage.Password.FillAsync(password);
        var booksPage = await loginPage.SignInButton.ClickAndOpenPageAsync<MainPage>();
    
        await booksPage.BookList.WaitVisibleAsync();
        await booksPage.BookList.ExpectAnyBookAsync();
    }
    
    public async Task RegisterUser(string? email, string? password)
    {
        var page = await Navigation.GoToPageAsync<RegisterPage>();
        await page.EmailInput.FillAsync(email);
        await page.PasswordInput.FillAsync(password);
        await page.PasswordConfirmationInput.FillAsync(password);
        var booksPage = await page.SubmitButton.ClickAndOpenPageAsync<MainPage>();
        await booksPage.CurrentUserMenu.ClickAsync();
        await booksPage.LogOutButton.WaitVisibleAsync();
        await booksPage.LogOutButton.ClickAsync();
    }
}