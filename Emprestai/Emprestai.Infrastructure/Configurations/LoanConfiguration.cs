using Emprestai.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Emprestai.Infrastructure.Configurations;

public class LoanConfiguration : IEntityTypeConfiguration<Loan>
{
    public void Configure(EntityTypeBuilder<Loan> builder)
    {
        builder.ToTable("Loans");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.PersonId)
            .IsRequired();

        builder.Property(x => x.ItemId)
            .IsRequired();

        builder.Property(x => x.BorrowedAt)
            .IsRequired();

        builder.Property(x => x.DueAt)
            .IsRequired();
    }
}
