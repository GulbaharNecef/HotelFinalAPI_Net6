﻿using HotelFinalAPI.Domain.Entities.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.IRepositories.IReservationRepos
{
    public interface IReservationReadRepository : IReadRepository<Reservation>
    {
    }
}
