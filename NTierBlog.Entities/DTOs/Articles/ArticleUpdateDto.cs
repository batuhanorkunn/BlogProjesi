﻿using Microsoft.AspNetCore.Http;
using NTierBlog.Entity.DTOs.Categories;
using NTierBlog.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierBlog.Entity.DTOs.Articles
{
	public class ArticleUpdateDto
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public Guid CategoryId { get; set; }
		public Image Image { get; set; }
		public IFormFile? Photo { get; set; }
		public IList<CategoryDto> Categories { get; set; }
	}
}
