using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NTierBlog.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NTierBlog.Data.Mappings
{
	public class UserRoleMap : IEntityTypeConfiguration<AppUserRole>
	{
		public void Configure(EntityTypeBuilder<AppUserRole> builder)
		{
			// Primary key
			builder.HasKey(r => new { r.UserId, r.RoleId });

			// Maps to the AspNetUserRoles table
			builder.ToTable("AspNetUserRoles");

			builder.HasData(new AppUserRole
			{
				UserId = Guid.Parse("FF570217-B6EF-4D7C-A132-17CBCC48A469"),
				RoleId = Guid.Parse("47F6DF48-1750-4699-8F98-166BA7473280")
			},new AppUserRole
			{
				UserId = Guid.Parse("02DE2D9C-D3E4-4BAA-B374-8F4D49265596"),
				RoleId = Guid.Parse("EC4D48D8-D5D8-4712-9A62-3D126EE4F898")
			});
		}
	}
}
