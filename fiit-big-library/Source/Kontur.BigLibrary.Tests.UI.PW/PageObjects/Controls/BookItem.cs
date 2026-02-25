using Kontur.BigLibrary.Tests.UI.PW.PageObjects.Controls.Base;
using Kontur.BigLibrary.Tests.UI.PW.PageObjects.Factories;
using Microsoft.Playwright;

namespace Kontur.BigLibrary.Tests.UI.PW.PageObjects.Controls;

public class BookItem : ControlBase
{
    // public BookItem(ILocator locator) : base(locator)
    public BookItem(ILocator locator, IControlFactory controlFactory, IPageFactory pageFactory) : base(locator, controlFactory, pageFactory)
    {
    }
    
    public Label BookStatus => ControlFactory.Create<Label>(Locator.Locator("data-tid='state-label'"));
    public Link BookLink => ControlFactory.Create<Link>(Locator.Locator("[data-tid='book-link']"));
    public Label IsBusyLabel => ControlFactory.Create<Label>(Locator.Locator("[data-tid='StateLabelBusy']"));
    public Label IsFreeLabel => ControlFactory.Create<Label>(Locator.Locator("[data-tid='StateLabelFree']"));
    public Task<string?> Href => Locator.GetAttributeAsync("href");
	
    public Task<bool> IsBusy => IsBusyLabel.IsVisibleAsync();
    public Task<bool> IsFree => IsFreeLabel.IsVisibleAsync();
    
    
}