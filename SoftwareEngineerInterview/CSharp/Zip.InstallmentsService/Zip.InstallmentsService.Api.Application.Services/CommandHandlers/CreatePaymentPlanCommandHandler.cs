using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Zip.InstallmentsService.Api.Application.Services.Common;
using Zip.InstallmentsService.Api.Common.Commands;
using Zip.InstallmentsService.Api.Common.Common;
using Zip.InstallmentsService.Api.Common.Extensions;
using Zip.InstallmentsService.Api.Common.Responses;
using Zip.InstallmentsService.Api.Domain.Core;
using Zip.InstallmentsService.Api.Domain.Core.Interfaces.Repositories;
using Zip.InstallmentsService.Api.Domain.Core.SharedKernel.Interfaces;

namespace Zip.InstallmentsService.Api.Application.Services.CommandHandlers;

public class CreatePaymentPlanCommandHandler : CommandHandler,
    IRequestHandler<CreatePaymentPlanCommand, ICommandResult<PaymentPlanResponse>>
{
    private readonly IMapper _mapper;
    private readonly IPaymentPlanRepository _paymentPlanRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePaymentPlanCommandHandler
    (IUnitOfWork unitOfWork,
        IPaymentPlanRepository paymentPlanRepository,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _paymentPlanRepository =
            paymentPlanRepository ?? throw new ArgumentNullException(nameof(paymentPlanRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ICommandResult<PaymentPlanResponse>> Handle(CreatePaymentPlanCommand request,
        CancellationToken cancellationToken)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));


        request.PurchaseDate.TryParseIso8601DateTimeToUtc(out var startDate);

        var paymentPlan = new PaymentPlan(request.PurchaseAmount, startDate,
            request.NumberOfInstallments,
            request.Frequency);

        _paymentPlanRepository.Add(paymentPlan);

        // Save
        await _unitOfWork.SaveChangesAsync();

        // Respond
        var successResult = new SuccessCommandResult<PaymentPlanResponse>();
        var result = _mapper.Map<PaymentPlan, PaymentPlanResponse>(paymentPlan);
        successResult.SetResult(result);
        return successResult;
    }
}