using System;
using System.ComponentModel.DataAnnotations;

namespace CCL_BackEnd_NET8.Models.Dtos
{
	public class CantidadProductoDto
	{
        [Required(ErrorMessage = "Campo cantidad es Obligatorio.")]
        public int Cantidad { get; set; }
    }
}

