using System;
using System.Collections.Generic;
using System.Linq;
using Zip.InstallmentsService.Api.Domain.Core.SharedKernel;
using Zip.InstallmentsService.Api.Domain.Core.SharedKernel.Interfaces;

namespace Zip.InstallmentsService.Api.Domain.Core;

public class PaymentPlan : Entity, IAuditableEntity
{
    #region Methods

    public void CreatePaymentPlan(DateTime purchaseDate, int numberOfInstallments, int frequencyDays)
    {
        if (PurchaseAmount <= 1)
            throw new ArgumentException("Payment plan purchase amount should be greater than or equal to 1.",
                nameof(PurchaseAmount));

        if (numberOfInstallments == 0) throw new ArgumentException($"{nameof(numberOfInstallments)} should have value");

        if (frequencyDays == 0) throw new ArgumentException($"{nameof(frequencyDays)} should have value");

        var installmentAmount = Math.Round(PurchaseAmount / numberOfInstallments, 2);

        Installments = Enumerable.Range(0, numberOfInstallments).Select(i =>
            new Installment(this, installmentAmount, purchaseDate.AddDays(i * frequencyDays))).ToList();
    }

    #endregion

    #region Constructor

    protected PaymentPlan()
    {
        State = ObjectState.Unchanged;
        Installments = new List<Installment>();
    }

    public PaymentPlan(decimal purchaseAmount, DateTime purchaseDate, int numberOfInstallments, int frequencyDays)
    {
        if (purchaseAmount < 0)
            throw new ArgumentException($"{nameof(purchaseAmount)} cannot be less than zero", nameof(purchaseAmount));

        PurchaseAmount = purchaseAmount;
        CreatePaymentPlan(purchaseDate, numberOfInstallments, frequencyDays);
    }

    #endregion

    #region Properties

    public List<Installment> Installments { get; private set; }
    public decimal PurchaseAmount { get; private set; }
    public DateTime CreationTime { get; private set; }
    public DateTime LastUpdated { get; private set; }

    #endregion
}