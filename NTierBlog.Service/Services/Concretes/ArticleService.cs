using AutoMapper;
using Microsoft.AspNetCore.Http;
using NTierBlog.Data.UnitOfWorks;
using NTierBlog.Entity.DTOs.Articles;
using NTierBlog.Entity.Entities;
using NTierBlog.Entity.Enums;
using NTierBlog.Service.Extensions;
using NTierBlog.Service.Helpers.Images;
using NTierBlog.Service.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NTierBlog.Service.Services.Concretes
{
	public class ArticleService : IArticleService
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper mapper;
		private readonly IHttpContextAccessor httpContextAccessor;
		private readonly IImageHelper imageHelper;
		private readonly ClaimsPrincipal _user;

		public ArticleService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, IImageHelper imageHelper)
		{
			this.unitOfWork = unitOfWork;
			this.mapper = mapper;
			this.httpContextAccessor = httpContextAccessor;
			_user = httpContextAccessor.HttpContext.User;
			this.imageHelper = imageHelper;
	
		}

		public async Task CreateArticleAsync(ArticleAddDto articleAddDto)
		{
			//var userId = Guid.Parse("FF570217-B6EF-4D7C-A132-17CBCC48A469");

			var userId = _user.GetLoggedInUserId();
			var userEmail = _user.GetLoggedInEmail();

			var imageUpload = await imageHelper.Upload(articleAddDto.Title, articleAddDto.Photo, ImageType.Post);
			Image image = new(imageUpload.FullName,articleAddDto.Photo.ContentType,userEmail);
			await unitOfWork.GetRepository<Image>().AddAsync(image);


			var article = new Article(articleAddDto.Title, articleAddDto.Content, userId, userEmail, articleAddDto.CategoryId, image.Id);

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
			var article = await unitOfWork.GetRepository<Article>().GetAsync(x => !x.IsDeleted && x.Id == articleId, x => x.Category, i => i.Image);
			var map = mapper.Map<ArticleDto>(article);
			return map;

		}
		public async Task<string> UpdateArticleAsync(ArticleUpdateDto articleUpdateDto)
		{
			var userEmail = _user.GetLoggedInEmail();
			var article = await unitOfWork.GetRepository<Article>().GetAsync(x => !x.IsDeleted && x.Id == articleUpdateDto.Id, x => x.Category, i => i.Image);

			if (articleUpdateDto.Photo != null)
			{
				imageHelper.Delete(article.Image.FileName);

				var imageUpload = await imageHelper.Upload(articleUpdateDto.Title, articleUpdateDto.Photo, ImageType.Post);
				Image image = new(imageUpload.FullName, articleUpdateDto.Photo.ContentType, userEmail);
				await unitOfWork.GetRepository<Image>().AddAsync(image);

				article.ImageId=  image.Id; // Güncellenen resmin Id'sini atıyoruz.
			}

			string articleTitleBeforeUpdate = article.Title;

			article.Title = articleUpdateDto.Title;
			article.Content = articleUpdateDto.Content;
			article.CategoryId = articleUpdateDto.CategoryId;
			article.ModifiedDate= DateTime.Now;
			article.ModifiedBy = userEmail;

			await unitOfWork.GetRepository<Article>().UpdatedAsync(article);
			await unitOfWork.SaveAsync();

			return articleTitleBeforeUpdate; //Toastr için yaptım bunu. Eski başlık adını güncellerken bu değişti diye yazdırmak için.
		}
		public async Task<string> SafeDeleteArticleAsync(Guid articleId)
		{
			var userEmail = _user.GetLoggedInEmail();
			var article = await unitOfWork.GetRepository<Article>().GetByGuidAsync(articleId);

			article.IsDeleted = true;
			article.DeletedDate = DateTime.Now;
			article.DeletedBy = userEmail;

			await unitOfWork.GetRepository<Article>().UpdatedAsync(article);
			await unitOfWork.SaveAsync();

			return article.Title;
		}

	}
}
