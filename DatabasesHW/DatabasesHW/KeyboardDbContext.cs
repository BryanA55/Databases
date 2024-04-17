using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabasesHW
{
    internal class KeyboardDbContext : DbContext
    {
        public DbSet<Keyboard> Keyboards {  get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-7MPBPGM;Initial Catalog=KeyboardDatabase;Integrated Security=True;Pooling=False;Encrypt=False;Trust Server Certificate=False",
                options => options.EnableRetryOnFailure()); // Had to disable security because I kept getting login error
        }
    }
}
