using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NTierBlog.Data.Context;
using NTierBlog.Data.Repositories.Abstracts;
using NTierBlog.Data.Repositories.Concretes;
using NTierBlog.Data.UnitOfWorks;

namespace NTierBlog.Data.Extensions
{
	public static class DataLayerExtensions
	{
		public static IServiceCollection LoadDataLayerExtension(this IServiceCollection services, IConfiguration config)
		{
			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
			services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(config.GetConnectionString("DefaultConnection")));
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			return services;
		}
				
			
	}
}
