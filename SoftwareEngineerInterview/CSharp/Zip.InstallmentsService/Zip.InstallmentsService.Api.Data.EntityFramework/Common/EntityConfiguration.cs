using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zip.InstallmentsService.Api.Domain.Core.SharedKernel;
using Zip.InstallmentsService.Api.Domain.Core.SharedKernel.Interfaces;

namespace Zip.InstallmentsService.Api.Data.EntityFramework.Common;

public abstract class EntityConfiguration<T> : IEntityTypeConfiguration<T>
    where T : Entity
{
    protected EntityConfiguration(PaymentPlanContext context)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
    }

    protected PaymentPlanContext Context { get; }
    protected abstract string SchemaName { get; }
    protected abstract string TableName { get; }

    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        if (string.IsNullOrWhiteSpace(SchemaName))
            throw new InvalidOperationException($"{nameof(SchemaName)} must be set.");

        if (string.IsNullOrWhiteSpace(TableName))
            throw new InvalidOperationException($"{nameof(TableName)} must be set.");

        builder.ToTable(TableName, SchemaName);

        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property<int>("ClusterId").ValueGeneratedOnAdd();

        builder.HasIndex("ClusterId")
            .HasDatabaseName($"IX_{TableName}_ClusterId")
            .IsUnique();

        builder.Property<int>("ClusterId")
            .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

        builder.HasKey(e => e.Id);

        builder.Property<bool>("IsDeleted");
        builder.HasIndex("IsDeleted")
            .HasDatabaseName($"IX_{TableName}_IsDeleted");

        builder.Ignore(e => e.State);

        builder.HasQueryFilter(Context.CreateDefaultGlobalQueryFilter<T>());

        if (typeof(ICreationAuditable).IsAssignableFrom(builder.Metadata.ClrType))
            builder.Property<DateTime>(nameof(ICreationAuditable.CreationTime))
                .HasColumnName("CreationTime")
                .IsRequired()
                .HasDefaultValueSql("GetUtcDate()")
                .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

        if (typeof(IUpdateAuditable).IsAssignableFrom(builder.Metadata.ClrType))
            builder.Property<DateTime>(nameof(IUpdateAuditable.LastUpdated))
                .HasColumnName("LastUpdated")
                .IsRequired()
                .HasDefaultValueSql("GetUtcDate()")
                .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
    }
}