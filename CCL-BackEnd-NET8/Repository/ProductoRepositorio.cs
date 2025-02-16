using System;
using CCL_BackEnd_NET8.Data;
using CCL_BackEnd_NET8.Models;
using CCL_BackEnd_NET8.Repository.IRepository;

namespace CCL_BackEnd_NET8.Repository
{
	public class ProductoRepositorio: IProductoRepositorio
	{
		private readonly ApplicationsDbContext _db;

        public ProductoRepositorio(ApplicationsDbContext db)
        {
            _db = db;
        }


        public bool ActualizarProducto(Producto producto)
        {
            _db.Prodocto.Update(producto);
            return Guardar();
        }

        public bool CrearProducto(Producto producto)
        {
            _db.Prodocto.Add(producto);
            return Guardar();
        }

        public bool EliminarProducto(Producto producto)
        {
            _db.Prodocto.Remove(producto);
            return Guardar();
        }

        public bool ExisteProducto(int id)
        {
            return _db.Prodocto.Any(p => p.Id == id);
        }

        public bool ExisteProducto(string nombre)
        {
            bool valor = _db.Prodocto.Any(p => p.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            return valor;
        }

        public Producto GetProducto(int ProductoId)
        {
            return _db.Prodocto.FirstOrDefault(p => p.Id == ProductoId);
        }

        public ICollection<Producto> GetProductos()
        {
            return _db.Prodocto.OrderBy(p => p.Id).ToList();
        }

        public bool Guardar()
        {
            return _db.SaveChanges() > 0 ? true : false;
        }
    }
}

