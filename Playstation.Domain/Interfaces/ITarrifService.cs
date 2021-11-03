﻿using Playstation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playstation.Domain.Interfaces
{
   public interface ITarrifService
    {
        Task<IEnumerable<Tarrif>> GetTarrifs();
        Task<Tarrif> GetByIdTarrif(int id);
        Task<Tarrif> CreateTarrif(Tarrif tarrif);
        Task UpdateTarrif(Tarrif tarrif);
        Task DeleteTarrif(int id);
    }
}
