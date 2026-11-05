using Emprestai.Domain.Entities;

namespace Emprestai.Application.Repositories;

public interface IPersonRepository : IRepository<Person>
{
    Task<bool> ExistsByEmailOrPhoneAsync(
        string? email = null,
        string? phone = null,
        CancellationToken cancellationToken = default);
}
