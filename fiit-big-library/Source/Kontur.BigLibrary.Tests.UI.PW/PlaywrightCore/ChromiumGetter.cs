using Kontur.BigLibrary.Tests.UI.PW.Helpers;
using Microsoft.Playwright;

namespace Kontur.BigLibrary.Tests.UI.PW.PlaywrightCore;

public class ChromiumGetter(IPlaywrightGetter playwrightGetter) 
    : IBrowserGetter  
{  
    /// <summary>  
    /// Лениво инициализируемый контекст браузера.    
    /// Создаётся при первом обращении.    
    /// </summary>    
    private readonly Lazy<Task<IBrowser>> _browser 
        = new(() => CreateBrowserAsync(playwrightGetter));  
  
    public Task<IBrowser> GetAsync()  
        => _browser.Value;  
    
    private static async Task<IBrowser> CreateBrowserAsync(IPlaywrightGetter getter)     {  
        var pw = await getter.GetAsync();  
        return await pw.Chromium.LaunchAsync(
            new BrowserTypeLaunchOptions
            {
                Headless = TestEnvironment.Headless
            });  
    }  
}