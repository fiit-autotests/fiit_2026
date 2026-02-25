using Kontur.BigLibrary.Tests.UI.PW.PageObjects.Pages;
using Kontur.BigLibrary.Tests.UI.PW.PlaywrightTests;
using Microsoft.Playwright;

namespace Kontur.BigLibrary.Tests.UI.PW;

public class Lecture2ExampleTests : TestBase
{
    private Random random = new();
    
    [Test]
    public async Task UserRegistration_WithPageObjectSuccess()
    {
        var validEmail = random.Next() + "@xx.com";
        var validPassword = "12345678Qwe!";
        
        // var playwright = await Playwright.CreateAsync();
        // var launchOptions = new BrowserTypeLaunchOptions
        // {
        //     Headless = false
        // };
        //
        // var browser = await playwright.Chromium.LaunchAsync(launchOptions);
        // var context = await browser.NewContextAsync();
        // var page = await context.NewPageAsync();
        
        //var loginPage = new LoginPage(page);
        //await page.GotoAsync(loginPage.BaseUrl + loginPage.Url);
        var loginPage = await Navigation.GoToPageAsync<LoginPage>();
        
        //await loginPage.RegistrationLink.ClickAsync();
        //var registrationPage = new RegisterPage(page);
        var registrationPage = await loginPage.RegistrationLink.ClickAndOpenPageAsync<RegisterPage>();
        
        await registrationPage.EmailInput.FillAsync(validEmail);
        await registrationPage.PasswordInput.FillAsync(validPassword);
        await registrationPage.PasswordConfirmationInput.FillAsync(validPassword);
                
        //await registrationPage.SubmitButton.ClickAsync();
        //var mainPage = new MainPage(page);
        var mainPage = await registrationPage.SubmitButton.ClickAndOpenPageAsync<MainPage>();
        
        await mainPage.WaitPageLoaded();
    }
    
    [Test]
    public async Task UserRegistration_WithPageObject_WhenEmailInvalid()
    {
        var invalidEmail = "qwe";
        var validPassword = "12345678Qwe!";
    
        var expectedValidationMessage = "Пожалуйста, введите корректный адрес электронной почты.";
        
        // var playwright = await Playwright.CreateAsync();
        // var launchOptions = new BrowserTypeLaunchOptions
        // {
        //     Headless = false
        // };
        //
        // var browser = await playwright.Chromium.LaunchAsync(launchOptions);
        // var context = await browser.NewContextAsync();
        // var page = await context.NewPageAsync();
        
        //var loginPage = new LoginPage(page);
        //await loginPage.RegistrationLink.ClickAsync();
        //var registrationPage = new RegisterPage(page);
        
        var loginPage = await Navigation.GoToPageAsync<LoginPage>();
        
        var registrationPage = await loginPage.RegistrationLink.ClickAndOpenPageAsync<RegisterPage>();
        
        await registrationPage.EmailInput.FillAsync(invalidEmail);
        await registrationPage.PasswordInput.FillAsync(validPassword);
        await registrationPage.PasswordConfirmationInput.FillAsync(validPassword);
        await registrationPage.SubmitButton.ClickAsync();
        
        await registrationPage.EmailInput.ValidationMessageLabel.Expect().ToHaveTextAsync(expectedValidationMessage);
    }
}