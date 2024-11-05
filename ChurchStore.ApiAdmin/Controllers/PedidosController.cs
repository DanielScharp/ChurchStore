using ChurchStore.App;
using ChurchStore.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChurchStore.ApiAdmin.Controllers
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
        [HttpPost]
        public async Task<IActionResult> Listar([FromBody] Pedido filtroPedido)
        {
            try
            {
                var listaUsuarios = await _pedidosApplication.Listar(filtroPedido);
                return Ok(ResultMessage.Sucesso(0, listaUsuarios));
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Data = ex.Message });
            }
        }

        [Route("itens/listar-pedidoId")]
        [HttpGet]
        public async Task<IActionResult> ListarItensPorPedido(int pedidoId)
        {
            try
            {
                var itensPedido = await _pedidosApplication.ListarItensPorPedido(pedidoId);
                return Ok(new { Success = true, Data = itensPedido });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Data = ex.Message });
            }
        }

        [Route("Alterar-status")]
        [HttpPost]
        public IActionResult AlterarStatusPedido([FromBody] Pedido pedido)
        {
            try
            {
                _ = _pedidosApplication.AlterarStatusPedido(pedido.PedidoId, pedido.StatusId);
                return Ok(new { Success = true, Message = "Status atualizado com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Data = ex.Message });
            }
        }
    }
}
