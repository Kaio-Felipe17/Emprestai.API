using AutoMapper;
using Emprestai.Application.DTOs.Person;
using Emprestai.Application.Repositories;
using Emprestai.Domain.Entities;
using MediatR;

namespace Emprestai.Application.Persons.Create;

public class CreatePersonHandler(IPersonRepository personRepository, IMapper mapper) : IRequestHandler<CreatePersonCommand, PersonDTO>
{
    public async Task<PersonDTO> Handle(CreatePersonCommand command, CancellationToken cancellationToken)
    {
        var person = new Person(command.Name, command?.Email, command?.Phone);

        if (await personRepository.ExistsByEmailOrPhoneAsync(email: person?.Email, phone: person?.Phone, cancellationToken))
            throw new InvalidOperationException("Email ou telefone já cadastrados.");

        await personRepository.CreateAsync(person, cancellationToken);
        return mapper.Map<PersonDTO>(person);
    }
}
