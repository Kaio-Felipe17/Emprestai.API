using Emprestai.Domain.Common;

namespace Emprestai.Domain.Entities;

public class Loan : BaseEntity
{
    public Guid PersonId { get; private set; }
    public Guid ItemId { get; private set; }
    public DateTime BorrowedAt { get; private set; }
    public DateTime DueAt { get; private set; }
    public DateTime? ReturnedAt { get; private set; }

    public Loan(Guid personId, Guid itemId, DateTime borrowedAt, DateTime dueAt)
    {
        if (dueAt <= borrowedAt)
            throw new ArgumentException(
                "O prazo de devolução deve ser posterior ao início do empréstimo.",
                nameof(dueAt));

        PersonId = personId;
        ItemId = itemId;
        BorrowedAt = borrowedAt;
        DueAt = dueAt;
    }

    public void Return(DateTime returnedAt)
    {
        if (ReturnedAt.HasValue)
            throw new InvalidOperationException("O empréstimo já foi devolvido.");

        if (returnedAt < BorrowedAt)
            throw new ArgumentException("A data de devolução não pode ser anterior à data de empréstimo.", nameof(returnedAt));

        ReturnedAt = returnedAt;
    }
}