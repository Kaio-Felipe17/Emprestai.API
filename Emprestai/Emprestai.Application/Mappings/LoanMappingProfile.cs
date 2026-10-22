using AutoMapper;
using Emprestai.Application.DTOs.Loan;
using Emprestai.Application.Loans.Create;
using Emprestai.Domain.Entities;

namespace Emprestai.Application.Mappings;

public class LoanMappingProfile : Profile
{
    public LoanMappingProfile()
    {
        CreateMap<CreateLoanCommand, Loan>()
            .ForMember(dest => dest.BorrowedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.ReturnedAt, opt => opt.Ignore());

        CreateMap<Loan, LoanDTO>().ReverseMap();
    }
}
