using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingAPI.DTOs;
using ParkingAPI.Models;
using ParkingAPI.Repositories;

namespace ParkingAPI.Controllers
{
    /// <summary>
    /// Controller responsável pelo gerenciamento de motos
    /// </summary>
    [Route("api/motos")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    public class MotoController : ControllerBase
    {
        private readonly ICRUDRepository<Moto> _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Construtor do controller de motos
        /// </summary>
        public MotoController(ICRUDRepository<Moto> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtém todas as motos de forma paginada
        /// </summary>
        /// <param name="pagination">Parâmetros de paginação</param>
        /// <returns>Lista paginada de motos</returns>
        /// <response code="200">Retorna a lista paginada de motos</response>
        /// <response code="401">Não autorizado</response>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResultDto<MotoDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParameters pagination)
        {
            var (items, totalCount) = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize);
            var motoDtos = _mapper.Map<IEnumerable<MotoDto>>(items);

            var totalPages = (int)Math.Ceiling(totalCount / (double)pagination.PageSize);
            var hasNext = pagination.Page < totalPages;
            var hasPrevious = pagination.Page > 1;

            var result = new PagedResultDto<MotoDto>
            {
                Data = motoDtos,
                CurrentPage = pagination.Page,
                PageSize = pagination.PageSize,
                TotalCount = totalCount,
                TotalPages = totalPages,
                HasNext = hasNext,
                HasPrevious = hasPrevious,
                Links = new Dictionary<string, string>
                {
                    ["self"] = Url.Action(nameof(GetAll), new { page = pagination.Page, pageSize = pagination.PageSize }) ?? "",
                    ["first"] = Url.Action(nameof(GetAll), new { page = 1, pageSize = pagination.PageSize }) ?? "",
                    ["last"] = Url.Action(nameof(GetAll), new { page = totalPages, pageSize = pagination.PageSize }) ?? ""
                }
            };

            if (hasPrevious)
                result.Links["prev"] = Url.Action(nameof(GetAll), new { page = pagination.Page - 1, pageSize = pagination.PageSize }) ?? "";

            if (hasNext)
                result.Links["next"] = Url.Action(nameof(GetAll), new { page = pagination.Page + 1, pageSize = pagination.PageSize }) ?? "";

            return Ok(result);
        }

        /// <summary>
        /// Obtém uma moto específica por ID
        /// </summary>
        /// <param name="id">ID da moto</param>
        /// <returns>Dados da moto</returns>
        /// <response code="200">Retorna a moto encontrada</response>
        /// <response code="404">Moto não encontrada</response>
        /// <response code="401">Não autorizado</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(MotoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var moto = await _repository.GetByIdAsync(id);
            if (moto == null) 
                return NotFound(new { message = "Moto não encontrada" });

            var motoDto = _mapper.Map<MotoDto>(moto);
            
            var response = new
            {
                data = motoDto,
                links = new Dictionary<string, string>
                {
                    ["self"] = Url.Action(nameof(Get), new { id }) ?? "",
                    ["update"] = Url.Action(nameof(Put), new { id }) ?? "",
                    ["delete"] = Url.Action(nameof(Delete), new { id }) ?? "",
                    ["all"] = Url.Action(nameof(GetAll)) ?? ""
                }
            };

            return Ok(response);
        }

        /// <summary>
        /// Cria uma nova moto
        /// </summary>
        /// <param name="dto">Dados da moto a ser criada</param>
        /// <returns>Moto criada</returns>
        /// <response code="201">Moto criada com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        [HttpPost]
        [ProducesResponseType(typeof(MotoDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Post([FromBody] MotoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var moto = _mapper.Map<Moto>(dto);
            var createdMoto = await _repository.AddAsync(moto);
            var motoDto = _mapper.Map<MotoDto>(createdMoto);

            var response = new
            {
                data = motoDto,
                links = new Dictionary<string, string>
                {
                    ["self"] = Url.Action(nameof(Get), new { id = motoDto.Id }) ?? "",
                    ["update"] = Url.Action(nameof(Put), new { id = motoDto.Id }) ?? "",
                    ["delete"] = Url.Action(nameof(Delete), new { id = motoDto.Id }) ?? "",
                    ["all"] = Url.Action(nameof(GetAll)) ?? ""
                }
            };

            return CreatedAtAction(nameof(Get), new { id = motoDto.Id }, response);
        }

        /// <summary>
        /// Atualiza uma moto existente
        /// </summary>
        /// <param name="id">ID da moto</param>
        /// <param name="dto">Dados atualizados da moto</param>
        /// <returns>Confirmação da atualização</returns>
        /// <response code="204">Moto atualizada com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="404">Moto não encontrada</response>
        /// <response code="401">Não autorizado</response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] MotoDto dto)
        {
            if (id != dto.Id) 
                return BadRequest(new { message = "ID da URL não confere com o ID do corpo da requisição" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingMoto = await _repository.GetByIdAsync(id);
            if (existingMoto == null)
                return NotFound(new { message = "Moto não encontrada" });

            var moto = _mapper.Map<Moto>(dto);
            await _repository.UpdateAsync(moto);
            return NoContent();
        }

        /// <summary>
        /// Remove uma moto
        /// </summary>
        /// <param name="id">ID da moto</param>
        /// <returns>Confirmação da remoção</returns>
        /// <response code="204">Moto removida com sucesso</response>
        /// <response code="404">Moto não encontrada</response>
        /// <response code="401">Não autorizado</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var moto = await _repository.GetByIdAsync(id);
            if (moto == null)
                return NotFound(new { message = "Moto não encontrada" });

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
