using Emprestai.Application.Items.Create;
using FluentValidation;

namespace Emprestai.Application.Validators.Item;

public class CreateItemValidator : AbstractValidator<CreateItemCommand>
{
	public CreateItemValidator()
	{
        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Nome é obrigatório.")
            .Must(name => name.Trim().Length <= 100)
            .WithMessage("Nome deve ter no máximo 100 caracteres.");

        RuleFor(x => x.Description)
            .Must(description => description is null ||
                                 description.Trim().Length <= 200)
            .WithMessage("Descrição deve ter no máximo 200 caracteres.");
    }
}
