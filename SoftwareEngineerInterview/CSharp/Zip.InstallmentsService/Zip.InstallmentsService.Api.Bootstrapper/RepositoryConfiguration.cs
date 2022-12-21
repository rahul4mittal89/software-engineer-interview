using Microsoft.Extensions.DependencyInjection;
using Zip.InstallmentsService.Api.Data.EntityFramework;
using Zip.InstallmentsService.Api.Data.EntityFramework.Common;

namespace Zip.InstallmentsService.Api.Bootstrapper;

public static class RepositoryConfiguration
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblyOf<PaymentPlanContext>()
            .AddClasses(classes => classes.AssignableTo(typeof(Repository<>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime());
    }
}