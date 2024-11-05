using ChurchStore.Api;
using ChurchStore.Api.Hubs;
using ChurchStore.App;
using ChurchStore.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChurchStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly PedidosApplication _pedidosApplication;
        private readonly IHubContext<PedidoHub> _hubContext;
        public PedidosController(PedidosApplication pedidosApplication, IHubContext<PedidoHub> hubContext)
        {
            _pedidosApplication = pedidosApplication;
            _hubContext = hubContext;
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
                return BadRequest(new { Success = false, Data = ex.Message });
            }
        }

        [Route("itens/adicionar")]
        [HttpPost]
        public async Task<IActionResult> AdicionarItemAoPedido([FromBody] PedidoItem request)
        {
            try
            {
                Pedido pedido = await _pedidosApplication.AdicionarItemAoPedido(request);

                // Enviar notificação para o painel do administrador
                await _hubContext.Clients.All.SendAsync("NovoPedido", new { Success = true, Message = "AlterPedido", Data = pedido });

                return Ok(new {Success = true, Data = pedido });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Data = ex.Message });
            }
        }

        [Route("itens/remover")]
        [HttpPost]
        public async Task<IActionResult> RemoverItemDoPedido([FromBody] PedidoItem request)
        {
            try
            {
                var pedido = await _pedidosApplication.RemoverItemDoPedido(request);

                await _hubContext.Clients.All.SendAsync("NovoPedido", new { Success = true, Message = "RemovPedido", Data = pedido });

                return Ok(new { Success = true, Data = pedido });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Data = ex.Message });
            }
        }

    }
}
