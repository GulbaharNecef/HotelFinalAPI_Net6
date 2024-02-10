using HotelFinalAPI.Application.IRepositories;
using HotelFinalAPI.Domain.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.IUnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> GetReadRepository<TEntity>() where TEntity : BaseEntity;
        IRepository<TEntity> GetWriteRepository<TEntity>() where TEntity : BaseEntity;
        Task<int> SaveChangesAsync();
    }
}
