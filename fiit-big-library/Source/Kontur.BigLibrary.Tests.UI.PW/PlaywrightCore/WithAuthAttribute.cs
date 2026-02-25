namespace Kontur.BigLibrary.Tests.UI.PW.PlaywrightCore;

[AttributeUsage(AttributeTargets.Class)]
public sealed class WithAuthAttribute : Attribute
{
    public string? Email { get; }
    public string? Password { get; }

    public WithAuthAttribute(string? email = null, string? password = null)
    {
        Email = email;
        Password = password;
    }
}