using System.Reflection;
using Kontur.BigLibrary.Tests.Core;
using Kontur.BigLibrary.Tests.Core.Helpers.StringGenerator;
using Kontur.BigLibrary.Tests.UI.PW.Helpers;
using Newtonsoft.Json;

namespace Kontur.BigLibrary.Tests.UI.PW.PlaywrightCore;

public class AuthContextProvider(BooksTestDataService dataService) : IAuthContextProvider
{
    public string? GetStorageStatePathAsync(Type testClass)
    {
        var attr = testClass.GetCustomAttribute<WithAuthAttribute>();
        if (attr == null)
            return null;
        
        return CreateAuthJson(attr.Email, attr.Password);
    }
    
    private string CreateAuthJson(string? email = null, string? password = null)
    {
        var token = GetUserToken(email, password);
        return GenerateStorageStateJson(token);
    }
    
    private string GetUserToken(string? email = null, string? password = null)
    {
        email ??= StringGenerator.GetEmail();
        password ??= StringGenerator.GetValidPassword();
        
        return dataService.GetOrCreateUserAndGetToken(email, password);
    }

    private string GenerateStorageStateJson(string token)
    {
        var json = new
        {
            cookies = Array.Empty<object>(),
            origins = new[]
            {
                new
                {
                    origin = ServiceInfo.GetServiceUrl(),
                    localStorage = new[]
                    {
                        new
                        {
                            name = "jwtToken",
                            value = "\u0022" + token + "\u0022"
                        }
                    }
                }
            }
        };

        var filePath = Path.Combine(AppContext.BaseDirectory, $"authState_{Guid.NewGuid()}.json");
        File.WriteAllText(filePath, JsonConvert.SerializeObject(json, Formatting.None));
        
        return filePath;
    }
}