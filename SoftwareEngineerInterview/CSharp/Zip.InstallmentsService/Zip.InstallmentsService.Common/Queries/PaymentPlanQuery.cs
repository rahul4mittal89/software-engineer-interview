using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zip.InstallmentsService.Api.Common.Responses;

namespace Zip.InstallmentsService.Api.Common.Queries;

public class PaymentPlanQuery : IRequest<PaymentPlanResponse>
{
    [FromRoute] public Guid PaymentPlanId { get; set; }
}