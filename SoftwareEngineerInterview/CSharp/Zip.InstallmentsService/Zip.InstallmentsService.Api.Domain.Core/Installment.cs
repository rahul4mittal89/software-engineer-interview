using System;
using Zip.InstallmentsService.Api.Domain.Core.SharedKernel;
using Zip.InstallmentsService.Api.Domain.Core.SharedKernel.Interfaces;

namespace Zip.InstallmentsService.Api.Domain.Core;

public class Installment : Entity, IAuditableEntity
{
    #region Constructors

    private Installment()
    {
        State = ObjectState.Unchanged;
    }

    public Installment(PaymentPlan paymentPlan, decimal amount, DateTime dueDate)
    {
        if (paymentPlan == null) throw new ArgumentNullException(nameof(paymentPlan));

        if (amount < 0) throw new ArgumentException($"{nameof(amount)} cannot be less than zero", nameof(amount));

        PaymentPlanId = paymentPlan.Id;
        Amount = amount;
        DueDate = dueDate;
    }

    #endregion

    #region Properties

    /// <summary>
    ///     Gets or sets the amount of the installment.
    /// </summary>
    public decimal Amount { get; }

    /// <summary>
    ///     Gets or sets the date that the installment payment is due.
    /// </summary>
    public DateTime DueDate { get; }

    /// <summary>
    ///     Gets or sets the date that the installment payment is due.
    /// </summary>
    public Guid PaymentPlanId { get; }

    public DateTime CreationTime { get; private set; }
    public DateTime LastUpdated { get; private set; }

    #endregion
}