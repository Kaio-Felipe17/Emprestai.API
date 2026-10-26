using Emprestai.Application.Loans.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Emprestai.API.Endpoints;

public static class LoanEndpoints
{
    public static void MapLoanEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/loans")
            .WithTags("Loans");

        group.MapPost("", CreateLoan);
    }

    public static async Task<IResult> CreateLoan([FromBody] CreateLoanCommand command, IMediator mediator)
    {
        var response = await mediator.Send(command);
        return Results.Ok(response);
    }
}
