using HotelFinalAPI.Domain.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.IRepositories
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(bool tracking = true);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true);
        Task<T> GetSingle(Expression<Func<T, bool>> method, bool tracking = true); //werilen serte uygun ilk datani getir
        Task<T> GetByIdAsync(string id, bool tracking = true);
    }
}
