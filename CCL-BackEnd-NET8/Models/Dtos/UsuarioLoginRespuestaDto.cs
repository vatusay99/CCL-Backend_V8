﻿using System;
using System.ComponentModel.DataAnnotations;

namespace CCL_BackEnd_NET8.Models.Dtos
{
	public class UsuarioLoginRespuestaDto
	{
		public Usuario Usuario { get; set; }

        public string Role { get; set; }

        public string Token { get; set; }
    }
}

