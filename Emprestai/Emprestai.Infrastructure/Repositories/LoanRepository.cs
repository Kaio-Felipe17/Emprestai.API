using Emprestai.Application.Repositories;
using Emprestai.Domain.Entities;
using Emprestai.Infrastructure.Common;
using Emprestai.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Emprestai.Infrastructure.Repositories;

public class LoanRepository(ApiDbContext context) : BaseRepository<Loan>(context), ILoanRepository
{
    public async Task<bool> HasOverdueLoansAsync(Guid personId)
    {
        return await _context.Loans
          .AsNoTracking()
          .AnyAsync(x =>
              x.PersonId == personId &&
              x.ReturnedAt == null &&
              x.DueAt < DateTime.UtcNow);
    }

    public async Task<bool> IsItemBorrowedAsync(Guid itemId) 
        => await _context.Loans.AsNoTracking().AnyAsync(x => x.ItemId == itemId && x.ReturnedAt == null);
}
