﻿
using System.Data.Entity;
 
namespace shedule.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DefaultConnection")
        {
        }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<MinRab> MinRab { get; set; }
        public DbSet<DBShop> DBShops { get; set; }

        public DbSet<Forecast> Forecasts { get; set; }
    }
}