using Emprestai.Application.Persons.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Emprestai.API.Endpoints;

public static class PersonEndpoints
{
    public static void MapPersonEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/persons")
            .WithTags("Persons");

        group.MapPost("", CreatePerson);
    }

    public static async Task<IResult> CreatePerson([FromBody] CreatePersonCommand command, IMediator mediator)
    {
        var result = await mediator.Send(command);
        return Results.Ok(result);
    }
}
