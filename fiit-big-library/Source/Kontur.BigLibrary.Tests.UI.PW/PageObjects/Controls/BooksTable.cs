using Kontur.BigLibrary.Tests.UI.PW.PageObjects.Controls.Base;
using Kontur.BigLibrary.Tests.UI.PW.PageObjects.Factories;
using Microsoft.Playwright;

namespace Kontur.BigLibrary.Tests.UI.PW.PageObjects.Controls;

public class BooksTable : ControlBase
{
    // public BooksTable(ILocator locator) : base(locator)
    public BooksTable(ILocator locator, IControlFactory controlFactory, IPageFactory pageFactory) : base(locator, controlFactory, pageFactory)
    {
    }

    public ListControl<TableRow> Books => 
        ControlFactory.CreateList<TableRow>(Locator.Locator("[data-tid='TableRow']"));
}