using Microsoft.Extensions.DependencyInjection;
using NTierBlog.Service.Services.Abstracts;
using NTierBlog.Service.Services.Concretes;
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
			return services;
		}


	}
}
