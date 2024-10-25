using ChurchStore.Domain;
using ChurchStore.Web.Api;
using ChurchStore.Web.Models;
using ChurchStore.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ChurchStore.Web.Controllers
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
                var result = await _apiSender.ListarProdutos();

                List<Produto>? listaProdutos = new List<Produto>();

                if (result.Success)
                {
                    listaProdutos = JsonConvert.DeserializeObject<List<Produto>>(result.Data.ToString());
                }

                return View(listaProdutos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarItensNaSacola([FromBody] List<Produto> produtos)
        {
            try
            {

                var clienteId = ClienteSessao.Logado.UsuarioId;
                string mensagemRetorno = "";

                foreach (Produto produto in produtos)
                {
                    var result = await _apiSender.AdicionarItensNaSacola(clienteId, produto);
                    if (result.Success)
                    {
                        mensagemRetorno += " <span>" + produto.ProdutoNome + " adicionado ao carrinho! (unidades: " + produto.Quantidade + ")</span><br /> ";
                    }
                    else
                    {
                        mensagemRetorno += " <span style='color:#f00'>Não foi possível adicionar " + produto.ProdutoNome + "!</span><br />";
                        mensagemRetorno += " <span style='color:#f00'>" + result.Data + "</span><br />";
                    }
                }

                return Json(new { success = true, data = mensagemRetorno });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
