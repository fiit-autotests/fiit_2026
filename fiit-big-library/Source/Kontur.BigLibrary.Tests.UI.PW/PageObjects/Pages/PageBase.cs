using Kontur.BigLibrary.Tests.Core;
using Kontur.BigLibrary.Tests.UI.PW.PageObjects.Controls.Base;
using Kontur.BigLibrary.Tests.UI.PW.PageObjects.Factories;
using Microsoft.Playwright;

namespace Kontur.BigLibrary.Tests.UI.PW.PageObjects.Pages;

/// <summary>
/// Абстрактный базовый класс для всех PageObjects.
/// Содержит общую логику и структуру для работы со страницами приложения.
/// </summary>
/// <remarks>
/// Реализует базовый уровень паттерна Wrapper для страниц,
/// оборачивая объект IPage.
/// </remarks>
// public abstract class PageBase(IPage page)
public abstract class PageBase(IPage page, IControlFactory controlFactory)
{

    /// <summary>
    /// Объект страницы Playwright. Предоставляет доступ к навигации,
    /// взаимодействию с элементами и другим API браузера.
    /// </summary>
    public IPage Page { get; } = page;

    public IControlFactory ControlFactory { get; } = controlFactory;

    /// <summary>
    /// Абстрактное свойство, возвращающее URL страницы.
    /// Должно быть реализовано в конкретных классах страниц.
    /// </summary>
    public abstract string Url { get; }

    public abstract string TitleText { get; }

    public string BaseUrl => ServiceInfo.GetServiceUrl();

    public async Task RefreshAsync()
    {
        await page.ReloadAsync();
    }
}