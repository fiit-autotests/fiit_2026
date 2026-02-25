namespace Kontur.BigLibrary.Tests.UI.PW.Helpers;

public static class TestEnvironment
{
    public static readonly bool Headless = GetBoolEnvironmentVariable("HEADLESS");
    
    private static bool GetBoolEnvironmentVariable(string variable)
    {
        var value = Environment.GetEnvironmentVariable(variable);
        return bool.TryParse(value, out var result) ? result : false;
    }
}