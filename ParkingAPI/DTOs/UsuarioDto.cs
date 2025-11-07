using System.ComponentModel.DataAnnotations;

namespace ParkingApi.DTOs
{
    /// <summary>
    /// DTO para transferência de dados de Usuário
    /// </summary>
    public class UsuarioDto
    {
        /// <summary>
        /// ID único do usuário
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// Nome de usuário para login
        /// </summary>
        /// <example>admin</example>
        [Required(ErrorMessage = "O username é obrigatório")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O username deve ter entre 3 e 50 caracteres")]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Senha do usuário
        /// </summary>
        /// <example>Senha@123</example>
        [Required(ErrorMessage = "A senha é obrigatória")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres")]
        public string Password { get; set; } = string.Empty;
    }
}
