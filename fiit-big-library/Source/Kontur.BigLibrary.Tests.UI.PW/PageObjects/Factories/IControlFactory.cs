using Kontur.BigLibrary.Tests.UI.PW.PageObjects.Controls.Base;
using Microsoft.Playwright;

namespace Kontur.BigLibrary.Tests.UI.PW.PageObjects.Factories;

/// <summary>
/// Фабрика для создания контролов на веб-страницах.
/// Предоставляет стандартизированный способ создания переиспользуемых UI-компонентов.
/// </summary>
/// <remarks>
/// Используется для инкапсуляции логики создания контролов (кнопок, полей ввода и т.д.).
/// Позволяет легко подменять реализацию контролов и упрощает поддержку тестов.
/// </remarks>
public interface IControlFactory
{
    /// <summary>Создает экземпляр указанного типа элемента</summary>
    /// <typeparam name="TControl">
    /// Тип создаваемого элемента, должен наследовать <see cref="ControlBase"/>.
    /// </typeparam>
    /// <param name="locator">
    /// Локатор Playwright, определяющий положение элемента на странице.
    /// </param>
    /// <returns>Инициализированный экземпляр контрола</returns>
    TControl Create<TControl>(ILocator locator) where TControl : ControlBase;
    
    ListControl<TControl> CreateList<TControl>(ILocator locator) where TControl : ControlBase;
    
    TModal CreateModal<TModal>(IPage page) where TModal : ModalBase<TModal>, IModal;
}