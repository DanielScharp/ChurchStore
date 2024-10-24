using ChurchStore.Database.Repositorios;
using ChurchStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchStore.App
{
    public class ProdutosApplication
    {
        private readonly ProdutosRepositorio _produtosRepositorio;
        public ProdutosApplication(ProdutosRepositorio produtosRepositorio)
        {
            _produtosRepositorio = produtosRepositorio;
        }

        public async Task<Produto> Retornar(int id)
        {
            try
            {
                return await _produtosRepositorio.Retornar(id);

            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Produto>> Listar(bool publico)
        {
            try
            {
                return await _produtosRepositorio.Listar(publico);

            }
            catch
            {
                throw;
            }
        }

        public async Task<Produto> AdicionarProduto(Produto produto)
        {
            try
            {
                return await _produtosRepositorio.AdicionarProduto(produto);

            }
            catch
            {
                throw;
            }
        }

        public async Task<Produto> AlterarProduto(Produto produto)
        {
            try
            {
                return await _produtosRepositorio.AlterarProduto(produto);

            }
            catch
            {
                throw;
            }
        }
    }
}
