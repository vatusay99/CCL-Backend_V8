
using System.Text;
using CCL_BackEnd_NET8.Data;
using CCL_BackEnd_NET8.ProductosMaper;
using CCL_BackEnd_NET8.Repository;
using CCL_BackEnd_NET8.Repository.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(p => p.AddPolicy("PoliticaCors", builder =>
{
    builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
}));

var connectionString = builder.Configuration
    .GetConnectionString("ConexionPostGreSQL")
    ?? throw new ArgumentNullException("No se encontro la cadena de conexion a DB");

builder.Services.AddDbContext<ApplicationsDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

// Cache
builder.Services.AddResponseCaching();

// Adicionamos los repos
builder.Services.AddScoped<IProductoRepositorio, ProductoRepositorio>();
builder.Services.AddScoped<IMovientoRepositorio, MovimientoRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

var key = builder.Configuration.GetValue<string>("ApiSettings:Secreta");


// agregar automapper≠
builder.Services.AddAutoMapper(typeof(ProductoMapper));

// Configuracion de permisos de autenticacion
builder.Services.AddAuthentication(
    x=>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }
    ).AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key))
        };
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("PoliticaCors");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//app.AddDataPrueba();

app.Run();

