using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using BadBroker.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;

namespace BadBroker.DataAccess
{
    public class BadBrokerContext : DbContext
    {
        public DbSet<QuotesData> QuotesData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=IT-REZERV\SQLEXPRESS01;Database=badbrokerdb;Trusted_Connection=True;");
        }
    }
}
