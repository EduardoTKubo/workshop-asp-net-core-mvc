using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Aula146_SalesWebMvc.Models
{
    public class Aula146_SalesWebMvcContext : DbContext
    {
        public Aula146_SalesWebMvcContext (DbContextOptions<Aula146_SalesWebMvcContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Department { get; set; }
        public DbSet<Seller> Seller { get; set; }
        public DbSet<SalesRecord> SalesRecord { get; set; }
    }
}
