using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NTierBlog.Data.Repositories.Abstracts;
using NTierBlog.Data.Repositories.Concretes;

namespace NTierBlog.Data.Extensions
{
	public static class DataLayerExtensions
	{
		public static IServiceCollection LoadDataLayerExtension(this IServiceCollection services, IConfiguration config)
		{
			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
				return services;
		}
	}
}
