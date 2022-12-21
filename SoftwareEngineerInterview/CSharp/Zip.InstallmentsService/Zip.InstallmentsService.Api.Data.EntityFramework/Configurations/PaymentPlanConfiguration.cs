using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zip.InstallmentsService.Api.Data.EntityFramework.Common;
using Zip.InstallmentsService.Api.Domain.Core;

namespace Zip.InstallmentsService.Api.Data.EntityFramework.Configurations;

public class PaymentPlanConfiguration : EntityConfiguration<PaymentPlan>
{
    public PaymentPlanConfiguration(PaymentPlanContext context) : base(context)
    {
    }

    protected override string SchemaName => "Installments";

    protected override string TableName => "PaymentPlan";

    public override void Configure(EntityTypeBuilder<PaymentPlan> builder)
    {
        base.Configure(builder);

        builder.Ignore(c => c.State);

        builder.Property(c => c.PurchaseAmount)
            .IsRequired();

        builder.OwnsMany(s => s.Installments, b =>
        {
            b.WithOwner().HasForeignKey(s => s.PaymentPlanId);

            b.ToTable("Installment", "Installments");

            b.Property<int>("ClusterId")
                .ValueGeneratedOnAdd();

            b.HasIndex("ClusterId")
                .IsUnique();

            b.Property<int>("ClusterId")
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            b.Property(c => c.PaymentPlanId)
                .HasColumnName("PaymentPlanId")
                .IsRequired();

            b.Property(c => c.Amount)
                .HasColumnName("Amount")
                .IsRequired();

            b.Property(c => c.DueDate)
                .HasColumnName("DueDate")
                .IsRequired()
                .HasConversion(d => DateTime.SpecifyKind(d, DateTimeKind.Utc),
                    d => DateTime.SpecifyKind(d, DateTimeKind.Utc));

            b.HasKey("PaymentPlanId", "Id");

            b.Ignore(s => s.State);
        }).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}