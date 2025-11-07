using Microsoft.AspNetCore.Mvc;
using ParkingApi.DTOs;

namespace ParkingApi.Services
{
    /// <summary>
    /// Serviço para geração de links HATEOAS
    /// </summary>
    public interface IHateoasService
    {
        /// <summary>
        /// Gera links de paginação
        /// </summary>
        IDictionary<string, string> GeneratePaginationLinks(string controllerName, int currentPage, int pageSize, int totalPages, bool hasNext, bool hasPrevious);

        /// <summary>
        /// Gera links para uma entidade específica
        /// </summary>
        IDictionary<string, string> GenerateEntityLinks(string controllerName, int id);
    }

    /// <summary>
    /// Implementação do serviço HATEOAS
    /// </summary>
    public class HateoasService : IHateoasService
    {
        private readonly IUrlHelper _urlHelper;

        public HateoasService(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        public IDictionary<string, string> GeneratePaginationLinks(string controllerName, int currentPage, int pageSize, int totalPages, bool hasNext, bool hasPrevious)
        {
            var links = new Dictionary<string, string>
            {
                ["self"] = _urlHelper.Action("GetAll", controllerName, new { page = currentPage, pageSize }) ?? "",
                ["first"] = _urlHelper.Action("GetAll", controllerName, new { page = 1, pageSize }) ?? "",
                ["last"] = _urlHelper.Action("GetAll", controllerName, new { page = totalPages, pageSize }) ?? ""
            };

            if (hasPrevious)
            {
                links["prev"] = _urlHelper.Action("GetAll", controllerName, new { page = currentPage - 1, pageSize }) ?? "";
            }

            if (hasNext)
            {
                links["next"] = _urlHelper.Action("GetAll", controllerName, new { page = currentPage + 1, pageSize }) ?? "";
            }

            return links;
        }

        public IDictionary<string, string> GenerateEntityLinks(string controllerName, int id)
        {
            return new Dictionary<string, string>
            {
                ["self"] = _urlHelper.Action("Get", controllerName, new { id }) ?? "",
                ["update"] = _urlHelper.Action("Put", controllerName, new { id }) ?? "",
                ["delete"] = _urlHelper.Action("Delete", controllerName, new { id }) ?? "",
                ["all"] = _urlHelper.Action("GetAll", controllerName) ?? ""
            };
        }
    }
}