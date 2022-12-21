using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zip.InstallmentsService.Api.Common.Queries;
using Zip.InstallmentsService.Api.Common.Responses;
using Zip.InstallmentsService.Api.Data.EntityFramework.Common;
using Zip.InstallmentsService.Api.Domain.Core;

namespace Zip.InstallmentsService.Api.Data.EntityFramework.QueryHandlers;

public class PaymentPlanQueryHandler : QueryHandler,
    IRequestHandler<PaymentPlanQuery, PaymentPlanResponse>
{
    private readonly IMapper _mapper;

    public PaymentPlanQueryHandler(PaymentPlanContext context, IMapper mapper) : base(context)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<PaymentPlanResponse> Handle(PaymentPlanQuery request, CancellationToken cancellationToken)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        var paymentPlan =
            await Context.PaymentPlans.SingleOrDefaultAsync(
                x => x.Id == request.PaymentPlanId, cancellationToken);

        return paymentPlan == null ? null : _mapper.Map<PaymentPlan, PaymentPlanResponse>(paymentPlan);
    }
}