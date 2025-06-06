﻿using NTierBlog.Entity.DTOs.Categories;
using NTierBlog.Entity.Entities;

namespace NTierBlog.Entity.DTOs.Articles
{
	public class ArticleDto
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public CategoryDto Category { get; set; }
		public  DateTime CreatedDate { get; set; } 
		public Image Image { get; set; }
		public string CreatedBy { get; set; }
		public bool IsDeleted { get; set; }
	}
}
