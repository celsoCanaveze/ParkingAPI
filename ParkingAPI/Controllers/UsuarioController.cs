using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingApi.DTOs;
using ParkingApi.Models;
using ParkingApi.Repositories;

namespace ParkingApi.Controllers
{
    /// <summary>
    /// Controller responsável pelo gerenciamento de usuários
    /// </summary>
    [Route("api/usuarios")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    public class UsuarioController : ControllerBase
    {
        private readonly ICRUDRepository<Usuario> _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Construtor do controller de usuários
        /// </summary>
        public UsuarioController(ICRUDRepository<Usuario> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtém todos os usuários de forma paginada
        /// </summary>
        /// <param name="pagination">Parâmetros de paginação</param>
        /// <returns>Lista paginada de usuários</returns>
        /// <response code="200">Retorna a lista paginada de usuários</response>
        /// <response code="401">Não autorizado</response>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResultDto<UsuarioDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParameters pagination)
        {
            var (items, totalCount) = await _repository.GetPagedAsync(pagination.Page, pagination.PageSize);
            var usuarioDtos = _mapper.Map<IEnumerable<UsuarioDto>>(items);

            var totalPages = (int)Math.Ceiling(totalCount / (double)pagination.PageSize);
            var hasNext = pagination.Page < totalPages;
            var hasPrevious = pagination.Page > 1;

            var result = new PagedResultDto<UsuarioDto>
            {
                Data = usuarioDtos,
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
        /// Obtém um usuário específico por ID
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <returns>Dados do usuário</returns>
        /// <response code="200">Retorna o usuário encontrado</response>
        /// <response code="404">Usuário não encontrado</response>
        /// <response code="401">Não autorizado</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(UsuarioDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario == null)
                return NotFound(new { message = "Usuário não encontrado" });

            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

            var response = new
            {
                data = usuarioDto,
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
        /// Cria um novo usuário
        /// </summary>
        /// <param name="dto">Dados do usuário a ser criado</param>
        /// <returns>Usuário criado</returns>
        /// <response code="201">Usuário criado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        [HttpPost]
        [ProducesResponseType(typeof(UsuarioDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Post([FromBody] UsuarioDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuario = _mapper.Map<Usuario>(dto);
            var createdUsuario = await _repository.AddAsync(usuario);
            var usuarioDto = _mapper.Map<UsuarioDto>(createdUsuario);

            var response = new
            {
                data = usuarioDto,
                links = new Dictionary<string, string>
                {
                    ["self"] = Url.Action(nameof(Get), new { id = usuarioDto.Id }) ?? "",
                    ["update"] = Url.Action(nameof(Put), new { id = usuarioDto.Id }) ?? "",
                    ["delete"] = Url.Action(nameof(Delete), new { id = usuarioDto.Id }) ?? "",
                    ["all"] = Url.Action(nameof(GetAll)) ?? ""
                }
            };

            return CreatedAtAction(nameof(Get), new { id = usuarioDto.Id }, response);
        }

        /// <summary>
        /// Atualiza um usuário existente
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <param name="dto">Dados atualizados do usuário</param>
        /// <returns>Confirmação da atualização</returns>
        /// <response code="204">Usuário atualizado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="404">Usuário não encontrado</response>
        /// <response code="401">Não autorizado</response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UsuarioDto dto)
        {
            if (id != dto.Id)
                return BadRequest(new { message = "ID da URL não confere com o ID do corpo da requisição" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingUsuario = await _repository.GetByIdAsync(id);
            if (existingUsuario == null)
                return NotFound(new { message = "Usuário não encontrado" });

            var usuario = _mapper.Map<Usuario>(dto);
            await _repository.UpdateAsync(usuario);
            return NoContent();
        }

        /// <summary>
        /// Remove um usuário
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <returns>Confirmação da remoção</returns>
        /// <response code="204">Usuário removido com sucesso</response>
        /// <response code="404">Usuário não encontrado</response>
        /// <response code="401">Não autorizado</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario == null)
                return NotFound(new { message = "Usuário não encontrado" });

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
