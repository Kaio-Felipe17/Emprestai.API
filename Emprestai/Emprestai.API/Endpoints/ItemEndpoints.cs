using Emprestai.Application.Items.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Emprestai.API.Endpoints;

public static class ItemEndpoints
{
    public static void MapItemEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/items")
            .WithTags("Items");

        group.MapPost("", CreateItem);
    }

    public static async Task<IResult> CreateItem([FromBody] CreateItemCommand command, IMediator mediator, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Results.Ok(result);
    }
}
