
using AutoMapper;
using CCL_BackEnd_NET8.Models;
using CCL_BackEnd_NET8.Models.Dtos;
using CCL_BackEnd_NET8.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CCL_BackEnd_NET8.Controllers
{
    [Route("api/movimiento")]
    [ApiController]
    public class MovimientosController : ControllerBase
    {
        private readonly IMovientoRepositorio _movRepo;
        private readonly IMapper _mapper;

        public MovimientosController(IMovientoRepositorio movRepo, IMapper mapper)
        {
            _movRepo = movRepo;
            _mapper = mapper;

        }

        // GET: api/values
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetMovimientos()
        {
            var listaMovimientos = _movRepo.GetMovimiento();
            var listaMovimientosDto = new List<MovimientoDto>();

            foreach (var lista in listaMovimientos)
            {
                listaMovimientosDto.Add(_mapper.Map<MovimientoDto>(lista));
            }

            return Ok(listaMovimientosDto);
        }

        // GET api/values/5
        [HttpGet("{movimientoId:int}", Name = "GetMovimientoById")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetMovimientoById(int movimientoId)
        {
            var itemMovimiento = _movRepo.GetMovimiento(movimientoId);
            if (itemMovimiento == null)
            {
                return NotFound();
            }

            var itemMovimientoDto = _mapper.Map<MovimientoDto>(itemMovimiento);

            return Ok(itemMovimientoDto);
        }

        // POST api/values
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(CrearMovimientoDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CrearMovimiento([FromBody] CrearMovimientoDto crearMovimientoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (crearMovimientoDto == null)
            {
                return BadRequest(ModelState);
            }

            var movimientoMapper = _mapper.Map<Movimiento>(crearMovimientoDto);
            if (!_movRepo.CrearMovimiento(movimientoMapper))
            {
                ModelState.AddModelError("", $"Algo salio mal creando el movimiento {crearMovimientoDto.Cantidad} con estado {crearMovimientoDto.Entrada_Salida}" );
                return StatusCode(404, ModelState);
            }

            return CreatedAtRoute("GetMovimientoById", new { movimientoId = movimientoMapper.Id }, movimientoMapper);
        }

        // Get api/movimiento/idProducto
        [HttpGet("BuscarIdProductoEnMovimiento/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult BuscarIdProductoEnMovimiento(int id)
        {
            var listaMovimientosPorIdProducto = _movRepo.GetMovimientoPorIdProducto(id);

            if(listaMovimientosPorIdProducto == null)
            {
                return NotFound();
            }

            var itemMovimientos = new List<MovimientoDto>();

            foreach (var item in listaMovimientosPorIdProducto)
            {
                itemMovimientos.Add(_mapper.Map<MovimientoDto>(item));
            }
            return Ok();
        }

    }
}

