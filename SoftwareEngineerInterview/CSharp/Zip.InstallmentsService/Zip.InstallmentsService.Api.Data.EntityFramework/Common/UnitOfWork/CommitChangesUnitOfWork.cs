using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zip.InstallmentsService.Api.Domain.Core.SharedKernel.Interfaces;

namespace Zip.InstallmentsService.Api.Data.EntityFramework.Common.UnitOfWork;

public class CommitChangesUnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;

    public CommitChangesUnitOfWork(DbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}