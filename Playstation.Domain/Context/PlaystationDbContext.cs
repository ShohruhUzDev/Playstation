using Microsoft.EntityFrameworkCore;
using Playstation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playstation.Domain.Context
{
    public class PlaystationDbContext: DbContext
    {
        public PlaystationDbContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Tarrif> Tarrifs { get; set; }
    }
}
