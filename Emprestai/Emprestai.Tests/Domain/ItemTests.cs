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
}
