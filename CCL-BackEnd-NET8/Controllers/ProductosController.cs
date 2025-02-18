using AutoMapper;
using CCL_BackEnd_NET8.Models;
using CCL_BackEnd_NET8.Models.Dtos;
using CCL_BackEnd_NET8.ProductosMaper;
using CCL_BackEnd_NET8.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CCL_BackEnd_NET8.Controllers
{
    [Route("/productos/movimiento")]
    [ApiController]
    [Authorize]
    [ResponseCache(Duration = 20)]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoRepositorio _prRepo;
        private readonly IMapper _mapper;

        public ProductosController(IProductoRepositorio prRepo, IMapper mapper)
        {
            _prRepo = prRepo;
            _mapper = mapper;
        }

        // GET: api/values
        [HttpGet(Name = "/inventario")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetProducts()
        {
            var listaProductos = _prRepo.GetProductos();
            var listaProductosDto = new List<ProductoDto>();

            foreach(var lista in listaProductos)
            {
                listaProductosDto.Add(_mapper.Map<ProductoDto>(lista));
            }

            return Ok(listaProductosDto);
        }

        // GET api/values/5
        [HttpGet("{productId:int}", Name = "GetProductById")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetProductById(int productId)
        {
            var itemProducto = _prRepo.GetProducto(productId);
            if(itemProducto == null)
            {
                return NotFound();
            }

            var itemProductoDto = _mapper.Map<UsuarioDatosDto>(itemProducto);

            return Ok(itemProductoDto);
        }

        // POST api/values
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CrearProducto([FromBody] CrearProductoDto crearProductoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           
            if (crearProductoDto == null)
            {
                return BadRequest(ModelState);
            }

            var productoMapper = _mapper.Map<Producto>(crearProductoDto);
            if (!_prRepo.CrearProducto(productoMapper))
            {
                ModelState.AddModelError("", $"Algo salio mal guardando el producto {crearProductoDto.Nombre}");
                return StatusCode(404, ModelState);
            }

            return CreatedAtRoute("GetProductById", new {productoId = productoMapper.Id}, productoMapper);
        }

        // PUT api/values/5
        [HttpPatch("{productId:int}", Name = "ActualizarCantidadProducto")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ActualizarCantidadProducto(int productId, [FromBody] ProductoDto productoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (productoDto == null || productId != productoDto.Id )
            {
                return BadRequest(ModelState);
            }

            var producto = _mapper.Map<Producto>(productoDto);

            if (!_prRepo.ActualizarProducto(producto))
            {
                ModelState.AddModelError("", $"Algo salio mal actualizando el producto {producto.Nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{productId:int}", Name = "BorrarProducto")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult BorrarProducto(int productId)
        {
            if (!_prRepo.ExisteProducto(productId))
            {
                return NotFound($"No existe Id {productId}");
            }

            var producto = _prRepo.GetProducto(productId);
            if (!_prRepo.EliminarProducto(producto))
            {
                ModelState.AddModelError("", $"Algo salio mal eliminar el producto {producto.Nombre}");
                return StatusCode(404, ModelState);
            }

            return NoContent();
        }
    }
}

