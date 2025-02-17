using System;
using System.ComponentModel.DataAnnotations;

namespace CCL_BackEnd_NET8.Models.Dtos
{
	public class UsuarioDto
	{
        public int Id { get; set; }

        public string Email { get; set; }

        public string Nombre { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}

