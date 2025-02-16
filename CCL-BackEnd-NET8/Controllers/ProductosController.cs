using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CCL_BackEnd_NET8.Models;
using CCL_BackEnd_NET8.Models.Dtos;
using CCL_BackEnd_NET8.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CCL_BackEnd_NET8.Controllers
{
    [Route("api/productos")]
    [ApiController]
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

            var itemProductoDto = _mapper.Map<ProductoDto>(itemProducto);

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

            

            return CreatedAtRoute("GetProducto", new {productoId = productoMapper.Id}, productoMapper);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

