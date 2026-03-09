using NUnit.Framework;
using UserCreatorTask.UserValidators;

namespace UserValidatorTests;

[Parallelizable(ParallelScope.Fixtures)]
public class EmailValidatorTests
{
    private readonly EmailValidator _validator = new();

    [TestCaseSource(nameof(MultiplyCases))]
    public bool Validate_Email_ReturnsIsValid(string password) => _validator.IsValid(password);

    private static IEnumerable<TestCaseData> MultiplyCases()
    {
        // Positive tests
        yield return new TestCaseData("simple@mail.com").Returns(true)
            .SetName("IsValid_ShouldReturnTrue_WhenEmailIsValid");
        yield return new TestCaseData("with.dot@mail.com").Returns(true)
            .SetName("IsValid_ShouldReturnTrue_WhenEmailContainsDotInLocalPart");
        yield return new TestCaseData("with-hyphen@mail.com").Returns(true)
            .SetName("IsValid_ShouldReturnTrue_WhenEmailContainsHyphenInLocalPart");
        yield return new TestCaseData("with_underscore@mail.com").Returns(true)
            .SetName("IsValid_ShouldReturnTrue_WhenEmailContainsUnderscoreInLocalPart");
        yield return new TestCaseData("with123@mail.com").Returns(true)
            .SetName("IsValid_ShouldReturnTrue_WhenEmailContainsNumbersInLocalPart");
        yield return new TestCaseData("apple@sub.domain.com").Returns(true)
            .SetName("IsValid_ShouldReturnTrue_WhenEmailContainsSubdomain");
        yield return new TestCaseData("apple@mail.ru").Returns(true)
            .SetName("IsValid_ShouldReturnTrue_WhenTldHasTwoChars");
        yield return new TestCaseData("apple@mail.info").Returns(true)
            .SetName("IsValid_ShouldReturnTrue_WhenTldHasFourChars");
        yield return new TestCaseData("apple123.doe-smith_test@sub.domain.info").Returns(true)
            .SetName("IsValid_ShouldReturnTrue_WhenEmailIsComplexWithAllValidElements");
        yield return new TestCaseData("apple..doe@mail.com").Returns(true)
            .SetName("IsValid_ShouldReturnTrue_WhenLocalPartContainsConsecutiveDots");

        // Negative tests
        yield return new TestCaseData("").Returns(false)
            .SetName("IsValid_ShouldReturnFalse_WhenEmailEmpty");
        yield return new TestCaseData("@mail.com").Returns(false)
            .SetName("IsValid_ShouldReturnFalse_WhenLocalPartIsEmpty");
        yield return new TestCaseData("apple+doe@mail.com").Returns(false)
            .SetName("IsValid_ShouldReturnFalse_WhenLocalPartContainsInvalidSymbol");
        yield return new TestCaseData("applemail.com").Returns(false)
            .SetName("IsValid_ShouldReturnFalse_WhenEmailDoesNotContainAtSymbol");
        yield return new TestCaseData("apple@@mail.com").Returns(false)
            .SetName("IsValid_ShouldReturnFalse_WhenEmailContainsMultipleAtSymbols");
        yield return new TestCaseData("apple@.mail.com").Returns(false)
            .SetName("IsValid_ShouldReturnFalse_WhenDomainStartsWithDot");
        yield return new TestCaseData("apple@mail..com").Returns(false)
            .SetName("IsValid_ShouldReturnFalse_WhenDomainContainsConsecutiveDots");
        yield return new TestCaseData("apple@mail.c").Returns(false)
            .SetName("IsValid_ShouldReturnFalse_WhenTldIsTooShort");
        yield return new TestCaseData("apple@mail.abcdef").Returns(false)
            .SetName("IsValid_ShouldReturnFalse_WhenTldIsTooLong");
    }
    
    [Test]
    public void IsValid_ThrowsArgumentNullException_WhenEmailIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => _validator.IsValid(null), 
            "Expected ArgumentNullException exception to be thrown.");
    }
}