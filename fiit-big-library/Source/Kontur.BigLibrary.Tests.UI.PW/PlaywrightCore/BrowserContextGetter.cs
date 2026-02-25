using Microsoft.Playwright;
using NUnit.Framework.Interfaces;

namespace Kontur.BigLibrary.Tests.UI.PW.PlaywrightCore;

public class BrowserContextGetter(IBrowserGetter browserGetter, IAuthContextProvider authContextProvider)
    : IBrowserContextGetter
{
    /// <summary>
    ///     Лениво инициализируемый контекст браузера.
    ///     Создаётся при первом обращении.
    /// </summary>
    private readonly Lazy<Task<IBrowserContext>> _browserContext
        = new(() => CreateContextAsync(browserGetter, authContextProvider));

    public Task<IBrowserContext> GetAsync()
    {
        return _browserContext.Value;
    }

    private static async Task<IBrowserContext> CreateContextAsync(IBrowserGetter getter,
        IAuthContextProvider authContextProvider)
    {
        var browser = await getter.GetAsync();

        var storageStatePath = authContextProvider.GetStorageStatePathAsync(
            Type.GetType(TestContext.CurrentContext.Test.ClassName!)!);

        var context = await browser.NewContextAsync(
            new BrowserNewContextOptions
            {
                StorageStatePath = storageStatePath,
                RecordVideoDir = "videos/"
            }
        );
        await context.Tracing.StartAsync(new TracingStartOptions
        {
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });
        return context;
    }
    
    public async ValueTask DisposeAsync()
    {
        var context = await _browserContext.Value;

        if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
        {
            var traceDir = Path.Combine(
                TestContext.CurrentContext.WorkDirectory, "traces");

            Directory.CreateDirectory(traceDir);

            var tracePath = Path.Combine(
                traceDir,
                $"{TestContext.CurrentContext.Test.Name}.zip");

            await context.Tracing.StopAsync(new TracingStopOptions
            {
                Path = tracePath
            });

            TestContext.WriteLine($"Trace: {tracePath}");
        }
        else
        {
            await context.Tracing.StopAsync();
        }

        await context.CloseAsync();
    }
}