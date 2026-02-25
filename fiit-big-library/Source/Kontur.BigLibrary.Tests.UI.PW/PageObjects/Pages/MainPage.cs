using Kontur.BigLibrary.Tests.UI.PW.PageObjects.Controls;
using Kontur.BigLibrary.Tests.UI.PW.PageObjects.Controls.Base;
using Kontur.BigLibrary.Tests.UI.PW.PageObjects.Factories;
using Microsoft.Playwright;

namespace Kontur.BigLibrary.Tests.UI.PW.PageObjects.Pages;

public class MainPage : LibraryBasePage
{
    // public MainPage(IPage page) : base(page)
    public MainPage(IPage page, IControlFactory controlFactory) : base(page, controlFactory)
    {
    }
    
    public override string Url => Urls.Main;
    public override string TitleText => "Список книг";
    
    public Button DownloadXml => ControlFactory.Create<Button>(Page.Locator("[data-tid='xml-download']"));
    public BookList BookList => ControlFactory.Create<BookList>(Page.Locator("[data-tid='book-list']"));
    public Button CurrentUserMenu => ControlFactory.Create<Button>(Page.Locator("[data-tid='current_user_menu']"));
    public Button LogOutButton => ControlFactory.Create<Button>(Page.Locator("[data-tid='LogOut']"));
    public Input SearchInput => ControlFactory.Create<Input>(Page.Locator("[placeholder='найти по названию, автору или рубрике']"));
    public Button AddBookButton => ControlFactory.Create<Button>(Page.Locator("[data-tid='book-add']"));
    public Toggle FreeOnlyFilter => ControlFactory.Create<Toggle>(Page.Locator("[data-tid='FreeOnlyToggle']"));
    public Button ChangeView => ControlFactory.Create<Button>(Page.Locator("[data-tid='book-list-change-view']"));
    //public AddBookModal AddBookLightbox =>ControlFactory.Create<AddBookModal>(Page.Locator("[id='create-book-lightbox']"));
    public BooksTable BooksTable => ControlFactory.Create<BooksTable>(Page.Locator("[data-tid='TableRows']"));
}