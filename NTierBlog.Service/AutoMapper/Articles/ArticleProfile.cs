using AutoMapper;
using NTierBlog.Entity.DTOs.Articles;
using NTierBlog.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierBlog.Service.AutoMapper.Articles
{
	public class ArticleProfile : Profile
	{
		public ArticleProfile()
		{
			CreateMap<ArticleDto, Article>().ReverseMap();
			CreateMap<ArticleUpdateDto, Article>().ReverseMap();
			CreateMap<ArticleUpdateDto, ArticleDto>().ReverseMap();
		}
	}
}
