namespace Kontur.BigLibrary.Tests.Core;

public static class ServiceInfo
{
    public static string GetServiceUrl()
    {
        return Environment.GetEnvironmentVariable("FIIT_SERVICE_URL") ?? "http://localhost:5000";
    }
}