using Appeals.Application.Interfaces;
using Appeals.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appeals.Persistance
{
    public sealed class AppealsDbContext : DbContext, IAppealsDbContext
    {
        public DbSet<Appeal> Appeals { get; set; }

        public AppealsDbContext(DbContextOptions<AppealsDbContext> options)
            : base(options){ }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AppealConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
