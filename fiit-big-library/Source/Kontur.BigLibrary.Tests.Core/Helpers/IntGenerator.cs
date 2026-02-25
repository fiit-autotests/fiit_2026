namespace Kontur.BigLibrary.Tests.Core.Helpers;

public static class IntGenerator
{
    private static readonly Random Random = new();

    public static int Get()
    {
        return Random.Next();
    }
}