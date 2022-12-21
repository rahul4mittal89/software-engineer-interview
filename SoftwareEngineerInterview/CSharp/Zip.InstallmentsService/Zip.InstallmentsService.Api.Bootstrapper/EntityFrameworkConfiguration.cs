using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Zip.InstallmentsService.Api.Data.EntityFramework;

namespace Zip.InstallmentsService.Api.Bootstrapper;

public static class EntityFrameworkConfiguration
{
    public static void AddEntityFrameworkConfiguration(this IServiceCollection services, string connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentException($"{nameof(connectionString)} cannot be null, empty or contain only whitespace",
                nameof(connectionString));

        services.AddDbContext<PaymentPlanContext>(options =>
            options.UseInMemoryDatabase(
                connectionString).EnableSensitiveDataLogging());
    }
}