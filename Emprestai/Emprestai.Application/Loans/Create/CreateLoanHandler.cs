using AutoMapper;
using Emprestai.Application.DTOs.Loan;
using Emprestai.Application.Exceptions;
using Emprestai.Application.Repositories;
using Emprestai.Domain.Entities;
using MediatR;

namespace Emprestai.Application.Loans.Create;

public class CreateLoanHandler(
    ILoanRepository loanRepository, 
    IPersonRepository personRepository, 
    IItemRepository itemRepository,
    IMapper mapper) : IRequestHandler<CreateLoanCommand, LoanDTO>
{
    public async Task<LoanDTO> Handle(CreateLoanCommand command, CancellationToken cancellationToken)
    {
        var person = await personRepository.GetAsync(command.PersonId, cancellationToken);
        if (person == null)
            throw new KeyNotFoundException("Pessoa não cadastrada.");

        var hasOverdueLoans = await loanRepository.HasOverdueLoansAsync(command.PersonId);
        if (hasOverdueLoans)
            throw new PersonHasOverdueLoansException("A pessoa possui um empréstimo atrasado. Não é possível realizar um novo empréstimo");

        var item = await itemRepository.GetAsync(command.ItemId, cancellationToken);
        if (item == null)
            throw new KeyNotFoundException("Item não cadastrado.");

        if (item.IsUnderMaintenance)
            throw new ItemUnderMaintenanceException("Item em manutenção.");

        var isItemBorrowed = await loanRepository.IsItemBorrowedAsync(command.ItemId);
        if (isItemBorrowed)
            throw new ItemAlreadyBorrowedException("Esse item já foi emprestado.");

        if (command.DueAt <= DateTime.UtcNow)
            throw new InvalidOperationException("O prazo devolução deve ser superior à data de empréstimo.");

        var borrowedAt = DateTime.UtcNow;
        var newLoan = new Loan(command.PersonId, command.ItemId, borrowedAt, command.DueAt);
        await loanRepository.CreateAsync(newLoan, cancellationToken);
        return mapper.Map<LoanDTO>(newLoan);
    }
}
