using Emprestai.Application.DTOs.Person;
using MediatR;

namespace Emprestai.Application.Persons.Create;

public record CreatePersonCommand : IRequest<PersonDTO>
{
    public required string Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
}
