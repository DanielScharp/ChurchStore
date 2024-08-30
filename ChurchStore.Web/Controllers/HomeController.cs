using ChurchStore.Domain;
using ChurchStore.Web.Api;
using ChurchStore.Web.Models;
using ChurchStore.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
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
        public IActionResult Index()
        {
            return View();
        }


        //Listar os leilões Destaques para montar os destaques na inicial
        [HttpGet]
        public async Task<IActionResult> ListaProdutos()
        {

            try
            {
                var result = await _apiSender.ListarProdutos();

                if (result.Success)
                {
                    var listaProdutos = JsonConvert.DeserializeObject<List<Produto>>(result.Data.ToString());
                    return View("_ListaProdutos", listaProdutos);
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
