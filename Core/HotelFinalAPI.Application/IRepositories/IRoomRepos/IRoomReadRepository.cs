﻿using HotelFinalAPI.Application.Helpers;
using HotelFinalAPI.Domain.Entities.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.IRepositories.IRoomRepos
{
    public interface IRoomReadRepository : IReadRepository<Room>
    {
        IQueryable<Room> GetFiltered(QueryObjectRoom query,bool tracking = true);
    }
}
