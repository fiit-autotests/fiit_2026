using FluentAssertions;
using Kontur.BigLibrary.Tests.UI.PW.PageObjects.Controls.Base;
using Kontur.BigLibrary.Tests.UI.PW.PageObjects.Factories;
using Microsoft.Playwright;

namespace Kontur.BigLibrary.Tests.UI.PW.PageObjects.Pages;

public class LibraryBasePage : PageBase
{
    // public LibraryBasePage(IPage page) : base(page)
    public LibraryBasePage(IPage page, IControlFactory controlFactory) : base(page, controlFactory)
    {
    }
    
    public async Task WaitPageLoaded()
    {
       await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
       (await Title.GetTextAsync()).Should().Be(TitleText);
    }

    public override string Url { get; } = Urls.Main;
    public override string TitleText { get; }
    
    // public Label Title => new Label(Page.Locator("title"));
    public Label Title => ControlFactory.Create<Label>(Page.Locator("title"));
}