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
        public BadBrokerContext(DbContextOptions<BadBrokerContext> options) : base(options) { }

        public DbSet<RatesData> RatesData { get; set; }
        public  DbSet<RatesPerDate> RatesPerDate { get; set; }
    }
}
