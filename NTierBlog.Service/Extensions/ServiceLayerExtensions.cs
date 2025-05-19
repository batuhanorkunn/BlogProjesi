using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using NTierBlog.Service.FluentValidations;
using NTierBlog.Service.Services.Abstracts;
using NTierBlog.Service.Services.Concretes;
using System.Globalization;
using System.Reflection;

namespace NTierBlog.Service.Extensions
{
	public static class ServiceLayerExtensions
	{
		public static IServiceCollection LoadServiceLayerExtension(this IServiceCollection services)
		{
			var assembly = Assembly.GetExecutingAssembly();


			services.AddScoped<IArticleService, ArticleService>();
			services.AddScoped<ICategoryService, CategoryService>();

			services.AddAutoMapper(assembly);

			services.AddControllersWithViews()
			  .AddFluentValidation(opt =>
			  {
				  opt.RegisterValidatorsFromAssemblyContaining<ArticleValidator>();
				  opt.DisableDataAnnotationsValidation = true;
				  opt.ValidatorOptions.LanguageManager.Culture = new CultureInfo("tr");
			  });

			return services;
		}


	}
}
