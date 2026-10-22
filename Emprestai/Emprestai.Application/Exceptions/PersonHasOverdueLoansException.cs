namespace Emprestai.Application.Exceptions;

public class PersonHasOverdueLoansException : Exception
{
    public PersonHasOverdueLoansException(string message) : base(message) { }
}
