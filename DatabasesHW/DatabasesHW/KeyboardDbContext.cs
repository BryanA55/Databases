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
            optionsBuilder.UseSqlServer("Server=localhost\\MSSQLSERVER01;Database=KeyboardDatabase;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;",
                options => options.EnableRetryOnFailure());
        }

    }
}
