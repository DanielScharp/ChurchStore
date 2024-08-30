using ChurchStore.Api;
using ChurchStore.App;
using ChurchStore.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;

namespace ChurchStore.API.Controllers
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
                var listaUsuarios = await _produtosApplication.Retornar(id);
                return Ok(listaUsuarios);
            }
            catch (Exception ex)
            {
                throw ex;

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
                throw ex;

            }
        }

        [Route("adicionar")]
        [HttpPost]
        public async Task<IActionResult> AdicionarProduto(Produto produto)
        {
            try
            {
                var listaUsuarios = await _produtosApplication.AdicionarProduto(produto);
                return Ok(listaUsuarios);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        [Route("alterar")]
        [HttpPost]
        public async Task<IActionResult> AlterarProduto(Produto produto)
        {
            try
            {
                var listaUsuarios = await _produtosApplication.AlterarProduto(produto);
                return Ok(listaUsuarios);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}
