using Emprestai.Application.DTOs.Item;
using MediatR;

namespace Emprestai.Application.Items.Create;

public record CreateItemCommand : IRequest<ItemDTO>
{
    public required string Name { get; set; }
    public string? Description { get; set; }
}
