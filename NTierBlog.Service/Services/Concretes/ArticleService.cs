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
		public async Task<List<ArticleDto>> GetAllArticlesAsync()
		{
			var articles = await unitOfWork.GetRepository<Article>().GetAllAsync();
			var map = mapper.Map<List<ArticleDto>>(articles);
			return map;

		}
	}
}
