using System;
using CCL_BackEnd_NET8.Models;
using CCL_BackEnd_NET8.Models.Dtos;

namespace CCL_BackEnd_NET8.Repository.IRepository
{
	public interface IUsuarioRepositorio
    {
		ICollection<Usuario> GetUsuario();

		Usuario GetUsuario(int Id);

		bool IsUniqueEmail(string email);

		Task<UsuarioLoginRespuestaDto> Login(UsuarioDto usuarioDto);
		Task<UsuarioDatosDto> Registro(UsuarioRegistradoDto usuarioRegistradoDto);
    }
}

