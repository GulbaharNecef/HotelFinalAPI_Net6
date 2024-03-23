using HotelFinalAPI.Application.IRepositories;
using HotelFinalAPI.Domain.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.IUnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        IReadRepository<TEntity> GetReadRepository<TEntity>() where TEntity : BaseEntity;
        IWriteRepository<TEntity> GetWriteRepository<TEntity>() where TEntity : BaseEntity;
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
