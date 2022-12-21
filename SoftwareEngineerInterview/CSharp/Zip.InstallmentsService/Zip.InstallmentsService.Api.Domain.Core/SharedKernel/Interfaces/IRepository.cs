using System;
using System.Threading.Tasks;

namespace Zip.InstallmentsService.Api.Domain.Core.SharedKernel.Interfaces;

public interface IRepository<T> where T : Entity
{
    T Find(Guid id);
    Task<T> GetByIdAsync(Guid id);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}