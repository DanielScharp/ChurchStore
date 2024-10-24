using ChurchStore.Domain;
using ChurchStore.Web.Api;
using ChurchStore.Web.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace ChurchStore.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApiSender _apiSender;

        public LoginController(IHttpContextAccessor httpContextAccessor, IApiService apiService)
        {
            _httpContextAccessor = httpContextAccessor;
            _apiSender = new ApiSender(apiService);

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> In(Login login)
        {

            try
            {

                //Atribue o IP da origem, Verificar se vem do Cliente 
                login.IpOrigem = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                login.AppOrigem = "NSC";

                var result = await _apiSender.EfetuarLoginCliente(login);

                if (result.Success)
                {
                    var loginResult = JsonConvert.DeserializeObject<LoginResult>(result.Data.ToString());

                    if (loginResult.UsuarioId > 0)
                    {

                        var userClaims = new List<Claim>()
                        {
                            new Claim("UsuarioId", loginResult.UsuarioId.ToString()),
                            new Claim("Apelido", loginResult.Apelido),
                            new Claim(ClaimTypes.Role, loginResult.Role),
                            new Claim("TokenApi", loginResult.Token)
                        };

                        var myIdentity = new ClaimsIdentity(userClaims, "Admin");
                        var userPrincipal = new ClaimsPrincipal(new[] { myIdentity });

                        //Cria o cookie
                        _ = _httpContextAccessor.HttpContext.SignInAsync(userPrincipal);

                        //Inicializa o helper para usar em uma classe estática *** INICIA NA PROGRAM.CS **** após o builder.Build();
                        //HttpContextAccessorHelper.Initialize(_httpContextAccessor);

                        return Json(1);

                    }
                    else
                    {
                        return Json("Cliente não localizado com os dados fornecidos!");
                    }
                }

                //Mostra a mensagem de Erro e o Status para mostrar na view
                var errorMessage = (result.Data == null ? result.Message : JsonConvert.DeserializeObject<Dictionary<string, string>>(result.Data.ToString())["error"]);
                return Json(errorMessage);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IActionResult SolicitarNovaSenha()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RecuperaSenhaCliente(Login login)
        {
            try
            {
                //Atribue o IP da origem, Verificar se vem do Cliente 
                login.IpOrigem = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                login.AppOrigem = "NSC";

                var result = await _apiSender.RecuperaSenhaCliente(login);

                if (result.Success)
                {
                    return Json(result);
                }

                //Mostra a mensagem de Erro e o Status para mostrar na view
                var errorMessage = (result.Data == null ? result.Message : JsonConvert.DeserializeObject<Dictionary<string, string>>(result.Data.ToString())["error"]);
                return Json(errorMessage);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        public IActionResult AcessoNegado()
        {
            return View();
        }


        public IActionResult Logoff()
        {
            try
            {
                if (_httpContextAccessor.HttpContext != null) _httpContextAccessor.HttpContext.SignOutAsync();

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

