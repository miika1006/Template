using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Template.Core.Item;

namespace Template.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {

        }
        public ApplicationDbContext(string connectionString) : base(new DbContextOptionsBuilder<ApplicationDbContext>().UseNpgsql(connectionString).Options){}

        public DbSet<Item> Items => Set<Item>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}