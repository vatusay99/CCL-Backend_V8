using System;
using CCL_BackEnd_NET8.Models;

namespace CCL_BackEnd_NET8.Repository.IRepository
{
	public interface IProductoRepositorio
	{
		ICollection<Producto> GetProductos();

		Producto GetProducto(int ProductoId);

		bool ExisteProducto(int id);

		bool ExisteProducto(string nombre);

		bool CrearProducto(Producto producto);
		bool ActualizarProducto(Producto producto);

		bool EliminarProducto(Producto producto);

		bool Guardar();
	}
}

