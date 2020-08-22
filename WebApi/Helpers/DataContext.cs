using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApi.Entities;
using System.Collections.Generic;

namespace WebApi.Helpers
{
    public class DataContext : DbContext
    {
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Rate> Rates { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }
        public virtual DbSet<WorkItem> WorkItems { get; set; }

        public virtual DbSet<Account> Accounts { get; set; }
        private readonly IConfiguration Configuration;
        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Unit testing stub
        public DataContext()
        {
            if (!IsRunningFromXUnit)
                throw new Exception("DataContext initiated without parameters outside unit tests");
        }

        private static readonly bool IsRunningFromXUnit = _isRunningFromXUnit();
        private static bool _isRunningFromXUnit()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var item in assemblies)
            {
                if (item.FullName.ToLowerInvariant().StartsWith("testhost"))
                    return true;
            }
            return false;
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