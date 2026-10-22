namespace Emprestai.Application.Exceptions;

public class ItemAlreadyBorrowedException : Exception
{
    public ItemAlreadyBorrowedException(string message) : base(message) { }
}
