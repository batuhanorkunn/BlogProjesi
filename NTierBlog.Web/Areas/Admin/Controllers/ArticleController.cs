﻿using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using NTierBlog.Entity.DTOs.Articles;
using NTierBlog.Entity.Entities;
using NTierBlog.Service.Services.Abstracts;
using NTierBlog.Web.ResultMessages;
using NToastNotify;

namespace NTierBlog.Web.Areas.Admin.Controllers
{
	[Area("Admin")]

	public class ArticleController : Controller
	{
		private readonly IArticleService articleService;
		private readonly ICategoryService categoryService;
		private readonly IMapper mapper;
		private readonly IValidator<Article> validator;
		private readonly IToastNotification toast;

		public ArticleController(IArticleService articleService, ICategoryService categoryService, IMapper mapper, IValidator<Article> validator, IToastNotification toast)
		{
			this.articleService = articleService;
			this.categoryService = categoryService;
			this.mapper = mapper;
			this.validator = validator;
			this.toast = toast;
		}
		public async Task<IActionResult> Index()
		{
			var articles = await articleService.GetAllArticlesWithCategoryNonDeletedAsync();
			return View(articles);
		}
		[HttpGet]
		public async Task<IActionResult> Add()
		{
			var categories = await categoryService.GetAllCategoriesNonDeleted();
			return View(new ArticleAddDto { Categories = categories });
		}
		[HttpPost]
		public async Task<IActionResult> Add(ArticleAddDto articleAddDto)
		{
			var map = mapper.Map<Article>(articleAddDto);
			var result = await validator.ValidateAsync(map);

			if (result.IsValid)
			{
				await articleService.CreateArticleAsync(articleAddDto);
				toast.AddSuccessToastMessage(Messages.Article.Add(articleAddDto.Title), new ToastrOptions { Title = "İşlem Başarılı" });
				return RedirectToAction("Index", "Article", new { Area = "Admin" });
			}
			else
			{
				result.AddToModelState(this.ModelState);

			}
			var categories = await categoryService.GetAllCategoriesNonDeleted();
			return View(new ArticleAddDto { Categories = categories });
		}
		[HttpGet]
		public async Task<IActionResult> Update(Guid articleId)
		{
			var article = await articleService.GetArticleWithCategoryNonDeletedAsync(articleId);
			var categories = await categoryService.GetAllCategoriesNonDeleted();

			var articleUpdateDto = mapper.Map<ArticleUpdateDto>(article);
			articleUpdateDto.Categories = categories;

			return View(articleUpdateDto);
		}
		[HttpPost]
		public async Task<IActionResult> Update(ArticleUpdateDto articleUpdateDto)
		{
			var map = mapper.Map<Article>(articleUpdateDto);
			var result = await validator.ValidateAsync(map);

			if ((result.IsValid))
			{
				var title = await articleService.UpdateArticleAsync(articleUpdateDto);
				toast.AddSuccessToastMessage(Messages.Article.Update(title), new ToastrOptions { Title = "İşlem Başarılı" });
				return RedirectToAction("Index", "Article", new { Area = "Admin" });

			}
			else
			{
				result.AddToModelState(this.ModelState);
			}

			var categories = await categoryService.GetAllCategoriesNonDeleted();
			articleUpdateDto.Categories = categories;

			return View(articleUpdateDto);
		}
		public async Task<IActionResult> Delete(Guid articleId)
		{
			var title = await articleService.SafeDeleteArticleAsync(articleId);
			toast.AddSuccessToastMessage(Messages.Article.Delete(title), new ToastrOptions { Title = "İşlem Başarılı" });


			return RedirectToAction("Index", "Article", new { Area = "Admin" });
		}
	}
}
