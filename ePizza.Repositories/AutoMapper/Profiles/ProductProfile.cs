using AutoMapper;
using ePizza.Entities.Concrete;
using ePizza.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Repositories.AutoMapper.Profiles
{
	public class ProductProfile : Profile
	{
		public ProductProfile()
		{
			CreateMap<ProductAddDto, Product>(); // tek yönlü gönderiyoruz 
			CreateMap<Product, ProductUpdateDto>(); // önce ıd yakalıyoruz 
			CreateMap<ProductUpdateDto, Product>(); // burada ıd ile yakaladıktan sonra bilgileri güncelleyip geri döndürüyoruz 
			
		}
	}
}
