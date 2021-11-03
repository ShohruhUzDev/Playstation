using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playstation.Domain.Context
{
   public  class PlaystationDbContextFactory
    {
        private readonly Action<DbContextOptionsBuilder> _configureDbContext;

        public PlaystationDbContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
        {
            _configureDbContext = configureDbContext;
        }

        public PlaystationDbContext CreateDbContext()
        {
            DbContextOptionsBuilder<PlaystationDbContext> options = new DbContextOptionsBuilder<PlaystationDbContext>();
            _configureDbContext(options);
            return new PlaystationDbContext(options.Options);


        }

    }
}
