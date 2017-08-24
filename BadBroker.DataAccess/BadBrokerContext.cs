using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BadBroker.DataAccess.Entities;

namespace BadBroker.DataAccess
{
    public class BadBrokerContext : DbContext
    {
        public DbSet<RatesPerDate> RatesPerDate { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseSqlServer($"Server=DESKTOP-79N4298\\SQLEXPRESS;Database=BadBrokerDB;Trusted_Connection=True;");
    }
}
