using Microsoft.Extensions.DependencyInjection;
using Zip.InstallmentsService.Api.Data.EntityFramework;
using Zip.InstallmentsService.Api.Data.EntityFramework.Common.UnitOfWork;
using Zip.InstallmentsService.Api.Domain.Core.SharedKernel.Interfaces;

namespace Zip.InstallmentsService.Api.Bootstrapper;

public static class UnitOfWorkConfiguration
{
    public static void ConfigureUnitOfWork(this IServiceCollection services)
    {
        services.AddTransient<IUnitOfWork>(serviceProvider =>
        {
            var crmContext = serviceProvider.GetRequiredService<PaymentPlanContext>();
            return new CommitChangesUnitOfWork(crmContext);
        });
    }
}