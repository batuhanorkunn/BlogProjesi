using NTierBlog.Entity.DTOs.Categories;
using NTierBlog.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierBlog.Service.Services.Abstracts
{
	public interface ICategoryService
	{
		Task<List<CategoryDto>> GetAllCategoriesNonDeleted();
		Task CreateCategoryAsync(CategoryAddDto categoryAddDto);
		Task<Category> GetCategoryByGuid(Guid id);
		Task<string> UpdateCategoryAsync(CategoryUpdateDto categoryUpdateDto);
		Task<string> SafeDeleteArticleAsync(Guid categoryId);
	}
}
