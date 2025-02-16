using System;
using System.ComponentModel.DataAnnotations;

namespace CCL_BackEnd_NET8.Models.Dtos
{
	public class CrearMovimientoDto
    {
        [Required(ErrorMessage = "Campo Cantidad es Obligatorio.")]
        public int Cantidad { get; set; }

        public enum E_S { Entrada, Salida }
        public E_S Entrada_Salida { get; set; }

        //public DateTime FechaMovimiento { get; set; }

        [Required(ErrorMessage = "Campo ProductoId es Obligatorio.")]
        public int ProductoId { get; set; }
    }
}

