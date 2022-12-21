using System;
using Microsoft.EntityFrameworkCore;

namespace Zip.InstallmentsService.Api.Data.EntityFramework.Common;

public abstract class QueryHandler
{
    protected QueryHandler(PaymentPlanContext context)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
        Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    protected PaymentPlanContext Context { get; }
}