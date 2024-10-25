using ChurchStore.Domain;
using ChurchStore.WebAdmin.Api;
using ChurchStore.WebAdmin.Models;
using ChurchStore.WebAdmin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ChurchStore.WebAdmin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApiSender _apiSender;

        public HomeController(IHttpContextAccessor httpContextAccessor, IApiService apiService)
        {
            _httpContextAccessor = httpContextAccessor;
            _apiSender = new ApiSender(apiService);

        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var result = await _apiSender.ListarPedidos();

                List<Pedido> listaPedidos = new List<Pedido>();
                if (result.Success)
                {
                    listaPedidos = JsonConvert.DeserializeObject<List<Pedido>>(result.Data.ToString());
                }
                return View(listaPedidos);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> ListarItensPedido(int pedidoId, string status, string dataAbreviada)
        {
            try
            {
                var result = await _apiSender.ListarItensPedidos(pedidoId);
                List<PedidoItem> listaItens = new List<PedidoItem>();

                if (result.Success)
                {
                    listaItens = JsonConvert.DeserializeObject<List<PedidoItem>>(result.Data.ToString());
                }
                ViewBag.Status = status;
                ViewBag.Data = dataAbreviada;

                return View("_ListarItensPedido", listaItens);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
