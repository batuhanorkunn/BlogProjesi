using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NTierBlog.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NTierBlog.Data.Context
{
	public class AppDbContext : DbContext
	{
		public AppDbContext()
		{
			
		}
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		public DbSet<Article> Articles { get; set; }
		public DbSet<Category> Categories{ get; set; }
		public DbSet<Image> Images{ get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			// PendingModelChangesWarning uyarısını bastır
			optionsBuilder.ConfigureWarnings(warnings =>
				warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}

	}
}
