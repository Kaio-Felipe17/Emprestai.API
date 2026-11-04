using Emprestai.Domain.Entities;
using System;
using Xunit;

namespace Emprestai.Tests.Domain;

public class ItemTests
{
    [Fact]
    public void StartMaintenance_ShouldMarkItemAsUnderMaintenance()
    {
        // Arrange
        var item = new Item("Item 1", null);

        // Act
        item.StartMaintenance();

        // Assert
        Assert.True(item.IsUnderMaintenance);
    }

    [Fact]
    public void StartMaintenance_ShouldThrow_WhenItemIsAlreadyUnderMaintenance()
    {
        // Arrange
        var item = new Item("Item 1", null);
        item.StartMaintenance();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(item.StartMaintenance);
        Assert.True(item.IsUnderMaintenance);
    }

    [Fact]
    public void FinishMaintenance_ShouldUnmarkItemAsUnderMaintenance()
    {
        // Arrange
        var item = new Item("Item 1", null);
        item.StartMaintenance();

        // Act
        item.FinishMaintenance();

        // Assert
        Assert.False(item.IsUnderMaintenance);
    }

    [Fact]
    public void FinishMaintenance_ShouldThrow_WhenItemIsNotUnderMaintenance()
    {
        // Arrange
        var item = new Item("Item 1", null);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(item.FinishMaintenance);
        Assert.False(item.IsUnderMaintenance);
    }

    [Fact]
    public void Constructor_ShouldCreateItem_WhenNameAndDescriptionAreValid()
    {
        // Arrange
        var name = "Item 1";
        var description = "Descrição item 1";

        // Act
        var item = new Item(name, description);

        // Assert
        Assert.Equal(name, item.Name);
        Assert.Equal(description, item.Description);
        Assert.False(item.IsUnderMaintenance);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_ShouldThrowArgumentException_WhenNameIsMissing(string? name)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Item(name!, null));
    }

    [Fact]
    public void Constructor_ShouldCreateItem_WhenNameHas100Characters()
    {
        // Arrange
        var expectedName = new string('A', 100);
        var name = $" {expectedName} ";

        // Act
        var item = new Item(name, null);

        // Assert
        Assert.Equal(expectedName, item.Name);
        Assert.Null(item.Description);
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentException_WhenNameHas101Characters()
    {
        // Arrange
        var name = new string('A', 101);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Item(name, null));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_ShouldSaveDescriptionNull_WhenIsMissing(string? description)
    {
        // Arrange
        var name = "Item 1";

        // Act
        var item = new Item(name, description);

        // Assert
        Assert.Equal(name, item.Name);
        Assert.Null(item.Description);
    }

    [Fact]
    public void Constructor_ShouldCreateItem_WhenDescriptionHas200Characters()
    {
        // Arrange
        var name = "Item";
        var expectedDescription = new string('A', 200);
        var description = $" {expectedDescription} ";

        // Act
        var item = new Item(name, description);

        // Assert
        Assert.Equal(name, item.Name);
        Assert.Equal(expectedDescription, item.Description);
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentException_WhenDescriptionHas201Characters()
    {
        // Arrange
        var name = "Item";
        var description = new string('A', 201);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Item(name, description));
    }

    [Fact]
    public void Constructor_ShouldTrimNameAndDescription_WhenTheyContainSurroundingSpaces()
    {
        // Arrange
        var name = "     Item 1     ";
        var description = "     Descrição item 1     ";

        // Act
        var item = new Item(name, description);

        // Assert
        Assert.Equal("Item 1", item.Name);
        Assert.Equal("Descrição item 1", item.Description);
        Assert.False(item.IsUnderMaintenance);
    }
}
