using ChurchStore.Domain;
using ChurchStore.Web.Api;
using ChurchStore.Web.Models;
using ChurchStore.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ChurchStore.Web.Controllers
{
    [Authorize]
    public class BagController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApiSender _apiSender;

        public BagController(IHttpContextAccessor httpContextAccessor, IApiService apiService)
        {
            _httpContextAccessor = httpContextAccessor;
            _apiSender = new ApiSender(apiService);

        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var clienteId = ClienteSessao.Logado.UsuarioId;

                var result = await _apiSender.ListarProdutosBag(clienteId);

                List<PedidoItem> listaProdutos = new List<PedidoItem>();
                if (result.Success)
                {
                    listaProdutos = JsonConvert.DeserializeObject<List<PedidoItem>>(result.Data.ToString());
                }
                return View(listaProdutos);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> RemoverProdutosBag(int produtoId, int pedidoId)
        {

            try
            {
                var clienteId = ClienteSessao.Logado.UsuarioId;

                var result = await _apiSender.RemoverProdutosBag(clienteId, produtoId, pedidoId);

                if (result.Success)
                {
                    return Json(new { success = true, message = "O produto foi removido do carrinho com sucesso!" });
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
