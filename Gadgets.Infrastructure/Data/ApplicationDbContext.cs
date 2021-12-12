using Gadgets.Application.Contracts.Data;
using Gadgets.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gadgets.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Weather>(
                w => {
                    w.HasKey(p => new { p.Query, p.RequestTime });
                }
            );
            base.OnModelCreating(builder);
        }

        public DbSet<Gadget> Gadgets { get; set; }

        public DbSet<Weather> Weather { get; set; }

        public void Guardar()
        {
            base.SaveChanges();
        }
    }
}
