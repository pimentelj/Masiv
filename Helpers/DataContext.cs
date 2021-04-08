using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Test.Models;
using Test.Entities;

namespace Test.Helpers
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("Connection"));
        }

        public DbSet<User> users { get; set; }
        public DbSet<Roulette> roulettes { get; set; }
        public DbSet<Bet> bets { get; set; }
    }
}