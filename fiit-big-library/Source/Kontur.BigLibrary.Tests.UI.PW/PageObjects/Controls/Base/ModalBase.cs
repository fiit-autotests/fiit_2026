using Kontur.BigLibrary.Tests.UI.PW.PageObjects.Factories;
using Microsoft.Playwright;

namespace Kontur.BigLibrary.Tests.UI.PW.PageObjects.Controls.Base;

public abstract class ModalBase<TModal> : ControlBase where TModal : ModalBase<TModal>, IModal
{
    public ModalBase(IPage page, IControlFactory controlFactory, IPageFactory pageFactory) 
        : base(page.Locator(TModal.Selector), controlFactory, pageFactory)
    {
        
    }
}