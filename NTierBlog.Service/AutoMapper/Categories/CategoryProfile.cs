using AutoMapper;
using NTierBlog.Entity.DTOs.Categories;
using NTierBlog.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierBlog.Service.AutoMapper.Categories
{
	public class CategoryProfile : Profile
	{
		public CategoryProfile()
		{
			CreateMap<CategoryDto,Category>().ReverseMap();
		}
	}
}
