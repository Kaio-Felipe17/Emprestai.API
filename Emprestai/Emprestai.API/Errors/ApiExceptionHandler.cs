using Emprestai.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Emprestai.API.Errors;

public sealed class ApiExceptionHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        var (status, title, detail) = exception switch
        {
            ItemAlreadyBorrowedException => (409, "Item já emprestado", exception.Message),
            ItemUnderMaintenanceException => (409, "Item em manutenção", exception.Message),
            PersonHasOverdueLoansException => (409, "Pessoa com empréstimos atrasados", exception.Message),
            ArgumentException => (400, "Dados inválidos", exception.Message),
            FluentValidation.ValidationException validation => (
                400,
                "Dados inválidos",
                string.Join("; ", validation.Errors
                    .Select(error => error.ErrorMessage))
            ),
            _ => (500, "Erro interno", "Não foi possível concluir a operação.")
        };

        context.Response.StatusCode = status;
        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = context,
            Exception = exception,
            ProblemDetails = new ProblemDetails { Status = status, Title = title, Detail = detail }
        });
    }
}