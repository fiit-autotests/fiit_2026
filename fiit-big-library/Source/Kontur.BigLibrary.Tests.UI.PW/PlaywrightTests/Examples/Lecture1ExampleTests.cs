using FluentAssertions;
using Microsoft.Playwright;

namespace Kontur.BigLibrary.Tests.UI.PW.PlaywrightTests.Examples;

public class Lecture1ExampleTests
{
    private Random random = new();

    [Test]
    public async Task UserRegistration_Success()
    {
        var validEmail = random.Next() + "@xx.com";
        var validPassword = "12345678Qwe!";

        // 1. Открываем браузер.
        var playwright = await Playwright.CreateAsync();

        var launchOptions = new BrowserTypeLaunchOptions { Headless = false };
        var browser = await playwright.Chromium.LaunchAsync(launchOptions);

        var context = await browser.NewContextAsync();

        var page = await context.NewPageAsync();

        //Arrange
        // 2. Переходим на страницу Библиотеки, открывается страница входа.
        await page.GotoAsync("http://localhost:5000/");

        //Act
        // 3. Кликаем по кнопке “Регистрация”.
        var registrationLink = page.Locator("a[href='/register']");
        // registrationLink = page.GetByText("Регистрация");
        // registrationLink = page.GetByRole(AriaRole.Link, new () { Name = "Регистрация" });

        await registrationLink.ClickAsync();

        // 4. В поле “Электронная почта” вводим почту.
        var email = page.Locator("input.form-control[type=email]");
        await email.FillAsync(validEmail);

        // 5. В поле “Пароль” вводим пароль.
        var password = page.Locator("input#password[type=password]");
        await password.FillAsync(validPassword);

        // 6. В поле “Подтверждение пароля” вводим пароль еще раз.
        var passwordConfirmation = page.Locator("input#password-confirmation[type=password]");
        await passwordConfirmation.FillAsync(validPassword);

        // 7. Кликаем по кнопке “Зарегистрироваться”.
        var registrationButton = page.Locator("button[type=submit]");
        await registrationButton.ClickAsync();

        //Assert
        // 8. Проверяем: Мы зарегистрировались в приложении, открылась страница со списком книг.
        var mainTitle = page.Locator("a[data-tid='titleLink']");
        var mainTitleText = await mainTitle.TextContentAsync();

        mainTitleText.Should().Be("Библиотека"); // проверка через FluentAssertions
        await Assertions.Expect(mainTitle).ToHaveTextAsync("Библиотека"); // проверка через PW Assertions
        await Assertions.Expect(page).ToHaveURLAsync("http://localhost:5000/");

        // 9. Закрываем браузер - высвобождаем ресурсы.
        // Закрываем браузер. Можно не использовать, если используется using var browser
        await browser.CloseAsync();
        // Высвобождаем ресурсы PW. Можно не использовать, если используется using var playwright
        playwright.Dispose();
    }
}