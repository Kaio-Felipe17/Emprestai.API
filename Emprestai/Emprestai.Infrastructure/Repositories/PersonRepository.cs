using Emprestai.Application.Repositories;
using Emprestai.Domain.Entities;
using Emprestai.Infrastructure.Common;
using Emprestai.Infrastructure.Context;

namespace Emprestai.Infrastructure.Repositories;

public class PersonRepository(ApiDbContext context) : BaseRepository<Person>(context), IPersonRepository
{
}
