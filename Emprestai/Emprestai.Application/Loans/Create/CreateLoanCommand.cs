using Emprestai.Application.DTOs.Loan;
using MediatR;

namespace Emprestai.Application.Loans.Create;

public record CreateLoanCommand : IRequest<LoanDTO>
{
    public required Guid PersonId { get; init; }
    public required Guid ItemId { get; init; }
    public required DateTime DueAt { get; init; }
}
