using HotelFinalAPI.Application.IRepositories;
using HotelFinalAPI.Application.IUnitOfWorks;
using HotelFinalAPI.Domain.Entities.BaseEntities;
using HotelFinalAPI.Persistance.Contexts;
using HotelFinalAPI.Persistance.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
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
        private IDbContextTransaction _transaction;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
        }
        public IReadRepository<TEntity> GetReadRepository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories.ContainsKey(typeof(TEntity)))
            {
                return (IReadRepository<TEntity>)_repositories[typeof(TEntity)];
            }
            var repository = new ReadRepository<TEntity>(_context);
            return repository;
        }

        public IWriteRepository<TEntity> GetWriteRepository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories.ContainsKey(typeof(TEntity)))
            {
                return (IWriteRepository<TEntity>)_repositories[typeof(TEntity)];
            }
            var repository = new WriteRepository<TEntity>(_context);
            return repository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch
            {
                // Handle exception
                await RollbackAsync();
                throw; // Re-throw the exception
            }
            finally
            {
                _transaction.Dispose();
            }
        }

        public async Task RollbackAsync()
        {
            try
            {
                await _transaction.RollbackAsync();
            }
            finally
            {
                _transaction.Dispose();
            }
        }

        /*
         public class UnitOfWork<TEntity> : IUnitOfWork<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private readonly IReadRepository<TEntity> _readRepository;
        private readonly IWriteRepository<TEntity> _writeRepository;

        public UnitOfWork(ApplicationDbContext context, IReadRepository<TEntity> readRepository, IWriteRepository<TEntity> writeRepository)
        {
            _context = context;
            _readRepository = readRepository;
            _writeRepository = writeRepository;
        }

        public IReadRepository<TEntity> GetReadRepository()
        {
            return _readRepository;
        }

        public IWriteRepository<TEntity> GetWriteRepository()
        {
            return _writeRepository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
         */
        //Instead of using a dictionary to store repositories, you can inject the read and write repositories directly into the UnitOfWork constructor. This makes the code simpler and eliminates the need for dictionary lookups.
        //The GetReadRepository and GetWriteRepository methods return the injected repositories directly, without the need for type checks or casting.
    }
}
