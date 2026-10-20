using Microsoft.EntityFrameworkCore;

namespace Emprestai.Infrastructure.Context;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }
}
