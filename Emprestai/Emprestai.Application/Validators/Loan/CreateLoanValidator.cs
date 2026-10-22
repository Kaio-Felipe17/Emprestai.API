using Emprestai.Application.Loans.Create;
using FluentValidation;

namespace Emprestai.Application.Validators.Loan;

public class CreateLoanValidator : AbstractValidator<CreateLoanCommand>
{
    public CreateLoanValidator()
    {
        RuleFor(x => x.PersonId)
            .NotEmpty().WithMessage("O ID da pessoa é obrigatório.");

        RuleFor(x => x.ItemId)
            .NotEmpty().WithMessage("O ID do item é obrigatório.");

        RuleFor(x => x.DueAt)
            .NotEmpty().WithMessage("A data de devolução é obrigatória.")
            .Must(dueAt => dueAt > DateTime.UtcNow).WithMessage("A data de devolução deve ser superior à data atual.");
    }
}
