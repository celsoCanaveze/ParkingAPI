using Microsoft.AspNetCore.Mvc;
using ParkingAPI.Models;
using ParkingAPI.Repositories;
using ParkingAPI.Services;
using System.ComponentModel.DataAnnotations;

namespace ParkingAPI.Controllers
{
    /// <summary>
    /// Controller responsável pela autenticação de usuários
    /// </summary>
    [Route("api/auth")]
    [ApiController]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ICRUDRepository<Usuario> _repository;

        /// <summary>
        /// Construtor do controller de autenticação
        /// </summary>
        public AuthController(IAuthService authService, ICRUDRepository<Usuario> repository)
        {
            _authService = authService;
            _repository = repository;
        }

        /// <summary>
        /// Realiza o login do usuário e retorna um token JWT
        /// </summary>
        /// <param name="login">Credenciais de login</param>
        /// <returns>Token JWT para autenticação</returns>
        /// <response code="200">Login realizado com sucesso, retorna o token</response>
        /// <response code="401">Credenciais inválidas</response>
        /// <response code="400">Dados de entrada inválidos</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = (await _repository.GetAllAsync())
                .FirstOrDefault(u => u.Username == login.Username && u.Password == login.Password);

            if (user == null) 
                return Unauthorized(new { message = "Usuário ou senha inválidos" });

            var token = _authService.GenerateToken(user.Username);
            
            var response = new LoginResponse
            {
                Token = token,
                ExpiresIn = 3600, // 1 hora
                TokenType = "Bearer",
                Username = user.Username
            };

            return Ok(response);
        }
    }

    /// <summary>
    /// Requisição de login
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Nome de usuário
        /// </summary>
        /// <example>admin</example>
        [Required(ErrorMessage = "Username é obrigatório")]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Senha do usuário
        /// </summary>
        /// <example>123456</example>
        [Required(ErrorMessage = "Password é obrigatório")]
        public string Password { get; set; } = string.Empty;
    }

    /// <summary>
    /// Resposta do login
    /// </summary>
    public class LoginResponse
    {
        /// <summary>
        /// Token JWT gerado
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Tempo de expiração em segundos
        /// </summary>
        public int ExpiresIn { get; set; }

        /// <summary>
        /// Tipo do token
        /// </summary>
        public string TokenType { get; set; } = string.Empty;

        /// <summary>
        /// Nome do usuário autenticado
        /// </summary>
        public string Username { get; set; } = string.Empty;
    }
}
