using Kontur.BigLibrary.Tests.UI.PW.PageObjects.Factories;
using Microsoft.Playwright;

namespace Kontur.BigLibrary.Tests.UI.PW.PageObjects.Controls.Base;

public class Toggle : ControlBase
{
    // public Toggle(ILocator locator): base(locator)
    public Toggle(ILocator locator, IControlFactory controlFactory, IPageFactory pageFactory) : base(locator, controlFactory, pageFactory)
    {
    }
}