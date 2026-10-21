using Emprestai.Domain.Entities;
using Xunit;

namespace Emprestai.Tests.Domain;

public class LoanTests
{
    [Fact]
    public void Return_WithValidDate_ShouldSetReturnedAt()
    {
        // Arrange
        var returnedAt = new DateTime(2026, 06, 18);
        var loan = new Loan(Guid.NewGuid(), Guid.NewGuid(), new DateTime(2026, 06, 10), new DateTime(2026, 06, 20));

        // Act
        loan.Return(returnedAt);

        // Assert
        Assert.Equal(returnedAt, loan.ReturnedAt);
    }

    [Fact]
    public void ShouldThrow_ArgumentException_WhenReturnedAtIsPreviousThanBorrowedAt()
    {
        var returnedAt = new DateTime(2026, 06, 05);
        var loan = new Loan(Guid.NewGuid(), Guid.NewGuid(), new DateTime(2026, 06, 10), new DateTime(2026, 06, 20));

        Assert.Throws<ArgumentException>(() => loan.Return(returnedAt));
        Assert.Null(loan.ReturnedAt);
    }

    [Fact]
    public void ShouldThrow_InvalidOperationException_WhenReturnedAtIsAlreadyFilled()
    {
        // Arrange
        var firstReturnedAt = new DateTime(2026, 06, 18);
        var secondReturnedAt = new DateTime(2026, 06, 19);
        var loan = new Loan(Guid.NewGuid(), Guid.NewGuid(), new DateTime(2026, 06, 10), new DateTime(2026, 06, 20));
        loan.Return(firstReturnedAt);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => loan.Return(secondReturnedAt));
        Assert.Equal(firstReturnedAt, loan.ReturnedAt);
    }

    [Fact]
    public void Return_WhenReturnedAtIsEqualToBorrowedAt_ShouldSetReturnedAt()
    {
        // Arrange
        var returnedAt = new DateTime(2026, 06, 10);
        var loan = new Loan(Guid.NewGuid(), Guid.NewGuid(), new DateTime(2026, 06, 10), new DateTime(2026, 06, 15));

        // Act
        loan.Return(returnedAt);

        // Assert
        Assert.Equal(returnedAt, loan.ReturnedAt);
    }
}
