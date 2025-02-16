using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCL_BackEnd_NET8.Models.Dtos
{
	public class MovimientoDto
	{
        public int Id { get; set; }

        [Required]
        public int Cantidad { get; set; }

        public enum E_S { Entrada, Salida }
        public E_S Entrada_Salida { get; set; }

        public DateTime FechaMovimiento { get; set; }

        public int ProductoId { get; set; }
    }
}

