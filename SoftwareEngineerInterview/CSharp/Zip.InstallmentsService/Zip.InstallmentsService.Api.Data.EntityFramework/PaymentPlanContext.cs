using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Zip.InstallmentsService.Api.Data.EntityFramework.Configurations;
using Zip.InstallmentsService.Api.Domain.Core;

namespace Zip.InstallmentsService.Api.Data.EntityFramework;

public class PaymentPlanContext : DbContext
{
    public PaymentPlanContext(DbContextOptions<PaymentPlanContext> options)
        : base(options)
    {
    }

    public DbSet<PaymentPlan> PaymentPlans { get; set; }

    internal Expression<Func<T, bool>> CreateDefaultGlobalQueryFilter<T>()
    {
        return e => EF.Property<bool>(e, "IsDeleted") == false;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new PaymentPlanConfiguration(this));
        var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
            v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        foreach (var property in entityType.GetProperties())
            if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                property.SetValueConverter(dateTimeConverter);
    }
}