using Kontur.BigLibrary.Tests.UI.PW.PageObjects.Controls;
using Kontur.BigLibrary.Tests.UI.PW.PageObjects.Controls.Base;
using Kontur.BigLibrary.Tests.UI.PW.PageObjects.Factories;
using Microsoft.Playwright;

namespace Kontur.BigLibrary.Tests.UI.PW.PageObjects.Pages;

public class RegisterPage : PageBase
{
    // public RegisterPage(IPage page) : base(page)
    public RegisterPage(IPage page, IControlFactory controlFactory) : base(page, controlFactory)
    {
    }

    public override string Url => Urls.Register;
    public override string TitleText => "Библиотека";

    public Button SubmitButton => ControlFactory.Create<Button>(Page.Locator("[type=submit]"));
    public ValidationInput EmailInput => ControlFactory.Create<ValidationInput>(Page.Locator("[data-tid=email-input]"));
    public ValidationInput PasswordInput => ControlFactory.Create<ValidationInput>(Page.Locator("[data-tid=password-first]"));
    public ValidationInput PasswordConfirmationInput => ControlFactory.Create<ValidationInput>(Page.Locator("[data-tid=password-confirmation]"));
    public Link LoginLink => ControlFactory.Create<Link>(Page.Locator("[href=/login]"));
    
    // public Button SubmitButton => new Button(Page.Locator("[type=submit]"));
    // public ValidationInput EmailInput => new ValidationInput(Page.Locator("[data-tid=email-input]"));
    // public ValidationInput PasswordInput => new ValidationInput(Page.Locator("[data-tid=password-first]"));
    // public ValidationInput PasswordConfirmationInput => new ValidationInput(Page.Locator("[data-tid=password-confirmation]"));
    // public Link LoginLink => new Link(Page.Locator("[href=/login]"));
    
    // public ILocator SubmitButton => Page.Locator("[type=submit]");
    // public ILocator EmailInput => Page.Locator("[type=email]");
    // public ILocator PasswordInput => Page.Locator("[id=password]");
    // public ILocator PasswordConfirmationInput => Page.Locator("[id=password-confirmation]");
    // public ILocator LoginLink => Page.Locator("[href=/login]");

    public async Task FillAllFields(string email, string password)
    {
        await EmailInput.FillAsync(email);
        await PasswordInput.FillAsync(password);
        await PasswordConfirmationInput.FillAsync(password);
    }
}