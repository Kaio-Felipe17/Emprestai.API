using Emprestai.Domain.Entities;
using Xunit;

namespace Emprestai.Tests.Domain;

public class PersonTests
{
    [Fact]
    public void Constructor_ShouldTrimNameAndEmail_WhenTheyContainSurroundingSpaces()
    {
        // Arrange
        var name = " Ana ";
        var email = " ana@email.com ";

        // Act
        var person = new Person(name, email, null);

        // Assert
        Assert.Equal("Ana", person.Name);
        Assert.Equal("ana@email.com", person.Email);
        Assert.Null(person.Phone);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_ShouldThrowArgumentException_WhenNameIsMissing(string? name)
    {
        Assert.Throws<ArgumentException>(() => new Person(name!, "ana@email.com", null));
    }

    [Theory]
    [InlineData("Ana", null, null)]
    [InlineData("Ana", " ", " ")]
    public void Constructor_ShouldThrowArgumentException_WhenContactInfoIsMissing(
        string name,
        string? email,
        string? phone)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Person(name, email, phone));
    }

    [Theory]
    [InlineData("ana@email.com", null, "ana@email.com", null)]
    [InlineData(null, "11911111111", null, "11911111111")]
    [InlineData("ana@email.com", "11911111111", "ana@email.com", "11911111111")]
    public void Constructor_ShouldCreatePerson_WhenAtLeastOneContactInfoIsFilled(
        string? email,
        string? phone,
        string? expectedEmail,
        string? expectedPhone)
    {
        // Arrange
        var name = "Ana";

        // Act
        var person = new Person(name, email, phone);

        // Assert
        Assert.Equal(expectedEmail, person.Email);
        Assert.Equal(expectedPhone, person.Phone);
    }
}
