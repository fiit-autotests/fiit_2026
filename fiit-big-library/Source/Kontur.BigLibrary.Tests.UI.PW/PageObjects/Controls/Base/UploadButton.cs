using Kontur.BigLibrary.Tests.UI.PW.PageObjects.Factories;
using Microsoft.Playwright;

namespace Kontur.BigLibrary.Tests.UI.PW.PageObjects.Controls.Base;

public class UploadButton : Input
{
    // public UploadButton(ILocator locator) : base(locator)
    public UploadButton(ILocator locator, IControlFactory controlFactory, IPageFactory pageFactory) : base(locator, controlFactory, pageFactory)
    {
    }

    public async Task SetInputFilesAsync(string filePath)
    {
        await Locator.SetInputFilesAsync(filePath);
    }
}