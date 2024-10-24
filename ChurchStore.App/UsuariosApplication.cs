using ChurchStore.Database.Repositorios;
using ChurchStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchStore.App
{
    public class UsuariosApplication
    {
        private readonly UsuariosRepositorio _usuarioRepositorio;
        public UsuariosApplication(UsuariosRepositorio leilaoRepositorio)
        {
            _usuarioRepositorio = leilaoRepositorio;
        }

        public async Task<Usuario> RetornarPorApelidoSenha(string apelido, string senha = "")
        {
            try
            {
                return await _usuarioRepositorio.RetornarPorApelidoSenha(apelido, senha);
            }
            catch
            {
                throw;
            }
        }
        public async Task<Usuario> RetornarPorApelidoEmail(string apelido, string email)
        {
            try
            {
                return await _usuarioRepositorio.RetornarPorApelidoEmail(apelido, email);
            }
            catch
            {
                throw;
            }
        }
        public void Cadastrar(Login user)
        {
            try
            {
                _usuarioRepositorio.Cadastrar(user);
            }
            catch
            {
                throw;
            }
        }
        public void Alterar(Usuario user)
        {
            try
            {
                _usuarioRepositorio.Alterar(user);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> AlterarSenha(Usuario user)
        {
            try
            {
                return await _usuarioRepositorio.AlterarSenha(user);
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<Usuario>> ListarUsuarios()
        {
            try
            {
                var lista = await _usuarioRepositorio.ListarUsuarios();
                return lista;
            }
            catch
            {
                throw;
            }
        }
    }
}

