using System;
using CCL_BackEnd_NET8.Data;
using CCL_BackEnd_NET8.Models;
using CCL_BackEnd_NET8.Models.Dtos;
using CCL_BackEnd_NET8.Repository.IRepository;

namespace CCL_BackEnd_NET8.Repository
{
	public class UsuarioRepositorio: IUsuarioRepositorio
    {
        private readonly ApplicationsDbContext _db;
        public UsuarioRepositorio(ApplicationsDbContext db)
		{
			_db = db;
		}

        public ICollection<Usuario> GetUsuario()
        {
            return _db.Usuario.OrderBy(c => c.Nombre).ToList();
        }

        public Usuario GetUsuario(int Id)
        {
            return _db.Usuario.FirstOrDefault(u => u.Id == Id);
        }

        public bool IsUniqueEmail(string email)
        {
            var usuarioDB = _db.Usuario.FirstOrDefault(u => u.Email == email);
            if (usuarioDB != null)
            {
                return false;
            }

            return true;

        }

        public Task<UsuarioLoginRespuestaDto> Login(UsuarioDto usuarioDto)
        {
            throw new NotImplementedException();
        }

        public Task<UsuarioDatosDto> Registro(UsuarioRegistradoDto usuarioRegistradoDto)
        {
            throw new NotImplementedException();
        }
    }
}

