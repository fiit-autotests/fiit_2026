using Kontur.BigLibrary.Tests.UI.PW.PageObjects.Factories;
using Microsoft.Playwright;

namespace Kontur.BigLibrary.Tests.UI.PW.PageObjects.Controls.Base;

public class DropdownSelect : ControlBase
{
    // public DropdownSelect(ILocator locator) : base(locator)
    public DropdownSelect(ILocator locator, IControlFactory controlFactory, IPageFactory pageFactory) : base(locator, controlFactory, pageFactory)
    {
    }
    
    public ListControl<Label> Options => 
        new ListControl<Label>(
            Locator.Locator("option"),
            // locator => new Label(locator)
            locator => ControlFactory.Create<Label>(locator)
            );

    public Task<string> GetSelectedTextAsync()
    {
        return Locator.InputValueAsync();
    }

    public Task<int> OptionsCount => Options.CountAsync();

    public async Task SelectByText(string text, bool partialMatch = false)
    {
        await Locator.SelectOptionAsync(text);
    }
}