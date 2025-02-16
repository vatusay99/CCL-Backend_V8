using System;
using CCL_BackEnd_NET8.Data;
using CCL_BackEnd_NET8.Models;
using CCL_BackEnd_NET8.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace CCL_BackEnd_NET8.Repository
{
	public class MovimientoRepositorio : IMovientoRepositorio
    {
		private readonly ApplicationsDbContext _db;

        public MovimientoRepositorio(ApplicationsDbContext db)
        {
            _db = db;
        }

        public bool ActualizarMovimiento(Movimiento movimiento)
        {
            movimiento.FechaMovimiento = new DateTime();
            _db.Movimiento.Update(movimiento);

            return Guardar();
        }

        public bool CrearMovimiento(Movimiento movimiento)
        {
            //movimiento.FechaMovimiento = DateTime.UtcNow.ToLocalTime();
            _db.Movimiento.Add(movimiento);
            return Guardar();
        }

        public bool EliminarMovimiento(Movimiento movimiento)
        {
            _db.Movimiento.Remove(movimiento);
            return Guardar();
        }

        public bool ExisteMovimiento(int id)
        {
            return _db.Movimiento.Any(m => m.Id == id);
        }

        public ICollection<Movimiento> GetMovimientos()
        {
            return _db.Movimiento.OrderBy(m => m.Id).ToList();
        }

        public Movimiento GetMovimiento(int movimientoId)
        {
            return _db.Movimiento.FirstOrDefault(m => m.Id == movimientoId);
        }

        public bool Guardar()
        {
            return _db.SaveChanges() > 0 ? true : false;
        }

        public ICollection<Movimiento> GetMovimiento()
        {
            return _db.Movimiento.OrderBy(m => m.Id).ToList();
        }

        public ICollection<Movimiento> GetMovimientoPorIdProducto(int Id)
        {

            return (ICollection<Movimiento>)_db.Movimiento.FirstOrDefault(m => m.ProductoId == Id);
            //return _db.Movimiento.Include(m => m.ProductoId).Where(m => m.ProductoId == Id).ToList();
        }
    }
}

