using MediatR;
using Zip.InstallmentsService.Api.Common.Common;
using Zip.InstallmentsService.Api.Common.Responses;

namespace Zip.InstallmentsService.Api.Common.Commands;

public class CreatePaymentPlanCommand : IRequest<ICommandResult<PaymentPlanResponse>>
{
    #region Public Properties

    /// <summary>
    ///     Gets or sets the frequency
    /// </summary>
    public int Frequency { get; set; }

    /// <summary>
    ///     Gets or sets the number of installments
    /// </summary>
    public int NumberOfInstallments { get; set; }

    /// <summary>
    ///     Gets or sets the purchase amount
    /// </summary>
    public decimal PurchaseAmount { get; set; }

    /// <summary>
    ///     Gets or sets the purchase date
    /// </summary>
    public string PurchaseDate { get; set; }

    #endregion
}