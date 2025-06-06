﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NTierBlog.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierBlog.Data.Mappings
{
	public class UserClaimMap : IEntityTypeConfiguration<AppUserClaim>
	{
		public void Configure(EntityTypeBuilder<AppUserClaim> builder)
		{
			// Primary key
			builder.HasKey(uc => uc.Id);

			// Maps to the AspNetUserClaims table
			builder.ToTable("AspNetUserClaims");
		}
	}
}
