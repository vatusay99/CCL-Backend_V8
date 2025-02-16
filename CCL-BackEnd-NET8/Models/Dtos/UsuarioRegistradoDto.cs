using System;
using System.ComponentModel.DataAnnotations;

namespace CCL_BackEnd_NET8.Models.Dtos
{
	public class UsuarioRegistradoDto
	{
        [Required(ErrorMessage ="El Email es Obligatorio")]
        [EmailAddress]
        [StringLength(120)]
        [Display(Name ="Correo Electronico")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El Password es Obligatorio")]
        public string Password { get; set; }

        [Required(ErrorMessage = "El Nombre es Obligatorio")]
        [MinLength(3)]
        [StringLength(100)]
        public string Nombre { get; set; }
    }
}

