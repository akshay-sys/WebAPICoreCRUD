using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCoreCURD.Models
{
    public class InventDbContext: DbContext
    {
        public InventDbContext(DbContextOptions<InventDbContext> options) : base(options)
        {
        }
        public DbSet<Products> products { get; set; }
        public DbSet<UserInfo> userInfos { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source= . ; Initial Catalog=InventDb; Trusted_Connection=True;");
        }*/
    }
}
