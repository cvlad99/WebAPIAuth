using BagamIdentity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BagamIdentity.Data
{
    public class DataContext : DbContext
    {
        private readonly ILoggerFactory loggerFactory = LoggerFactory.Create(config => config.AddConsole());
        public DbSet<User> Users { get; set; }
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(loggerFactory);
        }
    }
}
