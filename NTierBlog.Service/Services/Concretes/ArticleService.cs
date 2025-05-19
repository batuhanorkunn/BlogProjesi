using AutoMapper;
using NTierBlog.Data.UnitOfWorks;
using NTierBlog.Entity.DTOs.Articles;
using NTierBlog.Entity.Entities;
using NTierBlog.Service.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierBlog.Service.Services.Concretes
{
	public class ArticleService : IArticleService
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper mapper;

		public ArticleService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			this.unitOfWork = unitOfWork;
			this.mapper = mapper;
		}

		public async Task CreateArticleAsync(ArticleAddDto articleAddDto)
		{
			var userId = Guid.Parse("FF570217-B6EF-4D7C-A132-17CBCC48A469");
			var imageId = Guid.Parse("F71F4B9A-AA60-461D-B398-DE31001BF214");
			var article = new Article(articleAddDto.Title, articleAddDto.Content, userId, articleAddDto.CategoryId,imageId);
			
			await unitOfWork.GetRepository<Article>().AddAsync(article);
			await unitOfWork.SaveAsync();
		}

		public async Task<List<ArticleDto>> GetAllArticlesWithCategoryNonDeletedAsync()
		{
			var articles = await unitOfWork.GetRepository<Article>().GetAllAsync(x => !x.IsDeleted, x => x.Category);
			var map = mapper.Map<List<ArticleDto>>(articles);
			return map;

		}
		public async Task<ArticleDto> GetArticleWithCategoryNonDeletedAsync(Guid articleId)
		{
			var article = await unitOfWork.GetRepository<Article>().GetAsync(x => !x.IsDeleted && x.Id == articleId, x => x.Category);
			var map = mapper.Map<ArticleDto>(article);
			return map;

		}
		public async Task<string> UpdateArticleAsync(ArticleUpdateDto articleUpdateDto)
		{
			var article = await unitOfWork.GetRepository<Article>().GetAsync(x => !x.IsDeleted && x.Id == articleUpdateDto.Id, x => x.Category);

			string articleTitleBeforeUpdate = article.Title;

			article.Title = articleUpdateDto.Title;
			article.Content = articleUpdateDto.Content;
			article.CategoryId = articleUpdateDto.CategoryId;

			await unitOfWork.GetRepository<Article>().UpdatedAsync(article);
			await unitOfWork.SaveAsync();

			return articleTitleBeforeUpdate; //Toastr için yaptım bunu. Eski başlık adını güncellerken bu değişti diye yazdırmak için.
		}
		public async Task<string> SafeDeleteArticleAsync(Guid articleId)
		{
			var article = await unitOfWork.GetRepository<Article>().GetByGuidAsync(articleId);

			article.IsDeleted = true;
			article.DeletedDate = DateTime.Now;

			await unitOfWork.GetRepository<Article>().UpdatedAsync(article);
			await unitOfWork.SaveAsync();

			return article.Title; 
		}

	}
}
