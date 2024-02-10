using HotelFinalAPI.Application.IRepositories;
using HotelFinalAPI.Application.IUnitOfWork;
using HotelFinalAPI.Domain.Entities.BaseEntities;
using HotelFinalAPI.Persistance.Contexts;
using HotelFinalAPI.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Persistance.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private Dictionary<Type, object> _repositories; 
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
        }
        public IRepository<TEntity> GetReadRepository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories.ContainsKey(typeof(TEntity)))
            {
                return (IRepository<TEntity>)_repositories[typeof(TEntity)];
            }
            var repository = new ReadRepository<TEntity>(_context);
            return repository;
        }

        public IRepository<TEntity> GetWriteRepository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories.ContainsKey(typeof(TEntity)))
            {
                return (IRepository<TEntity>)_repositories[typeof(TEntity)];
            }
            var repository = new WriteRepository<TEntity>(_context);
            return repository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
