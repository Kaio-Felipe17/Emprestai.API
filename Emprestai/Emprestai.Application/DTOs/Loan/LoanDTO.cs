namespace Emprestai.Application.DTOs.Loan;

public class LoanDTO
{
    public Guid Id { get; set; }
    public Guid PersonId { get; set; }
    public Guid ItemId { get; set; }
    public DateTime BorrowedAt { get; set; }
    public DateTime DueAt { get; set; }
    public DateTime? ReturnedAt { get; set; }
}
