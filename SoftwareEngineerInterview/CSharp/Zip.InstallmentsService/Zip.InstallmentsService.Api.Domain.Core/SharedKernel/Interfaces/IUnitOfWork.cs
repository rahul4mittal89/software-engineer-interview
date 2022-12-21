using System.Threading.Tasks;

namespace Zip.InstallmentsService.Api.Domain.Core.SharedKernel.Interfaces;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}