using ChurchStore.App;
using ChurchStore.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChurchStore.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuariosApplication _usuarioApplication;

        public UsuariosController(UsuariosApplication usuarioApplication)
        {
            _usuarioApplication = usuarioApplication;
        }

        [Route("retornar")]
        [HttpGet]
        public async Task<IActionResult> Retornar(string apelido, string senha = "")
        {
            try
            {
                var listaUsuarios = await _usuarioApplication.RetornarPorApelidoSenha(apelido, senha);
                return Ok(listaUsuarios);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("listar")]
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var listaUsuarios = await _usuarioApplication.ListarUsuarios();
                return Ok(listaUsuarios);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("cadastrar")]
        [HttpPost]
        public void Cadastrar(Login user)
        {
            try
            {
                 _usuarioApplication.Cadastrar(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("Alterar")]
        [HttpPut]
        public void Alterar(Usuario? user)
        {
            try
            {
                 _usuarioApplication.Alterar(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
