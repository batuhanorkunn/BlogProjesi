using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NTierBlog.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierBlog.Data.Mappings
{
	public class RoleMap : IEntityTypeConfiguration<AppRole>
	{
		public void Configure(EntityTypeBuilder<AppRole> builder)
		{
			// Primary key
			builder.HasKey(r => r.Id);

			// Index for "normalized" role name to allow efficient lookups
			builder.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();

			// Maps to the AspNetRoles table
			builder.ToTable("AspNetRoles");

			// A concurrency token for use with the optimistic concurrency checking
			builder.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

			// Limit the size of columns to use efficient database types
			builder.Property(u => u.Name).HasMaxLength(256);
			builder.Property(u => u.NormalizedName).HasMaxLength(256);

			// The relationships between Role and other entity types
			// Note that these relationships are configured with no navigation properties

			// Each Role can have many entries in the UserRole join table
			builder.HasMany<AppUserRole>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();

			// Each Role can have many associated RoleClaims
			builder.HasMany<AppRoleClaim>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();

			builder.HasData(new AppRole
			{
				Id = Guid.Parse("47F6DF48-1750-4699-8F98-166BA7473280"),
				Name="Superadmin",
				NormalizedName = "SUPERADMIN",
				ConcurrencyStamp = Guid.NewGuid().ToString()
			},new AppRole
			{
				Id= Guid.Parse("EC4D48D8-D5D8-4712-9A62-3D126EE4F898"),
				Name="Admin",
				NormalizedName = "ADMIN",
				ConcurrencyStamp = Guid.NewGuid().ToString()
			},new AppRole
			{
				Id=Guid.Parse("1D0C4254-3174-450C-A7D9-CE6BE2E0D0FA"),
				Name= "User",
				NormalizedName = "USER",
				ConcurrencyStamp = Guid.NewGuid().ToString()
			});
		}
	}
}
