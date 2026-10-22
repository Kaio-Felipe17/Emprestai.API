namespace Emprestai.Application.Exceptions;

public class ItemUnderMaintenanceException : Exception
{
    public ItemUnderMaintenanceException(string message) : base(message) { }
}
