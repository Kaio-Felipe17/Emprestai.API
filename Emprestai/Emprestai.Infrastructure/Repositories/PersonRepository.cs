using Emprestai.Application.Repositories;
using Emprestai.Domain.Entities;
using Emprestai.Infrastructure.Common;
using Emprestai.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Emprestai.Infrastructure.Repositories;

public class PersonRepository(ApiDbContext context) : BaseRepository<Person>(context), IPersonRepository
{
    public async Task<bool> ExistsByEmailOrPhoneAsync(
        string? email = null,
        string? phone = null,
        CancellationToken cancellationToken = default)
    {
        var hasEmail = !string.IsNullOrWhiteSpace(email);
        var hasPhone = !string.IsNullOrWhiteSpace(phone);

        return await context.Persons.AnyAsync(
            x => (hasEmail && x.Email == email) ||
                 (hasPhone && x.Phone == phone),
            cancellationToken);
    }
}
