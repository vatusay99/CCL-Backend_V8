using System;
using CCL_BackEnd_NET8.Models;

namespace CCL_BackEnd_NET8.Repository.IRepository
{
	public interface IMovientoRepositorio
    {
		ICollection<Movimiento> GetMovimiento();
		ICollection<Movimiento> GetMovimientoPorIdProducto(int Id);

		Movimiento GetMovimiento(int movimientoId);

        bool ExisteMovimiento(int movimientoId);

		bool CrearMovimiento(Movimiento movimiento);

		bool ActualizarMovimiento(Movimiento movimiento);

		bool EliminarMovimiento(Movimiento movimiento);

		bool Guardar();
	}
}

