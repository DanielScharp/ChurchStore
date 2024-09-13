using ChurchStore.Domain;
using ChurchStore.Web.Api;
using ChurchStore.Web.Models;
using ChurchStore.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ChurchStore.Web.Controllers
{
    public class BagController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApiSender _apiSender;

        public BagController(IHttpContextAccessor httpContextAccessor, IApiService apiService)
        {
            _httpContextAccessor = httpContextAccessor;
            _apiSender = new ApiSender(apiService);

        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ListaProdutosBag()
        {

            try
            {
                var clienteId = ClienteSessao.Logado.UsuarioId;

                var result = await _apiSender.ListarProdutosBag(clienteId);

                if (result.Success)
                {
                    var listaProdutos = JsonConvert.DeserializeObject<List<PedidoItem>>(result.Data.ToString());
                    return View("_ListarProdutosBag", listaProdutos);
                }
                else
                {
                    return Json(new { success = false, message = result.Message });
                }


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
