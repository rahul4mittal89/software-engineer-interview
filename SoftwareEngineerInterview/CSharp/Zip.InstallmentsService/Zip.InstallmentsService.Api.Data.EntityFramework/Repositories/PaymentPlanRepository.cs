using System.Linq;
using Zip.InstallmentsService.Api.Data.EntityFramework.Common;
using Zip.InstallmentsService.Api.Domain.Core;
using Zip.InstallmentsService.Api.Domain.Core.Interfaces.Repositories;

namespace Zip.InstallmentsService.Api.Data.EntityFramework.Repositories;

public class PaymentPlanRepository : Repository<PaymentPlan>, IPaymentPlanRepository
{
    public PaymentPlanRepository(PaymentPlanContext context) : base(context)
    {
    }

    protected override IQueryable<PaymentPlan> IncludeAll()
    {
        return DbSet;
    }
}