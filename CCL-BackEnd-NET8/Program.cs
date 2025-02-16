using CCL_BackEnd_NET8.Data;
using CCL_BackEnd_NET8.Extencions;
using CCL_BackEnd_NET8.ProductosMaper;
using CCL_BackEnd_NET8.Repository;
using CCL_BackEnd_NET8.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration
    .GetConnectionString("ConexionPostGreSQL")
    ?? throw new ArgumentNullException("No se encontro la cadena de conexion a DB");

builder.Services.AddDbContext<ApplicationsDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

// Adicionamos los repos
builder.Services.AddScoped<IProductoRepositorio, ProductoRepositorio>();

// agregar automapper
builder.Services.AddAutoMapper(typeof(ProductoMapper));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.AddDataPrueba();

app.Run();

