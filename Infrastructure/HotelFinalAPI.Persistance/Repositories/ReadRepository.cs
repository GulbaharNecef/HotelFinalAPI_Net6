using HotelFinalAPI.Application.Exceptions.CommonExceptions;
using HotelFinalAPI.Application.IRepositories;
using HotelFinalAPI.Domain.Entities.BaseEntities;
using HotelFinalAPI.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Persistance.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;

        public ReadRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll(bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)//eger tracking olunmagi istenilmirse
                query = query.AsNoTracking();
            return query;
        }

        public async Task<T> GetSingle(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return query.FirstOrDefault(method);
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.Where(method);
            if (!tracking)
                query = query.AsNoTracking();
            return query;
        }

        public async Task<T> GetByIdAsync(string id, bool tracking = true)
        {
            bool checkIdFormat = Guid.TryParse(id, out Guid guid);
            if (checkIdFormat)
            {
                var query = Table.AsQueryable();
                if (!tracking)
                    query = query.AsNoTracking();
                return await query.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
            }
            else
                throw new InvalidIdFormatException(id);

        }


        /*
        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll(bool tracking = true)
        => Table; // table ni birbasa return ede bilir, cunki Table DbSet tipindedi, DBSet de IQueryable<> den torenir
       
        public async Task<T> GetSingle(Expression<Func<T, bool>> method, bool tracking = true)
           => await Table.FirstOrDefaultAsync(method);

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
           => Table.Where(method);

        public async Task<T> GetByIdAsync(string id, bool tracking = true)
        //=> await Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
        //=> await Table.FindAsync(Guid.Parse(id));
        {
            bool checkId = Guid.TryParse(id, out Guid guid);
            if (checkId)
            {
                return await Table.FindAsync(guid);
            }
            else
                throw new InvalidIdFormatException(id);
        }
        */
    }
}
