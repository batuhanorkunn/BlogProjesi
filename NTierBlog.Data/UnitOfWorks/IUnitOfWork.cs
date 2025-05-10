using NTierBlog.Core.Entities;
using NTierBlog.Data.Repositories.Abstracts;

namespace NTierBlog.Data.UnitOfWorks
{
	public interface IUnitOfWork : IAsyncDisposable
	{
		IRepository<T> GetRepository<T>() where T:class ,IEntityBase, new();
		Task<int> SaveAsync();
		int Save();
	}
}
