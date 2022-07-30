using Microsoft.EntityFrameworkCore;
using Playstation.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playstation.WPF.Context
{
    public class PlaystationDbContext: DbContext
    {
        //public PlaystationDbContext(DbContextOptions<PlaystationDbContext> options):base(options)
        //{
        //    Database.EnsureCreated();
        //}
        public DbSet<Order> Orders { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Tarrif> Tarrifs { get; set; }

        private const string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=plays;Trusted_Connection=True;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseMySql("Server = localhost; port = 3306; Database = SparTestDb; Uid = root; Pwd = root ", new MySqlServerVersion(new Version(8, 0, 18)), enable =>enable.EnableRetryOnFailure());

            optionsBuilder.UseSqlServer(@"Server =(localdb)\MSSQLLocalDB; Database = plays; Trusted_Connection = True; ");
            //, new MySqlServerVersion(new Version(5, 0, 2))
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False

        //    optionsBuilder.UseMySql("Server = 127.0.0.1; port = 3306; Database = SparTestDb; uid = root; password = root" providerName="MySql.Data.MySqlClient");


        //}
    }
}
