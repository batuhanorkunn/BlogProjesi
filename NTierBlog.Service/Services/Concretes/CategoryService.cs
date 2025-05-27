using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using NTierBlog.Data.UnitOfWorks;
using NTierBlog.Entity.DTOs.Articles;
using NTierBlog.Entity.DTOs.Categories;
using NTierBlog.Entity.Entities;
using NTierBlog.Service.Extensions;
using NTierBlog.Service.Services.Abstracts;
using NTierBlog.Service.Services.Concretes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NTierBlog.Service.Services.Concretes
{
	public class CategoryService : ICategoryService
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper mapper;
		private readonly IHttpContextAccessor httpContextAccessor;
		private readonly ClaimsPrincipal _user;

		public CategoryService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
		{
			this.unitOfWork = unitOfWork;
			this.mapper = mapper;
			this.httpContextAccessor = httpContextAccessor;
			this.httpContextAccessor = httpContextAccessor;
			_user = httpContextAccessor.HttpContext.User;
		}
		public async Task<List<CategoryDto>> GetAllCategoriesNonDeleted()
		{
		


			var categories =await unitOfWork.GetRepository<Category>().GetAllAsync(x => !x.IsDeleted);
			var map = mapper.Map<List<CategoryDto>>(categories);

			return map;
		}
		public async Task CreateCategoryAsync(CategoryAddDto categoryAddDto)
		{	
			var userEmail = _user.GetLoggedInEmail();

			Category category = new(categoryAddDto.Name,userEmail);

			await unitOfWork.GetRepository<Category>().AddAsync(category);
			await unitOfWork.SaveAsync();

		
		}
		public async Task<Category> GetCategoryByGuid(Guid id)
		{
			var category = await unitOfWork.GetRepository<Category>().GetByGuidAsync(id);
			return category;

		}
		public async Task<string> UpdateCategoryAsync(CategoryUpdateDto categoryUpdateDto)
		{
			var userEmail = _user.GetLoggedInEmail();
			var category = await unitOfWork.GetRepository<Category>().GetAsync(x => !x.IsDeleted && x.Id == categoryUpdateDto.Id);

			string categoryTitleBeforeUpdate = category.Name;

			category.Name = categoryUpdateDto.Name;
			category.ModifiedBy = userEmail;
			category.ModifiedDate = DateTime.Now;

			await unitOfWork.GetRepository<Category>().UpdatedAsync(category);
			await unitOfWork.SaveAsync();

			return categoryTitleBeforeUpdate;


		}

		public async Task<string> SafeDeleteArticleAsync(Guid categoryId)
		{
			var userEmail = _user.GetLoggedInEmail();
			var category = await unitOfWork.GetRepository<Category>().GetByGuidAsync(categoryId);

			category.IsDeleted = true;
			category.DeletedDate = DateTime.Now;
			category.DeletedBy = userEmail;

			await unitOfWork.GetRepository<Category>().UpdatedAsync(category);
			await unitOfWork.SaveAsync();

			return category.Name;
		}
	}
}

