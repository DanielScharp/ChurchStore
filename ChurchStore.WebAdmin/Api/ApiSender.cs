using ChurchStore.Domain;
using ChurchStore.WebAdmin.Services;

namespace ChurchStore.WebAdmin.Api
{
    public class ApiSender
    {
        private readonly IApiService _apiService;

        public ApiSender(IApiService apiService)
        {
            _apiService = apiService;
        }
        //-----------------------------------------Usuarios-----------------------------------------
        public async Task<ApiResponse> EfetuarLoginCliente(Login login)
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

        public async Task<ApiResponse> RecuperaSenhaCliente(Login login)
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
        //-----------------------------------------Pedidos-----------------------------------------
        public async Task<ApiResponse> ListarPedidos(Pedido filtroPedido)
        {

            try
            {
                var parameters = new Dictionary<string, string>
                {
                    {"pedidoId", filtroPedido.PedidoId.ToString()},
                    {"clienteNome", filtroPedido.ClienteNome},
                    {"statusId", filtroPedido.StatusId.ToString()},
                };

                var request = new ApiRequest
                {
                    RouteValue = "Pedidos/Listar",
                    Content = parameters,
                };

                return await _apiService.PostAsync(request);

            }
            catch
            {
                throw;
            }

        }
        public async Task<ApiResponse> ListarItensPedidos(int pedidoId)
        {

            try
            {
                var parameters = new Dictionary<string, string>
                {
                    {"pedidoId", pedidoId.ToString()},
                };

                var request = new ApiRequest
                {
                    RouteValue = "Pedidos/itens/listar-pedidoId",
                    Content = parameters,
                };

                return await _apiService.GetAsync(request);

            }
            catch
            {
                throw;
            }

        }

        public async Task<ApiResponse> AlterarStatusPedido(int pedidoId, int statusId)
        {

            try
            {
                var parameters = new Dictionary<string, string>
                {
                    {"pedidoId", pedidoId.ToString()},
                    {"statusId", statusId.ToString()}
                };

                var request = new ApiRequest
                {
                    RouteValue = "Pedidos/Alterar-status",
                    Content = parameters,
                };

                return await _apiService.PostAsync(request);

            }
            catch
            {
                throw;
            }

        }
        //-----------------------------------------Fim Pedidos-----------------------------------------
        //-----------------------------------------Produtos-----------------------------------------

        public async Task<ApiResponse> RetornarProduto(int id)
        {

            try
            {
                var parameters = new Dictionary<string, string>
                {
                    {"id", id.ToString()},
                };

                var request = new ApiRequest
                {
                    RouteValue = "Produtos/Retornar",
                    Content = parameters,
                };

                return await _apiService.GetAsync(request);

            }
            catch
            {
                throw;
            }

        }
        public async Task<ApiResponse> ListarProdutos()
        {

            try
            {
                var parameters = new Dictionary<string, string>
                {
                    {"publico", "false"},
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

    }
}
