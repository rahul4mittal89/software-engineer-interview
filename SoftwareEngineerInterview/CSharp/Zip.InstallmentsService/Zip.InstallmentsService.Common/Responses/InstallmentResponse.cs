using System;

namespace Zip.InstallmentsService.Api.Common.Responses;

public class InstallmentResponse
{
    #region Public Properties

    /// <summary>
    ///     Gets or sets the installment amount
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    ///     Gets or sets the due date in MMM dd, yyyy
    /// </summary>
    public string DueDate { get; set; }

    /// <summary>
    ///     Gets or sets the installment Id
    /// </summary>
    public Guid InstallmentId { get; set; }

    #endregion
}