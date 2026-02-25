using Kontur.BigLibrary.Tests.UI.PW.PageObjects.Controls.Base;
using Kontur.BigLibrary.Tests.UI.PW.PageObjects.Factories;
using Microsoft.Playwright;

namespace Kontur.BigLibrary.Tests.UI.PW.PageObjects.Controls;

public class TableRow : ControlBase
{
    // public TableRow(ILocator locator) : base(locator)
    public TableRow(ILocator locator, IControlFactory controlFactory, IPageFactory pageFactory) : base(locator, controlFactory, pageFactory)
    {
    }
    
    public Link Name => ControlFactory.Create<Link>(Locator.Locator("[data-tid='Name']"));
    public Label Author => ControlFactory.Create<Label>(Locator.Locator("[data-tid='Author']"));
    public Label Rubric => ControlFactory.Create<Label>(Locator.Locator("[data-tid='Rubric']"));
    public Label Status => ControlFactory.Create<Label>(Locator.Locator("[data-tid='State']"));
    public Label Price => ControlFactory.Create<Label>(Locator.Locator("[data-tid='Price']"));
}