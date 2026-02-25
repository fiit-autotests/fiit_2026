using Kontur.BigLibrary.Tests.UI.PW.PageObjects.Controls.Base;
using Kontur.BigLibrary.Tests.UI.PW.PageObjects.Factories;
using Microsoft.Playwright;

namespace Kontur.BigLibrary.Tests.UI.PW.PageObjects.Controls;

public class ValidationInput : ControlBase
{
    // public ValidationInput(ILocator locator) : base(locator)
    public ValidationInput(ILocator locator, IControlFactory controlFactory, IPageFactory pageFactory) : base(locator, controlFactory, pageFactory)
    {
    }

    // private Input Input => new Input(Locator.Locator("[data-tid='input']"));
    // public Label ValidationMessageLabel => new Label(Locator.Locator("[data-tid='validation-message']"));
    
    private Input Input => ControlFactory.Create<Input>(Locator.Locator("[data-tid='input']"));
    public Label ValidationMessageLabel => ControlFactory.Create<Label>(Locator.Locator("[data-tid='validation-message']"));
    
    public Task<string?> ValidationMessage => ValidationMessageLabel.GetTextAsync();

    public Task FillAsync(string text) => Input.FillAsync(text);
}