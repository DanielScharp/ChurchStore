using ChurchStore.Api;
using ChurchStore.App;
using ChurchStore.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChurchStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly PedidosApplication _pedidosApplication;

        public PedidosController(PedidosApplication pedidosApplication)
        {
            _pedidosApplication = pedidosApplication;
        }

        [Route("listar")]
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var listaUsuarios = await _pedidosApplication.Listar();
                return Ok(ResultMessage.Sucesso(0, listaUsuarios));
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        [Route("itens/listar")]
        [HttpGet]
        public async Task<IActionResult> ListarItens(int clienteId)
        {
            try
            {
                var listaUsuarios = await _pedidosApplication.ListarItensPorCliente(clienteId);
                return Ok(ResultMessage.Sucesso(0, listaUsuarios));
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        [Route("itens/listar-pedidoId")]
        [HttpGet]
        public async Task<IActionResult> ListarItensPorPedido(int pedidoId)
        {
            try
            {
                var listaUsuarios = await _pedidosApplication.ListarItensPorPedido(pedidoId);
                return Ok(listaUsuarios);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        [Route("itens/adicionar")]
        [HttpPost]
        public async Task<IActionResult> AdicionarItemAoPedido([FromBody] AdicionarItemPedidoRequest request)
        {
            try
            {
                var quantidadeRestanteEstoque = await _pedidosApplication.AdicionarItemAoPedido(request.ClienteId, request.ProdutoId, request.Quantidade);
                return Ok(new {Success = true, Data= quantidadeRestanteEstoque });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("itens/remover")]
        [HttpPost]
        public async Task<IActionResult> RemoverItemDoPedido(int clienteId, int produtoId, int pedidoId)
        {
            try
            {
                var removido = await _pedidosApplication.RemoverItemDoPedido(clienteId, produtoId, pedidoId);
                return Ok(removido);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        [Route("Alterar-status")]
        [HttpPut]
        public IActionResult AlterarStatusPedido(int pedidoId, int statusId)
        {
            try
            {
                _pedidosApplication.AlterarStatusPedido(pedidoId, statusId);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}
