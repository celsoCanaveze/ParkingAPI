using System.ComponentModel.DataAnnotations;

namespace ParkingAPI.DTOs
{
    /// <summary>
    /// DTO para transferência de dados de Cliente
    /// </summary>
    public class ClienteDto
    {
        /// <summary>
        /// ID único do cliente
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// Nome completo do cliente
        /// </summary>
        /// <example>João Silva</example>
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 100 caracteres")]
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// CPF do cliente (apenas números)
        /// </summary>
        /// <example>12345678901</example>
        [Required(ErrorMessage = "O CPF é obrigatório")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "CPF deve conter exatamente 11 dígitos")]
        public string CPF { get; set; } = string.Empty;
    }
}
