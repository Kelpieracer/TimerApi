using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApi.Entities;

namespace WebApi.Helpers
{
    public class DataContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        private readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var dbConnectionString = Configuration.GetConnectionString("WebApiDatabase");
            var dbEnvironmentVariableName = Environment.GetEnvironmentVariable("ConnectionStrings__WebApiDatabase");

            // connect to sqlite database
            if (dbEnvironmentVariableName == null)
            {
                options.UseSqlite(dbConnectionString);
                return;
            }

            // connect to sql server
            options.UseSqlServer(dbConnectionString);
        }
    }
}