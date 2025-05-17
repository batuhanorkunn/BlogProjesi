using AutoMapper;
using NTierBlog.Data.UnitOfWorks;
using NTierBlog.Entity.DTOs.Categories;
using NTierBlog.Entity.Entities;
using NTierBlog.Service.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierBlog.Service.Services.Concretes
{
	public class CategoryService : ICategoryService
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper mapper;

		public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			this.unitOfWork = unitOfWork;
			this.mapper = mapper;
		}
		public async Task<List<CategoryDto>> GetAllCategoriesNonDeleted()
		{
			var categories =await unitOfWork.GetRepository<Category>().GetAllAsync(x => !x.IsDeleted);
			var map = mapper.Map<List<CategoryDto>>(categories);

			return map;
		}
	}
}
