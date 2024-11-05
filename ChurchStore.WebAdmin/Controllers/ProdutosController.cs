using ChurchStore.Domain;
using ChurchStore.WebAdmin.Api;
using ChurchStore.WebAdmin.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ChurchStore.WebAdmin.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApiSender _apiSender;

        public ProdutosController(IHttpContextAccessor httpContextAccessor, IApiService apiService)
        {
            _httpContextAccessor = httpContextAccessor;
            _apiSender = new ApiSender(apiService);

        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var result = await _apiSender.ListarProdutos();

                List<Produto> listaProdutos = new List<Produto>();
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

        public async Task<IActionResult> RetornarProduto(int id)
        {
            try
            {
                var result = await _apiSender.RetornarProduto(id);

                Produto produto = new Produto();

                if (result.Success)
                {
                    produto = JsonConvert.DeserializeObject<Produto>(result.Data.ToString());
                }

                return View("_RetornarProduto", produto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
