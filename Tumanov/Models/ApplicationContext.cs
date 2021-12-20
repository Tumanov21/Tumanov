using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Site.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Models
{
    public class ApplicationContext:IdentityDbContext<User>
    {
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext>options):base(options)
        {
            Database.EnsureCreated();
        }
    }
}
