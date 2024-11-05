using ChurchStore.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChurchStore.ApiAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly ProdutosApplication _produtosApplication;

        public ProdutosController(ProdutosApplication produtosApplication)
        {
            _produtosApplication = produtosApplication;
        }

        [Route("retornar")]
        [HttpGet]
        public async Task<IActionResult> Retornar(int id)
        {
            try
            {
                var produto = await _produtosApplication.Retornar(id);
                return Ok(ResultMessage.Sucesso(id, produto));
            }
            catch (Exception ex)
            {
                return BadRequest(ResultMessage.Erro(ex));
            }
        }

        [Route("listar")]
        [HttpGet]
        public async Task<IActionResult> Listar(bool publico)
        {
            try
            {
                var listaUsuarios = await _produtosApplication.Listar(publico);
                return Ok(ResultMessage.Sucesso(0, listaUsuarios));
            }
            catch (Exception ex)
            {
                return BadRequest(ResultMessage.Erro(ex));
            }
        }
    }
}
