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

			var article = new Article
			{
				Title = articleAddDto.Title,
				Content = articleAddDto.Content,
				CategoryId = articleAddDto.CategoryId,
				UserId = userId
			};
			await unitOfWork.GetRepository<Article>().AddAsync(article);
			await unitOfWork.SaveAsync();
		}

		public async Task<List<ArticleDto>> GetAllArticlesWithCategoryNonDeletedAsync()
		{
			var articles = await unitOfWork.GetRepository<Article>().GetAllAsync(x => !x.IsDeleted, x => x.Category);
			var map = mapper.Map<List<ArticleDto>>(articles);
			return map;

		}
	}
}
