using Emprestai.Application.Persons.Create;
using FluentValidation;

namespace Emprestai.Application.Validators.Person;

public class CreatePersonValidator : AbstractValidator<CreatePersonCommand>
{
	public CreatePersonValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty().WithMessage("Nome é obrigatório.")
			.MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres.");

		RuleFor(x => x)
			.Must(x => !string.IsNullOrWhiteSpace(x.Email) || !string.IsNullOrWhiteSpace(x.Phone))
			.WithMessage("Pelo menos uma informação de contato precisa ser preenchida (E-mail ou Telefone).");

		RuleFor(x => x.Email)
			.EmailAddress()
			.WithMessage("O e-mail não possui um formato válido.")
			.When(x => !string.IsNullOrWhiteSpace(x.Email));

        RuleFor(x => x.Phone)
			.Matches(@"^[1-9]{2}(?:[0-9]{8}|9[0-9]{8})$")
			.WithMessage("Informe um telefone com DDD, contendo apenas números: 10 dígitos para fixo ou 11 para celular.")
			.When(x => !string.IsNullOrWhiteSpace(x.Phone));
    }
}
