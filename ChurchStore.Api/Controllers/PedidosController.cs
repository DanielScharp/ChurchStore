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

        [Route("itens/adicionar")]
        [HttpPost]
        public async Task<IActionResult> AdicionarItemAoPedido([FromBody] AdicionarItemPedidoRequest request)
        {
            try
            {
                var quantidadeRestanteEstoque = await _pedidosApplication.AdicionarItemAoPedido(request);
                return Ok(new {Success = true, Data= quantidadeRestanteEstoque });
            }
            catch (Exception ex)
            {
                // Retorna a mensagem do erro no campo Data
                return BadRequest(new { Success = false, Data = ex.Message });
            }
        }

        [Route("itens/remover")]
        [HttpPost]
        public async Task<IActionResult> RemoverItemDoPedido([FromBody] RemoverItemDoPedidoRequest request)
        {
            try
            {
                var removido = await _pedidosApplication.RemoverItemDoPedido(request);

                if (removido)
                {
                    return Ok(new { Success = true, Data = removido });
                }
                else
                {
                    return BadRequest(new { Success = false, Data = removido });
                }
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
