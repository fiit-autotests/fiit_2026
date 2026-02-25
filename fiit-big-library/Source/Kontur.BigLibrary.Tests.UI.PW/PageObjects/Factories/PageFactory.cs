using Kontur.BigLibrary.Tests.UI.PW.PageObjects.Pages;
using Microsoft.Playwright;

namespace Kontur.BigLibrary.Tests.UI.PW.PageObjects.Factories;

public class PageFactory(IDependenciesFactory dependenciesFactory) : IPageFactory  
{  
    public TPage Create<TPage>(IPage page) where TPage : PageBase  
    {  
        var dependency = dependenciesFactory.CreateDependency(typeof(TPage));  
        return (TPage)Activator.CreateInstance(
            typeof(TPage), 
            new []{page}.Concat(dependency).ToArray()
        )!;  
    }  
}