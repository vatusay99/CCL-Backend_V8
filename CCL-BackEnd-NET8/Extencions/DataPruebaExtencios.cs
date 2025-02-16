using System;
using Bogus;
using CCL_BackEnd_NET8.Data;
using CCL_BackEnd_NET8.Models;

namespace CCL_BackEnd_NET8.Extencions
{
	public static class DataPruebaExtencios
	{

		//public static async void AddDataPrueba(this IApplicationBuilder app)
		//{
		//	using var scope = app.ApplicationServices.CreateScope();
		//	var service = scope.ServiceProvider;
		//	var dbContext = service.GetRequiredService<ApplicationsDbContext>();

		//	if(!dbContext.Producto.Any())
		//	{
		//		var productoCollection = new List<Producto>();
		//		var faker = new Faker();
		//		for(var i =1; i<100; i++)
		//		{
		//			productoCollection.Add(new Producto
		//			{
		//				Nombre = faker.Commerce.ProductName(),
		//				Cantidad = faker.Random.Int(10, 100)
		//			});
		//		}

		//		await dbContext.Prodocto.AddRangeAsync(productoCollection);
		//		await dbContext.SaveChangesAsync();
		//	}
		//}

		//public DataPruebaExtencios()
		//{
		//}
	}
}

