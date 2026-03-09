using NUnit.Framework;
using UserCreatorTask.UserValidators;

namespace UserValidatorTests;

[Parallelizable(ParallelScope.Children)]
public class PasswordValidatorTests
{
    private readonly PasswordValidator _validator = new();

    [TestCaseSource(nameof(MultiplyCases))]
    public bool Validate_Password_ReturnsIsValid(string password) => _validator.IsValid(password);

    private static IEnumerable<TestCaseData> MultiplyCases()
    {
        // Positive tests
        yield return new TestCaseData(new string('a', 95) + "A322?!").Returns(true)
            .SetName("IsValid_ShouldReturnTrue_WhenPasswordMeetsAllRequirements");
        yield return new TestCaseData(new string('a', 93) + " A322!?").Returns(true)
            .SetName("IsValid_ShouldReturnTrue_WhenPasswordContainsSpaces");
        
        // Negative tests
        yield return new TestCaseData(new string('Ъ', 95) + "Ъ322!?").Returns(false)
            .SetName("IsValid_ShouldReturnFalse_WhenPasswordContainsNonEnglishCharacters");
        yield return new TestCaseData("a1A!" + new string('a', 52)).Returns(false)
            .SetName("IsValid_ShouldReturnFalse_WhenPasswordIsTooShort");
        yield return new TestCaseData(new string('a', 96) + "A322").Returns(false)
            .SetName("IsValid_ShouldReturnFalse_WhenPasswordMissingSpecialSymbols");
        yield return new TestCaseData(new string('a', 96) + "322?!").Returns(false)
            .SetName("IsValid_ShouldReturnFalse_WhenPasswordMissingUppercase");
        yield return new TestCaseData(new string('A', 96) + "322?!").Returns(false)
            .SetName("IsValid_ShouldReturnFalse_WhenPasswordMissingLowercase");
        yield return new TestCaseData(new string('a', 97) + "ww!").Returns(false)
            .SetName("IsValid_ShouldReturnFalse_WhenPasswordMissingNumbers");
    }

    [Test]
    [NonParallelizable]
    public void IsValid_ShouldThrowArgumentNullException_WhenPasswordIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => _validator.IsValid(null),
            "Expected ArgumentNullException exception to be thrown.");
    }
}