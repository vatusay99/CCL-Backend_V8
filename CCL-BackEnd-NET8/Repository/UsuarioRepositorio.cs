
using CCL_BackEnd_NET8.Data;
using CCL_BackEnd_NET8.Models;
using CCL_BackEnd_NET8.Models.Dtos;
using CCL_BackEnd_NET8.Repository.IRepository;
using Microsoft.AspNetCore.Identity.Data;
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
        private string Issuer;
        private string Audience;

        public UsuarioRepositorio(ApplicationsDbContext db, IConfiguration config)
		{
			_db = db;
            claveSecreta = config.GetValue<string>("ApiSettings:Secreta");
            Issuer = config.GetValue<string>("ApiSettings:Issuer");
            Audience = config.GetValue<string>("ApiSettings:Audience");
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
            var passwordEncriptado = obtenermd5(usuarioLoginRespuestaDto.Usuario.Password);

            var usuario = _db.Usuario.FirstOrDefault(
                    u => u.Email.ToLower() == usuarioLoginRespuestaDto.Usuario.Email.ToLower()
                        && u.Password == passwordEncriptado
                );

            if (usuario == null)
            {
                return new UsuarioLoginRespuestaDto()
                {
                    Token = "",
                    Usuario = null,
                };
            }

            var token = GenerateTwt(usuarioLoginRespuestaDto, usuario.Role);

            if (token.IsNullOrEmpty())
            {
                return new UsuarioLoginRespuestaDto()
                {
                    Token = "",
                    Usuario = null,
                };
            }

            UsuarioLoginRespuestaDto usuarioLoginRespuesta = new UsuarioLoginRespuestaDto()
            {
                Token = token,
                Usuario = usuarioLoginRespuestaDto.Usuario
            };

            return usuarioLoginRespuesta;
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

        public string GenerateTwt(UsuarioLoginRespuestaDto usuarioLoginRespuestaDto, string role)
        {
            var SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(claveSecreta));
            var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Email, usuarioLoginRespuestaDto.Usuario.Email.ToString()),
                    new Claim(ClaimTypes.Role, role.ToString())
                };

            var token = new JwtSecurityToken(
                    Issuer,
                    Audience,
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(2),
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

