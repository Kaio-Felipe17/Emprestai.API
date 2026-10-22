using Emprestai.Application.Loans.Create;
using Emprestai.Application.Mappings;
using Emprestai.Application.Repositories;
using Emprestai.Application.Validators.Loan;
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

#region MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateLoanHandler).Assembly));
#endregion

#region Repositories
builder.Services.AddScoped<ILoanRepository, LoanRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
#endregion

#region Automapper
builder.Services.AddAutoMapper(cfg => cfg.AddMaps(typeof(LoanMappingProfile).Assembly));
#endregion

#region Validators
builder.Services.AddValidatorsFromAssemblyContaining<CreateLoanValidator>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
