using ChurchStore.Domain;
using ChurchStore.Web.Services;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace ChurchStore.Web.Api
{
    public class ApiSender
    {
        private readonly IApiService _apiService;

        public ApiSender(IApiService apiService)
        {
            _apiService = apiService;
        }

        //-----------------------------------------Usuarios-----------------------------------------
        public async Task<ApiResponse> EfetuarLoginCliente(Domain.Login login)
        {

            try
            {
                var parameters = new Dictionary<string, string>
                {
                    {"apelido", login.Apelido},
                    {"password", login.Password},
                    {"ipOrigem", login.IpOrigem},
                    {"appOrigem", login.AppOrigem},
                };

                var request = new ApiRequest
                {
                    RouteValue = "Login/in",
                    Content = parameters,
                };

                return await _apiService.PostAsync(request);

            }
            catch
            {
                throw;
            }

        }

        public async Task<ApiResponse> RecuperaSenhaCliente(Domain.Login login)
        {

            try
            {
                var parameters = new Dictionary<string, string>
                {
                    {"email", login.Email},
                    {"apelido", login.Apelido},
                    {"ipOrigem", login.IpOrigem},
                    {"appOrigem", login.AppOrigem},
                };

                var leilaoRequest = new ApiRequest
                {
                    RouteValue = "Login/reenviar-senha",
                    Content = parameters,
                };

                return await _apiService.PostAsync(leilaoRequest);

            }
            catch
            {
                throw;
            }
        }
        //-----------------------------------------Usuarios-----------------------------------------
        public async Task<ApiResponse> ListarProdutos()
        {

            try
            {
                var parameters = new Dictionary<string, string>
                {
                    {"publico", "true"},
                };

                var request = new ApiRequest
                {
                    RouteValue = "Produtos/Listar",
                    Content = parameters,
                };

                return await _apiService.GetAsync(request);

            }
            catch
            {
                throw;
            }

        }
    }
}
