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

        public DbSet<Aula146_SalesWebMvc.Models.Department> Department { get; set; }
    }
}
