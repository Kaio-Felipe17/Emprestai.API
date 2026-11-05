using Emprestai.API.Endpoints;
using Emprestai.API.Errors;
using Emprestai.Application.Behaviors;
using Emprestai.Application.Loans.Create;
using Emprestai.Application.Mappings;
using Emprestai.Application.Persons.Create;
using Emprestai.Application.Repositories;
using Emprestai.Application.Validators.Loan;
using Emprestai.Application.Validators.Person;
using Emprestai.Infrastructure.Context;
using Emprestai.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ApiExceptionHandler>();

#region MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateLoanHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(CreatePersonHandler).Assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
#endregion

#region Repositories
builder.Services.AddScoped<ILoanRepository, LoanRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
#endregion

#region Automapper
builder.Services.AddAutoMapper(cfg => cfg.AddMaps(typeof(LoanMappingProfile).Assembly));
builder.Services.AddAutoMapper(cfg => cfg.AddMaps(typeof(PersonMappingProfile).Assembly));
#endregion

#region Validators
builder.Services.AddValidatorsFromAssemblyContaining<CreateLoanValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreatePersonValidator>();
#endregion

var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

#region Map Endpoints
app.MapLoanEndpoints();
#endregion

app.Run();
