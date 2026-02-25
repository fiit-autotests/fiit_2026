using Kontur.BigLibrary.Tests.UI.PW.PageObjects.Controls.Base;
using Kontur.BigLibrary.Tests.UI.PW.PageObjects.Factories;
using Microsoft.Playwright;

namespace Kontur.BigLibrary.Tests.UI.PW.PageObjects.Pages;

public class BookPage : LibraryBasePage
{
    // public BookPage(IPage page) : base(page)
    public BookPage(IPage page, IControlFactory controlFactory) : base(page, controlFactory)
    {
    }
    
    public override string Url { get; } 
    public override string TitleText { get; }

    public Link AllBooks => ControlFactory.Create<Link>(Page.Locator("[data-tid='AllBooks']"));
    public Link HowToTakeLink => ControlFactory.Create<Link>(Page.Locator("[data-tid='HowToTakeLink']"));
    public Button CheckoutBook => ControlFactory.Create<Button>(Page.Locator("[data-tid='CheckoutBook']"));
    public Button ReturnBook => ControlFactory.Create<Button>(Page.Locator("[data-tid='ReturnBook']"));
    public Button Enqueue => ControlFactory.Create<Button>(Page.Locator("[data-tid='Enqueue']"));
    public Label BookName => ControlFactory.Create<Label>(Page.Locator("[data-tid='Name']"));
    public Label BookAuthor => ControlFactory.Create<Label>(Page.Locator("[data-tid='Author']"));
    public Label BookDescription => ControlFactory.Create<Label>(Page.Locator("[data-tid='Description']"));
    public Label FreeState => ControlFactory.Create<Label>(Page.Locator("[data-tid='StateLabelFree']"));
    public Label BusyState => ControlFactory.Create<Label>(Page.Locator("[data-tid='StateLabelBusy']"));
    public Label MessageAlert => ControlFactory.Create<Label>(Page.Locator("[data-tid='MessageAlert']"));
}