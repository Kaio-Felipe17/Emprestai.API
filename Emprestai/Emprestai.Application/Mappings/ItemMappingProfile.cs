using AutoMapper;
using Emprestai.Application.DTOs.Item;
using Emprestai.Domain.Entities;

namespace Emprestai.Application.Mappings;

public class ItemMappingProfile : Profile
{
	public ItemMappingProfile()
	{
		CreateMap<Item, ItemDTO>();
	}
}
