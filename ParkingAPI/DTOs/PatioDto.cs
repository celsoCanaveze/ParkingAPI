using System.ComponentModel.DataAnnotations;

namespace ParkingApi.DTOs
{
    /// <summary>
    /// DTO para transferência de dados de Pátio
    /// </summary>
    public class PatioDto
    {
        /// <summary>
        /// ID único do pátio
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// Nome do pátio de estacionamento
        /// </summary>
        /// <example>Pátio Central</example>
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 100 caracteres")]
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Localização física do pátio
        /// </summary>
        /// <example>Rua das Flores, 123 - Centro</example>
        [Required(ErrorMessage = "A localização é obrigatória")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "A localização deve ter entre 5 e 200 caracteres")]
        public string Localizacao { get; set; } = string.Empty;
    }
}
