using AutoMapper;
using Emprestai.Application.DTOs.Person;
using Emprestai.Domain.Entities;

namespace Emprestai.Application.Mappings;

public class PersonMappingProfile : Profile
{
	public PersonMappingProfile()
	{
		CreateMap<Person, PersonDTO>();
	}
}
