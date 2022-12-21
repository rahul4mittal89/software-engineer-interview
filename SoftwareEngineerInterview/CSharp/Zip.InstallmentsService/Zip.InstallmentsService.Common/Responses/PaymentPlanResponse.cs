using System;
using System.Collections.Generic;

namespace Zip.InstallmentsService.Api.Common.Responses;

public class PaymentPlanResponse
{
    #region Public Properties

   /// <summary>
   /// Gets or sets the installments
   /// </summary>
    public List<InstallmentResponse> Installments { get; set; }

    /// <summary>
    /// Get or sets the payment plan Id
    /// </summary>
    public Guid PaymentPlanId { get; set; }

    /// <summary>
    /// Gets or sets the Purchase amount
    /// </summary>
    public decimal PurchaseAmount { get; set; }

    #endregion
}