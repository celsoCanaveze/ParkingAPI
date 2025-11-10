using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingAPI.DTOs;
using ParkingAPI.Models;
using ParkingAPI.Repositories;
using ParkingAPI.Services;
using System.ComponentModel.DataAnnotations;

namespace ParkingAPI.Controllers
{
    /// <summary>
    /// Controller responsável pelo gerenciamento de clientes
    /// </summary>
    [Route("api/clientes")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    public class ClienteController : ControllerBase
    {
        private readonly ICRUDRepository<Cliente> _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Construtor do controller de clientes
        /// </summary>
        /// <param name="repository">Repositório de clientes</param>
        /// <param name="mapper">Mapper para conversão de objetos</param>
        public ClienteController(ICRUDRepository<Cliente> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtém todos os clientes de forma paginada
        /// </summary>
        /// <param name="pagination">Parâmetros de paginação</param>
        /// <returns>Lista paginada de clientes</returns>
        /// <response code="200">Retorna a lista paginada de clientes</response>
        /// <response code="401">Não autorizado</response>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResultDto<ClienteDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParameters pagination)
        {
            var (items, totalCount) = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize);
            var clienteDtos = _mapper.Map<IEnumerable<ClienteDto>>(items);

            var totalPages = (int)Math.Ceiling(totalCount / (double)pagination.PageSize);
            var hasNext = pagination.Page < totalPages;
            var hasPrevious = pagination.Page > 1;

            var result = new PagedResultDto<ClienteDto>
            {
                Data = clienteDtos,
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
        /// Obtém um cliente específico por ID
        /// </summary>
        /// <param name="id">ID do cliente</param>
        /// <returns>Dados do cliente</returns>
        /// <response code="200">Retorna o cliente encontrado</response>
        /// <response code="404">Cliente não encontrado</response>
        /// <response code="401">Não autorizado</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ClienteDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var cliente = await _repository.GetByIdAsync(id);
            if (cliente == null) 
                return NotFound(new { message = "Cliente não encontrado" });

            var clienteDto = _mapper.Map<ClienteDto>(cliente);
            
            // Adicionar links HATEOAS
            var response = new
            {
                data = clienteDto,
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
        /// Cria um novo cliente
        /// </summary>
        /// <param name="dto">Dados do cliente a ser criado</param>
        /// <returns>Cliente criado</returns>
        /// <response code="201">Cliente criado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        [HttpPost]
        [ProducesResponseType(typeof(ClienteDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Post([FromBody] ClienteDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cliente = _mapper.Map<Cliente>(dto);
            var createdCliente = await _repository.AddAsync(cliente);
            var clienteDto = _mapper.Map<ClienteDto>(createdCliente);

            var response = new
            {
                data = clienteDto,
                links = new Dictionary<string, string>
                {
                    ["self"] = Url.Action(nameof(Get), new { id = clienteDto.Id }) ?? "",
                    ["update"] = Url.Action(nameof(Put), new { id = clienteDto.Id }) ?? "",
                    ["delete"] = Url.Action(nameof(Delete), new { id = clienteDto.Id }) ?? "",
                    ["all"] = Url.Action(nameof(GetAll)) ?? ""
                }
            };

            return CreatedAtAction(nameof(Get), new { id = clienteDto.Id }, response);
        }

        /// <summary>
        /// Atualiza um cliente existente
        /// </summary>
        /// <param name="id">ID do cliente</param>
        /// <param name="dto">Dados atualizados do cliente</param>
        /// <returns>Confirmação da atualização</returns>
        /// <response code="204">Cliente atualizado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="404">Cliente não encontrado</response>
        /// <response code="401">Não autorizado</response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] ClienteDto dto)
        {
            if (id != dto.Id) 
                return BadRequest(new { message = "ID da URL não confere com o ID do corpo da requisição" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingCliente = await _repository.GetByIdAsync(id);
            if (existingCliente == null)
                return NotFound(new { message = "Cliente não encontrado" });

            var cliente = _mapper.Map<Cliente>(dto);
            await _repository.UpdateAsync(cliente);
            return NoContent();
        }

        /// <summary>
        /// Remove um cliente
        /// </summary>
        /// <param name="id">ID do cliente</param>
        /// <returns>Confirmação da remoção</returns>
        /// <response code="204">Cliente removido com sucesso</response>
        /// <response code="404">Cliente não encontrado</response>
        /// <response code="401">Não autorizado</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var cliente = await _repository.GetByIdAsync(id);
            if (cliente == null)
                return NotFound(new { message = "Cliente não encontrado" });

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
