using Microsoft.EntityFrameworkCore;
using Playstation.Domain.Context;
using Playstation.Domain.Interfaces;
using Playstation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playstation.Domain.Services
{
    public class TarrifService : ITarrifService
    {
        private readonly PlaystationDbContext _dbContext;

        public TarrifService(PlaystationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Tarrif> CreateTarrif(Tarrif tarrif)
        {
            _dbContext.Add(tarrif);
            await  _dbContext.SaveChangesAsync();
            return tarrif;
        }

        public async Task DeleteTarrif(int id)
        {
            var tarrif = await _dbContext.Tarrifs.FirstOrDefaultAsync(x => x.Id == id);
            _dbContext.Remove(tarrif);
           await _dbContext.SaveChangesAsync();
        }

        public async Task<Tarrif> GetByIdTarrif(int id)
        {
            var tarrif = await _dbContext.Tarrifs.FirstOrDefaultAsync(x => x.Id == id);
            return tarrif;
        }

        public async Task<IEnumerable<Tarrif>> GetTarrifs()
        {
            var tarrifs = await _dbContext.Tarrifs.ToListAsync();
            return tarrifs;
        }

        public async Task UpdateTarrif(Tarrif tarrif)
        {
            _dbContext.Update(tarrif);
           await _dbContext.SaveChangesAsync();
        }
    }
}
