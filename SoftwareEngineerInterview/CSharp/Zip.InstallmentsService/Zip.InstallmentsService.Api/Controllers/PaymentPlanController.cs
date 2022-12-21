using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Zip.InstallmentsService.Api.Common;
using Zip.InstallmentsService.Api.Common.Commands;
using Zip.InstallmentsService.Api.Common.Common;
using Zip.InstallmentsService.Api.Common.Queries;
using Zip.InstallmentsService.Api.Common.Responses;

namespace Zip.InstallmentsService.Api.Controllers;

/// <summary>
///     Produces Api for creating installment plan and getting installment plan
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/payment-plan-api")]
[ApiVersion("1.0")]
[Produces("application/json")]
public class PaymentPlanController : ControllerBase
{
    #region Constructor

    public PaymentPlanController(IMediator mediator, ILogger<PaymentPlanController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    #endregion

    #region Private Variables

    private readonly ILogger<PaymentPlanController> _logger;
    private readonly IMediator _mediator;

    #endregion

    #region Public Methods
    /// <summary>
    /// Endpoint to Create Payment Plan
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [SwaggerOperation(Tags = new[] { "PaymentPlan/Installments" })]
    [HttpPost(Name = RouteNames.CreateInstallmentPaymentPlan)]
    [ProducesResponseType(typeof(PaymentPlanResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreatePaymentPlanAsync([FromBody] CreatePaymentPlanCommand command)
    {

        if (command == null) return BadRequest();

        if (!ModelState.IsValid) return UnprocessableEntity(ModelState);

        var commandResult = await _mediator.Send(command);

        if (commandResult.ResultType == CommandResultType.UnprocessableEntity)
        {
            ModelState.AddModelErrors(commandResult.Errors);
            return UnprocessableEntity(ModelState);
        }

        if (commandResult.ResultType == CommandResultType.Conflict)
        {
            ModelState.AddModelErrors(commandResult.Errors);
            return Conflict(ModelState);
        }

        return CreatedAtRoute(
            RouteNames.GetInstallmentPaymentPlanById,
            new PaymentPlanQuery { PaymentPlanId = commandResult.Result.PaymentPlanId },
            commandResult.Result);
    }

    /// <summary>
    /// Endpoint to get payment plan by payment plan ID.
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [SwaggerOperation(Tags = new[] { "PaymentPlan/Installments" })]
    [ProducesResponseType(typeof(PaymentPlanResponse), StatusCodes.Status200OK)]
    [HttpGet("payment-plan/{paymentPlanId}", Name = RouteNames.GetInstallmentPaymentPlanById)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPaymentPlanByIdAsync([FromRoute] PaymentPlanQuery query)
    {
        if (!ModelState.IsValid) return BadRequest();

        var paymentPlanResponse = await _mediator.Send(query);

        if (paymentPlanResponse == null) return NotFound();

        return Ok(paymentPlanResponse);
    }

    #endregion
}