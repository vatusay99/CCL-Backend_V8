using System;
using CCL_BackEnd_NET8.Data;
using CCL_BackEnd_NET8.Models;
using CCL_BackEnd_NET8.Models.Dtos;
using CCL_BackEnd_NET8.Repository.IRepository;
using System.Security.Cryptography;
using System.Text;

namespace CCL_BackEnd_NET8.Repository
{
	public class UsuarioRepositorio: IUsuarioRepositorio
    {
        private readonly ApplicationsDbContext _db;
        public UsuarioRepositorio(ApplicationsDbContext db)
		{
			_db = db;
		}

        public ICollection<Usuario> GetUsuarios()
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

        public Task<UsuarioLoginRespuestaDto> Login(UsuarioLoginRespuestaDto usuarioLoginRespuestaDto)
        {
            throw new NotImplementedException();
        }

        public async Task<Usuario> Registro(UsuarioRegistradoDto usuarioRegistradoDto)
        {
            var passwordEncriptado = obtenermd5(usuarioRegistradoDto.Password);

            Usuario usuario = new Usuario()
            {
                Email = usuarioRegistradoDto.Email,
                Password = passwordEncriptado,
                Nombre = usuarioRegistradoDto.Nombre,
                Role = usuarioRegistradoDto.Role
            };

            _db.Usuario.Add(usuario);
            await _db.SaveChangesAsync();
            usuario.Password = passwordEncriptado;
            return usuario;
        }

        public static string obtenermd5(string valor)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(valor);
            data = x.ComputeHash(data);
            string resp = "";
            for (int i =0; i<data.Length; i++)
            {
                resp += data[i].ToString("x2").ToLower();
            }
            return resp;
        }
    }
}

