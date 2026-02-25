using Kontur.BigLibrary.Tests.UI.PW.PageObjects.Controls.Base;
using Microsoft.Playwright;

namespace Kontur.BigLibrary.Tests.UI.PW.PageObjects.Factories;

public class SimpleControlFactory(IDependenciesFactory dependenciesFactory) 
    : IControlFactory  
{  
    public TControl Create<TControl>(ILocator locator) where TControl : ControlBase  
    {  
        var dependency = dependenciesFactory.CreateDependency(typeof(TControl));  
        return (TControl)Activator.CreateInstance(
            typeof(TControl), 
            new []{locator}.Concat(dependency).ToArray()
        )!;  
    }

    public ListControl<TControl> CreateList<TControl>(ILocator locator) where TControl : ControlBase
    {
        return new ListControl<TControl>(
            locator, locator => Create<TControl>(locator));
    }
    
    public TModal CreateModal<TModal>(IPage page) where TModal : ModalBase<TModal>, IModal 
    {  
        var dependency = dependenciesFactory.CreateDependency(typeof(TModal));  
        return (TModal)Activator.CreateInstance(
            typeof(TModal), 
            new []{page}.Concat(dependency).ToArray()
        )!;  
    }
}