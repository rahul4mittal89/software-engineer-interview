using AutoMapper;
using Zip.InstallmentsService.Api.Common.Responses;
using Zip.InstallmentsService.Api.Domain.Core;

namespace Zip.InstallmentsService.Api.Common.Mappers;

public class PaymentPlanMapper : Profile
{
    public PaymentPlanMapper()
    {
        CreateMap<PaymentPlan, PaymentPlanResponse>()
            .ForMember(dest => dest.PaymentPlanId, opt => opt.MapFrom(src => src.Id));

        CreateMap<Installment, InstallmentResponse>()
            .ForMember(dest => dest.InstallmentId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate.ToString("MMM dd, yyyy")));
    }
}