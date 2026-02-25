namespace Kontur.BigLibrary.Tests.UI.PW.PlaywrightCore;

public interface IAuthContextProvider
{
    string? GetStorageStatePathAsync(Type testClass);
}