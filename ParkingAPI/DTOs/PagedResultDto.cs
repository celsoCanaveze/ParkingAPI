using System.ComponentModel.DataAnnotations;

namespace ParkingApi.DTOs
{
    /// <summary>
    /// Resultado paginado genérico
    /// </summary>
    /// <typeparam name="T">Tipo dos dados retornados</typeparam>
    public class PagedResultDto<T>
    {
        /// <summary>
        /// Lista de dados da página atual
        /// </summary>
        public IEnumerable<T> Data { get; set; } = new List<T>();

        /// <summary>
        /// Número da página atual (base 1)
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Tamanho da página
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Total de registros
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Total de páginas
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Indica se há página anterior
        /// </summary>
        public bool HasPrevious { get; set; }

        /// <summary>
        /// Indica se há próxima página
        /// </summary>
        public bool HasNext { get; set; }

        /// <summary>
        /// Links de navegação HATEOAS
        /// </summary>
        public IDictionary<string, string> Links { get; set; } = new Dictionary<string, string>();
    }

    /// <summary>
    /// Parâmetros de paginação
    /// </summary>
    public class PaginationParameters
    {
        private const int MaxPageSize = 50;
        private int _pageSize = 10;

        /// <summary>
        /// Número da página (padrão: 1)
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "O número da página deve ser maior que 0")]
        public int Page { get; set; } = 1;

        /// <summary>
        /// Tamanho da página (padrão: 10, máximo: 50)
        /// </summary>
        [Range(1, MaxPageSize, ErrorMessage = "O tamanho da página deve estar entre 1 e 50")]
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
    }
}