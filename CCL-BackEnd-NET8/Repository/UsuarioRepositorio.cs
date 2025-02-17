
using CCL_BackEnd_NET8.Data;
using CCL_BackEnd_NET8.Models;
using CCL_BackEnd_NET8.Models.Dtos;
using CCL_BackEnd_NET8.Repository.IRepository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CCL_BackEnd_NET8.Repository
{
	public class UsuarioRepositorio: IUsuarioRepositorio
    {
        private readonly ApplicationsDbContext _db;
        private string claveSecreta;
        public UsuarioRepositorio(ApplicationsDbContext db, IConfiguration config)
		{
			_db = db;
            claveSecreta = config.GetValue<string>("ApiSettings:Secreta");
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

        public async Task<UsuarioLoginRespuestaDto> Login(UsuarioLoginRespuestaDto usuarioLoginRespuestaDto)
        {
            //var passwordEncriptado = obtenermd5(usuarioLoginRespuestaDto.Password);

            //var usuario = await _db.Usuario.FirstOrDefault(
            //        u => u.Email.ToLower() == usuarioLoginRespuestaDto.usuarioDatosDto.Email.ToLower()
            //            && u.Password == passwordEncriptado
            //    );

            //if (usuario == null)
            //{
            //    return new UsuarioLoginRespuestaDto()
            //    {
                    //Usuario = null,
                    //Token = "",
            //    };
            //}

            // si usuario existe y es correcto emaily pass
            var Jwt = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(claveSecreta);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    //new Claim(ClaimTypes.Email, usuario.Email),
                    //new Claim(ClaimTypes.Role, usuario.Role),

                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = Jwt.CreateToken(tokenDescriptor);
            //UsuarioLoginRespuestaDto usuarioLoginRespuestaDto = new UsuarioLoginRespuestaDto()
            //{
            //    Token = Jwt.WriteToken(token),
                //Usuario = usuario
            //};

            return usuarioLoginRespuestaDto;
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
            //usuario.Password = passwordEncriptado;
            return usuario;
        }

        public static string obtenermd5(string valor)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] data = Encoding.UTF8.GetBytes(valor);
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

