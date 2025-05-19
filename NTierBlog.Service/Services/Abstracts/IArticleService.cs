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
		Task<List<ArticleDto>> GetAllArticlesWithCategoryNonDeletedAsync();
		Task<ArticleDto> GetArticleWithCategoryNonDeletedAsync(Guid articleId);
		Task<string> UpdateArticleAsync(ArticleUpdateDto articleUpdateDto);
		Task<string> SafeDeleteArticleAsync(Guid articleId);
		Task CreateArticleAsync(ArticleAddDto articleAddDto);
	}
}
