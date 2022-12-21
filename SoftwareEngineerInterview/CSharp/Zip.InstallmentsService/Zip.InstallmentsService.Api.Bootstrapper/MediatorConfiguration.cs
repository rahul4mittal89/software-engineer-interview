using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Zip.InstallmentsService.Api.Application.Services.Common;
using Zip.InstallmentsService.Api.Data.EntityFramework.Common;

namespace Zip.InstallmentsService.Api.Bootstrapper;

public static class MediatorConfiguration
{
    public static void ConfigureMediator(this IServiceCollection services)
    {
        services.AddMediatR(typeof(QueryHandler));
        services.AddMediatR(typeof(CommandHandler));
    }
}