using Emprestai.Domain.Entities;

namespace Emprestai.Application.Repositories;

public interface ILoanRepository : IRepository<Loan>
{
    Task<bool> HasOverdueLoansAsync(Guid personId);
    Task<bool> IsItemBorrowedAsync(Guid itemId);
}   
