using AutoMapper;
using Emprestai.Application.DTOs.Loan;
using Emprestai.Application.Loans.Create;
using Emprestai.Domain.Entities;

namespace Emprestai.Application.Mappings;

public class LoanMappingProfile : Profile
{
    public LoanMappingProfile()
    {
        CreateMap<Loan, LoanDTO>().ReverseMap();
    }
}
