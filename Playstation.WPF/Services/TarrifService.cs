using Microsoft.EntityFrameworkCore;
using Playstation.WPF.Context;
using Playstation.WPF.Interfaces;
using Playstation.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playstation.WPF.Services
{
    public class TarrifService : ITarrifService
    {
        private readonly PlaystationDbContext _dbContext;

        public TarrifService()
        {
            _dbContext = new PlaystationDbContext();
        }
        public async Task<Tarrif> CreateTarrif(Tarrif tarrif)
        {
            using (var dbContext = new PlaystationDbContext())
            {
                dbContext.Add(tarrif);
                await dbContext.SaveChangesAsync();
                return tarrif;
            }

               
        }

        public async Task DeleteTarrif(int id)
        {
            using (var dbContext = new PlaystationDbContext())
            {
                var tarrif = await dbContext.Tarrifs.FirstOrDefaultAsync(x => x.Id == id);
                dbContext.Remove(tarrif);
                await dbContext.SaveChangesAsync();
            }

             
        }

        public async Task<Tarrif> GetByIdTarrif(int id)
        {
            using (var dbContext = new PlaystationDbContext())
            {
                var tarrif = await dbContext.Tarrifs.FirstOrDefaultAsync(x => x.Id == id);
                return tarrif;
            }
               
        }

        public async Task<IEnumerable<Tarrif>> GetTarrifs()
        {
            using(var dbContext=new PlaystationDbContext())
            {
                var tarrifs = await dbContext.Tarrifs.ToListAsync();
                return tarrifs;
            }
           
        }

        public async Task UpdateTarrif(Tarrif tarrif)
        {
            using( var dbContext=new PlaystationDbContext())
            {
                dbContext.Entry(tarrif).Property(p => p.Title).IsModified = true;
                dbContext.Entry(tarrif).Property(p => p.Amount).IsModified = true;
                dbContext.Entry(tarrif).Property(p => p.TotalMinutes).IsModified = true;

                
               
                await dbContext.SaveChangesAsync();
            }
           
        }
    }
}
