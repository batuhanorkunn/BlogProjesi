﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NTierBlog.Entity.Entities;

namespace NTierBlog.Data.Mappings
{
	public class ArticleMap : IEntityTypeConfiguration<Article>
	{
		public void Configure(EntityTypeBuilder<Article> builder)
		{
			builder.HasData(new Article
			{
				Id = Guid.NewGuid(),
				Title = "Asp.net Core Deneme Makalesi 1",
				Content = "Asp.net Core Sed porttitor lectus nibh. Nulla porttitor accumsan tincidunt. Vivamus suscipit tortor eget felis porttitor volutpat. Praesent sapien massa, convallis a pellentesque nec, egestas non nisi. Sed porttitor lectus nibh. Nulla porttitor accumsan tincidunt. Proin eget tortor risus. Donec rutrum congue leo eget malesuada. Curabitur non nulla sit amet nisl tempus convallis quis ac lectus. Sed porttitor lectus nibh. Curabitur aliquet quam id dui posuere blandit. Mauris blandit aliquet elit, eget tincidunt nibh pulvinar a. Curabitur aliquet quam id dui posuere blandit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla porttitor accumsan tincidunt. Pellentesque in ipsum id orci porta dapibus. Vivamus magna justo, lacinia eget consectetur sed, convallis at tellus. Praesent sapien massa, convallis a pellentesque nec, egestas non nisi.",
				ViewCount = 15,
				CategoryId = Guid.Parse("4C569A9A-5F41-478F-9D17-69AC5B02AE0B"),
				ImageId = Guid.Parse("F71F4B9A-AA60-461D-B398-DE31001BF214"),
				CreatedBy = "Admin Test",
				CreatedDate = DateTime.Now,
				IsDeleted = false,
				UserId = Guid.Parse("FF570217-B6EF-4D7C-A132-17CBCC48A469")

			},
			new Article
			{
				Id = Guid.NewGuid(),
				Title = "Visual Studio Deneme Makalesi 1",
				Content = "Visual Studio Sed porttitor lectus nibh. Nulla porttitor accumsan tincidunt. Vivamus suscipit tortor eget felis porttitor volutpat. Praesent sapien massa, convallis a pellentesque nec, egestas non nisi. Sed porttitor lectus nibh. Nulla porttitor accumsan tincidunt. Proin eget tortor risus. Donec rutrum congue leo eget malesuada. Curabitur non nulla sit amet nisl tempus convallis quis ac lectus. Sed porttitor lectus nibh. Curabitur aliquet quam id dui posuere blandit. Mauris blandit aliquet elit, eget tincidunt nibh pulvinar a. Curabitur aliquet quam id dui posuere blandit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla porttitor accumsan tincidunt. Pellentesque in ipsum id orci porta dapibus. Vivamus magna justo, lacinia eget consectetur sed, convallis at tellus. Praesent sapien massa, convallis a pellentesque nec, egestas non nisi.",
				ViewCount = 15,
				CategoryId = Guid.Parse("D23E4F79-9600-4B5E-B3E9-756CDCACD2B1"),
				ImageId = Guid.Parse("D16A6EC7-8C50-4AB0-89A5-02B9A551F0FA"),
				CreatedBy = "Admin Test",
				CreatedDate = DateTime.Now,
				IsDeleted = false,
				UserId = Guid.Parse("02DE2D9C-D3E4-4BAA-B374-8F4D49265596")

			});
		}
	}
}