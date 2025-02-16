using System;
using System.ComponentModel.DataAnnotations;

namespace CCL_BackEnd_NET8.Models.Dtos
{
	public class ProductoDto
	{
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo nombre Obligatorio.")]
        [MinLength(3, ErrorMessage ="Nombre debe contener minimo 3 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage ="Campo cantidad es Obligatorio.")]
        public int Cantidad { get; set; }
    }
}

