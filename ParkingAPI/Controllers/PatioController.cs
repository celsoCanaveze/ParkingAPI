using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingApi.DTOs;
using ParkingApi.Models;
using ParkingApi.Repositories;

namespace ParkingApi.Controllers
{
    /// <summary>
    /// Controller responsável pelo gerenciamento de pátios de estacionamento
    /// </summary>
    [Route("api/patios")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    public class PatioController : ControllerBase
    {
        private readonly ICRUDRepository<Patio> _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Construtor do controller de pátios
        /// </summary>
        public PatioController(ICRUDRepository<Patio> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtém todos os pátios de forma paginada
        /// </summary>
        /// <param name="pagination">Parâmetros de paginação</param>
        /// <returns>Lista paginada de pátios</returns>
        /// <response code="200">Retorna a lista paginada de pátios</response>
        /// <response code="401">Não autorizado</response>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResultDto<PatioDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParameters pagination)
        {
            var (items, totalCount) = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize);
            var patioDtos = _mapper.Map<IEnumerable<PatioDto>>(items);

            var totalPages = (int)Math.Ceiling(totalCount / (double)pagination.PageSize);
            var hasNext = pagination.Page < totalPages;
            var hasPrevious = pagination.Page > 1;

            var result = new PagedResultDto<PatioDto>
            {
                Data = patioDtos,
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
        /// Obtém um pátio específico por ID
        /// </summary>
        /// <param name="id">ID do pátio</param>
        /// <returns>Dados do pátio</returns>
        /// <response code="200">Retorna o pátio encontrado</response>
        /// <response code="404">Pátio não encontrado</response>
        /// <response code="401">Não autorizado</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(PatioDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var patio = await _repository.GetByIdAsync(id);
            if (patio == null)
                return NotFound(new { message = "Pátio não encontrado" });

            var patioDto = _mapper.Map<PatioDto>(patio);

            var response = new
            {
                data = patioDto,
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
        /// Cria um novo pátio
        /// </summary>
        /// <param name="dto">Dados do pátio a ser criado</param>
        /// <returns>Pátio criado</returns>
        /// <response code="201">Pátio criado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        [HttpPost]
        [ProducesResponseType(typeof(PatioDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Post([FromBody] PatioDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var patio = _mapper.Map<Patio>(dto);
            var createdPatio = await _repository.AddAsync(patio);
            var patioDto = _mapper.Map<PatioDto>(createdPatio);

            var response = new
            {
                data = patioDto,
                links = new Dictionary<string, string>
                {
                    ["self"] = Url.Action(nameof(Get), new { id = patioDto.Id }) ?? "",
                    ["update"] = Url.Action(nameof(Put), new { id = patioDto.Id }) ?? "",
                    ["delete"] = Url.Action(nameof(Delete), new { id = patioDto.Id }) ?? "",
                    ["all"] = Url.Action(nameof(GetAll)) ?? ""
                }
            };

            return CreatedAtAction(nameof(Get), new { id = patioDto.Id }, response);
        }

        /// <summary>
        /// Atualiza um pátio existente
        /// </summary>
        /// <param name="id">ID do pátio</param>
        /// <param name="dto">Dados atualizados do pátio</param>
        /// <returns>Confirmação da atualização</returns>
        /// <response code="204">Pátio atualizado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="404">Pátio não encontrado</response>
        /// <response code="401">Não autorizado</response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] PatioDto dto)
        {
            if (id != dto.Id)
                return BadRequest(new { message = "ID da URL não confere com o ID do corpo da requisição" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingPatio = await _repository.GetByIdAsync(id);
            if (existingPatio == null)
                return NotFound(new { message = "Pátio não encontrado" });

            var patio = _mapper.Map<Patio>(dto);
            await _repository.UpdateAsync(patio);
            return NoContent();
        }

        /// <summary>
        /// Remove um pátio
        /// </summary>
        /// <param name="id">ID do pátio</param>
        /// <returns>Confirmação da remoção</returns>
        /// <response code="204">Pátio removido com sucesso</response>
        /// <response code="404">Pátio não encontrado</response>
        /// <response code="401">Não autorizado</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var patio = await _repository.GetByIdAsync(id);
            if (patio == null)
                return NotFound(new { message = "Pátio não encontrado" });

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
