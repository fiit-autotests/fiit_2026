using Kontur.BigLibrary.Tests.UI.PW.PageObjects.Factories;
using Kontur.BigLibrary.Tests.UI.PW.PageObjects.Pages;
using Microsoft.Playwright;

namespace Kontur.BigLibrary.Tests.UI.PW.PlaywrightCore;

public class Navigation(IPlaywrightPageGetter pageGetter, IPageFactory pageObjectsFactory)
{
    private readonly Lazy<Task<IPage>> _page = new(pageGetter.GetAsync);

    public async Task<TPage> GoToPageAsync<TPage>(PageGotoOptions? options = null)
        where TPage : PageBase
    {
        var page = await _page.Value;
        var pageObject = pageObjectsFactory.Create<TPage>(page);

        await page.GotoAsync($"{pageObject.BaseUrl}/{pageObject.Url ?? string.Empty}", options);
        
        return pageObject;
    }

    public async Task<IPage> GoToUrlAsync(string url, PageGotoOptions? options = null)
    {
        var browserPage = await _page.Value;
        await browserPage.GotoAsync(url, options);
        return browserPage;
    }
}