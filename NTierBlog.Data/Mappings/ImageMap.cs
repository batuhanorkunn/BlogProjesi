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
	public class ImageMap : IEntityTypeConfiguration<Image>
	{
		public void Configure(EntityTypeBuilder<Image> builder)
		{
			builder.HasData(new Image
			{
				Id = Guid.Parse("F71F4B9A-AA60-461D-B398-DE31001BF214"),
				FileName = "images/testimage",
				FileType = "jpg",
				CreatedBy = "Admin Test",
				CreatedDate = DateTime.Now,
				IsDeleted = false
			},
			new Image
			{
				Id = Guid.Parse("D16A6EC7-8C50-4AB0-89A5-02B9A551F0FA"),
				FileName = "images/vstest",
				FileType = "png",
				CreatedBy = "Admin Test",
				CreatedDate = DateTime.Now,
				IsDeleted = false
			});
		}
	}
}
