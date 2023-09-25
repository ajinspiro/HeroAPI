using HeroAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HeroAPI.DataAccess
{
    public class HeroDbContext : DbContext
    {
        public HeroDbContext(DbContextOptions<HeroDbContext> options) : base(options)
        {

        }

        public DbSet<Hero> Heroes { get; set; }
    }
}
