using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NTierBlog.Entity.DTOs.Images;
using NTierBlog.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierBlog.Service.Helpers.Images
{
	public interface IImageHelper
	{
		Task<ImageUploadedDto> Upload(string name, IFormFile imageFile, ImageType imageType, string folderName = null);
		void Delete(string imageName);
	}
}
