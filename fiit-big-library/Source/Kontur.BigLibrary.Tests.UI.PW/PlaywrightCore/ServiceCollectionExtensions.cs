using Kontur.BigLibrary.Tests.Core.ApiClients;
using Kontur.BigLibrary.Tests.UI.PW.Helpers;
using Kontur.BigLibrary.Tests.UI.PW.PageObjects.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace Kontur.BigLibrary.Tests.UI.PW.PlaywrightCore;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPlaywright(this IServiceCollection services)
        => services.AddSingleton<IPlaywrightGetter, PlaywrightSingleton>()
            .AddScoped<IBrowserGetter, ChromiumGetter>()
            .AddSingleton<IAuthContextProvider, AuthContextProvider>()
            .AddScoped<IBrowserContextGetter, BrowserContextGetter>()
            .AddScoped<IPlaywrightPageGetter, PlaywrightPageGetter>()
            .AddScoped<IPageFactory, PageFactory>()
            .AddScoped<IControlFactory, SimpleControlFactory>()
            .AddScoped<IDependenciesFactory, DependencyFactory>()
            .AddScoped<Navigation>();

    public static IServiceCollection AddTestDataHelpers(this IServiceCollection services)
        => services.AddSingleton<AuthApiClient>()
            .AddSingleton<BooksApiClient>()
            .AddSingleton<BooksTestDataService>();
}