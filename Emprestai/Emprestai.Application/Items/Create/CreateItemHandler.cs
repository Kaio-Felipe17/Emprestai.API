using AutoMapper;
using Emprestai.Application.DTOs.Item;
using Emprestai.Application.Repositories;
using Emprestai.Domain.Entities;
using MediatR;

namespace Emprestai.Application.Items.Create;

public class CreateItemHandler(IItemRepository itemRepository, IMapper mapper) : IRequestHandler<CreateItemCommand, ItemDTO>
{
    public async Task<ItemDTO> Handle(CreateItemCommand command, CancellationToken cancellationToken)
    {
        var item = new Item(command.Name, command.Description);
        await itemRepository.CreateAsync(item, cancellationToken);
        return mapper.Map<ItemDTO>(item);
    }
}
