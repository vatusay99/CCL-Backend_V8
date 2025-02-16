using System;
using AutoMapper;
using CCL_BackEnd_NET8.Models;
using CCL_BackEnd_NET8.Models.Dtos;

namespace CCL_BackEnd_NET8.ProductosMaper
{
	public class ProductoMapper: Profile
	{
		public ProductoMapper()
		{
			CreateMap<Producto, ProductoDto>().ReverseMap();
			CreateMap<Producto, CrearProductoDto>().ReverseMap();

        }
	}
}

