using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMS
{
    internal class MyDbContext : DbContext
    {
        public DbSet<Dog> Dogs { get; set; }
        public DbSet<Toy> Toys { get; set; }
        public DbSet<Park> Parks { get; set; }
        public DbSet<DogParkVisits> DogParkVisits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Utils.CONNECTION_STRING);
        }
    }
}
