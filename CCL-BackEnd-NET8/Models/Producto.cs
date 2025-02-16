using System;
using System.ComponentModel.DataAnnotations;

namespace CCL_BackEnd_NET8.Models
{
	public class Producto
	{
		[Key]
        public int Id { get; set; }

		[Required]
		public string Nombre { get; set; }

		[Required]
		public int Cantidad { get; set; }
	}
}

