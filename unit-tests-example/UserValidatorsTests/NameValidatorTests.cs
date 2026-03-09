using NUnit.Framework;
using UserCreatorTask;
using UserCreatorTask.UserValidators;

namespace UserValidatorTests;

[Parallelizable(ParallelScope.Fixtures)]
public class NameValidatorTests
{
    private readonly NameValidator _validator = new();

    [TestCaseSource(nameof(MultiplyCases))]
    public bool IsValid_NameValidation_ReturnsExpectedResult(string name) => _validator.IsValid(name);

    private static IEnumerable<TestCaseData> MultiplyCases()
    {
        // Positive tests
        yield return new TestCaseData("Apple Banana").Returns(true)
            .SetName("IsValid_ShouldReturnTrue_WhenNameAndSurnameAreValid");
        yield return new TestCaseData("A B").Returns(true)
            .SetName("IsValid_ShouldReturnTrue_WhenNameAndSurnameAreSingleLetters");

        // Negative tests
        yield return new TestCaseData("Apple").Returns(false)
            .SetName("IsValid_ShouldReturnFalse_WhenOnlyOneWordProvided");
        yield return new TestCaseData("Apple Banana Third").Returns(false)
            .SetName("IsValid_ShouldReturnFalse_WhenThreeWordsProvided");
        yield return new TestCaseData("").Returns(false)
            .SetName("IsValid_ShouldReturnFalse_WhenStringIsEmpty");
        yield return new TestCaseData("   ").Returns(false)
            .SetName("IsValid_ShouldReturnFalse_WhenStringContainsOnlySpaces");
        yield return new TestCaseData(" Apple Banana").Returns(false)
            .SetName("IsValid_ShouldReturnFalse_WhenNameHasLeadingSpace");
        yield return new TestCaseData("Apple Banana ").Returns(false)
            .SetName("IsValid_ShouldReturnFalse_WhenNameHasTrailingSpace");
        yield return new TestCaseData("Apple  Banana").Returns(false)
            .SetName("IsValid_ShouldReturnFalse_WhenNameHasDoubleSpaceBetweenWords");
        yield return new TestCaseData("Apple123 Banana").Returns(false)
            .SetName("IsValid_ShouldReturnFalse_WhenNameContainsNumbers");
        yield return new TestCaseData("Apple B'anana").Returns(false)
            .SetName("IsValid_ShouldReturnFalse_WhenNameContainsSpecialSymbols");
        yield return new TestCaseData("Яблоко Банан").Returns(false)
            .SetName("IsValid_ShouldReturnFalse_WhenNameIsCyrillic");
    }
    
    [Test]
    public void IsValid_ShouldThrowArgumentNullException_WhenNameIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => _validator.IsValid(null), 
            "Expected ArgumentNullException exception to be thrown.");
    }
}