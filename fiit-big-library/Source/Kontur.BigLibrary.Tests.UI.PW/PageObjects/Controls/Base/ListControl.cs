using Microsoft.Playwright;

namespace Kontur.BigLibrary.Tests.UI.PW.PageObjects.Controls.Base;

public class ListControl<T> where T : ControlBase
{
    private readonly ILocator _itemsLocator;
    private readonly Func<ILocator, T> _factory;

    public ListControl(ILocator itemsLocator, Func<ILocator, T> factory)
    {
        _itemsLocator = itemsLocator;
        _factory = factory;
    }

    public Task<int> CountAsync()
    {
        return _itemsLocator.CountAsync();
    }

    public T this[int index] =>
        _factory(_itemsLocator.Nth(index));

    public async Task<List<T>> ToListAsync()
    {
        var count = await CountAsync();
        var result = new List<T>();

        for (int i = 0; i < count; i++)
            result.Add(this[i]);

        return result;
    }
}