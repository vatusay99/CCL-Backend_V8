using System;
using System.Net;

namespace CCL_BackEnd_NET8.Models
{
	public class RespuestasApi
	{
		public RespuestasApi()
		{
			ErrorMessages = new List<string>();
		}
		
		public HttpStatusCode StatusCode { get; set; }

		public bool isSuccess { get; set; } = true;

		public List<string> ErrorMessages { get; set; }

		public object Result { get; set; }

	}
}

