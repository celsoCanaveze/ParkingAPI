using System.ComponentModel.DataAnnotations;

namespace ParkingApi.DTOs
{
    /// <summary>
    /// DTO para transferência de dados de Moto
    /// </summary>
    public class MotoDto
    {
        /// <summary>
        /// ID único da moto
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// Placa da moto
        /// </summary>
        /// <example>ABC-1234</example>
        [Required(ErrorMessage = "A placa é obrigatória")]
        [RegularExpression(@"^[A-Z]{3}-?\d{4}$", ErrorMessage = "Placa deve estar no formato ABC-1234 ou ABC1234")]
        public string Placa { get; set; } = string.Empty;

        /// <summary>
        /// Modelo da moto
        /// </summary>
        /// <example>Honda CB 600F</example>
        [Required(ErrorMessage = "O modelo é obrigatório")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "O modelo deve ter entre 2 e 50 caracteres")]
        public string Modelo { get; set; } = string.Empty;

        /// <summary>
        /// ID do cliente proprietário da moto
        /// </summary>
        /// <example>1</example>
        [Required(ErrorMessage = "O ID do cliente é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "ID do cliente deve ser maior que 0")]
        public int ClienteId { get; set; }
    }
}
