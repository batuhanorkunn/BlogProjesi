using NTierBlog.Entity.DTOs.Articles;
using NTierBlog.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierBlog.Service.Services.Abstracts
{
	public interface IArticleService
	{
		Task<List<ArticleDto>> GetAllArticlesAsync();
	}
}
