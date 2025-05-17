using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using NTierBlog.Entity.DTOs.Articles;
using NTierBlog.Service.Services.Abstracts;

namespace NTierBlog.Web.Areas.Admin.Controllers
{
	[Area("Admin")]

	public class ArticleController : Controller
	{
		private readonly IArticleService articleService;
		private readonly ICategoryService categoryService;

		public ArticleController(IArticleService articleService , ICategoryService categoryService)
		{
			this.articleService = articleService;
			this.categoryService = categoryService;
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
			return View(new ArticleAddDto { Categories = categories});
		}
		[HttpPost]
		public async Task<IActionResult> Add(ArticleAddDto articleAddDto)
		{
			await articleService.CreateArticleAsync(articleAddDto);
			RedirectToAction("Index", "Article", new { Area = "Admin" });


			var categories = await categoryService.GetAllCategoriesNonDeleted();
			return View(new ArticleAddDto { Categories = categories });
		}
	}
}
