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
        //-----------------------------------------FIM Usuarios-----------------------------------------
        //-----------------------------------------Produtos-----------------------------------------
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
        //-----------------------------------------Fim Produtos-----------------------------------------
        //-----------------------------------------BAG-----------------------------------------
        public async Task<ApiResponse> ListarProdutosBag(int clienteId)
        {

            try
            {
                var parameters = new Dictionary<string, string>
                {
                    {"clienteId", clienteId.ToString()},
                };

                var request = new ApiRequest
                {
                    RouteValue = "Pedidos/itens/Listar",
                    Content = parameters,
                };

                return await _apiService.GetAsync(request);

            }
            catch
            {
                throw;
            }

        }
        public async Task<ApiResponse> RemoverProdutosBag(int clienteId, int produtoId, int pedidoId, double produtoValor)
        {

            try
            {
                var parameters = new Dictionary<string, string>
                {
                    {"clienteId", clienteId.ToString()},
                    {"produtoId", produtoId.ToString()},
                    {"pedidoId", pedidoId.ToString()},
                    {"produtoValor", produtoValor.ToString().Replace(",", ".")},
                };

                var request = new ApiRequest
                {
                    RouteValue = "Pedidos/itens/remover",
                    Content = parameters,
                };

                return await _apiService.PostAsync(request);

            }
            catch
            {
                throw;
            }

        }
        //-----------------------------------------Fim BAG-----------------------------------------
        //-----------------------------------------Pedidos-----------------------------------------
        public async Task<ApiResponse> AdicionarItensNaSacola(int clienteId, Produto produto)
        {

            try
            {
                var parameters = new Dictionary<string, string>
                {
                    {"clienteId", clienteId.ToString()},
                    {"produtoId", produto.ProdutoId.ToString()},
                    {"quantidade", produto.Quantidade.ToString()},
                    {"produtoValor", produto.ProdutoValor.ToString().Replace(",", ".")},
                };

                var request = new ApiRequest
                {
                    RouteValue = "Pedidos/itens/adicionar",
                    Content = parameters,
                };

                return await _apiService.PostAsync(request);
            }
            catch
            {
                throw;
            }

        }
        //-----------------------------------------FimPedidos-----------------------------------------


    }
}
