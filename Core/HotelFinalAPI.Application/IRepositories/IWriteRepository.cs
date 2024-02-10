using HotelFinalAPI.Domain.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.IRepositories
{
    public interface IWriteRepository<T> :IRepository<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T model);
        Task<bool> AddRangeAsync(List<T> models); //(IEnumerable<T> items);
        Task<bool> RemoveByIdAsync(string id);
        bool Remove(T model);
        bool Update(T model);
        Task<int> SaveAsync();
        bool RemoveRange(List<T> models);

    }
}
