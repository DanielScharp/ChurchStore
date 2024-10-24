using ChurchStore.Database.Repositorios;
using ChurchStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchStore.App
{
    public class PedidosApplication
    {
        private readonly PedidosRepositorio _pedidosRepositorio;
        public PedidosApplication(PedidosRepositorio pedidosRepositorio)
        {
            _pedidosRepositorio = pedidosRepositorio;
        }

        public async Task<List<Pedido>> Listar()
        {
            try
            {
                return await _pedidosRepositorio.Listar();
            }
            catch
            {
                throw;
            }
        }


        public async Task<List<PedidoItem>> ListarItensPorCliente(int clienteId)
        {
            try
            {
                return await _pedidosRepositorio.ListarItensPorCliente(clienteId);
            }
            catch
            {
                throw;
            }
        }
        

        public async Task<List<PedidoItem>> ListarItensPorPedido(int pedidoId)
        {
            try
            {
                return await _pedidosRepositorio.ListarItensPorPedido(pedidoId);
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> AdicionarItemAoPedido(int clienteId, int produtoId, int quantidade)
        {
            try
            {
                int estoqueRestante = 0;
                int pedidoId = await _pedidosRepositorio.RetornaIdPedidoAberto(clienteId);

                int estoqueProduto = await _pedidosRepositorio.RetornaQuantidadeProdutoNoEstoque(produtoId);

                if (estoqueProduto >= quantidade)
                {
                    estoqueRestante = estoqueProduto - quantidade;
                }
                else {
                    throw new Exception("Erro: Não há estoque suficiente!");
                }

                _pedidosRepositorio.AlterarQuantidadeEstoque(produtoId, estoqueRestante);

                if (pedidoId > 0)
                {
                    PedidoItem verificaSeProdutoJaEstaNoPedido = await _pedidosRepositorio.VerificaSeProdutoJaEstaNoPedido(pedidoId, produtoId);
                    var alteraQuantidade = verificaSeProdutoJaEstaNoPedido.Quantidade;
                    quantidade = alteraQuantidade + quantidade;
                    if (verificaSeProdutoJaEstaNoPedido.ProdutoId > 0)
                    {
                        _pedidosRepositorio.AlterarQuantidadeItensPedido(pedidoId, produtoId, quantidade);
                    }
                    else
                    {
                        _pedidosRepositorio.InserirPedidoItem(pedidoId, clienteId, produtoId, quantidade);
                    }
                }
                else
                {
                    pedidoId = await _pedidosRepositorio.AdicionarPedido(clienteId);

                    _pedidosRepositorio.InserirPedidoItem(pedidoId, clienteId, produtoId, quantidade);
                }

                return estoqueRestante;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> RemoverItemDoPedido(int clienteId, int produtoId, int pedidoId)
        {
            try
            {
                int qtdAtualProduto = await _pedidosRepositorio.RetornaQuantidadeProdutoNoEstoque(produtoId);

                int qtdProdutosRetorno = await _pedidosRepositorio.RetornaQuantidadeProdutoPorCliente(clienteId, produtoId, pedidoId);

                int quantidadeTotalProduto = qtdAtualProduto + qtdProdutosRetorno;

                bool quantidadeAlterada = await _pedidosRepositorio.AlterarQuantidadeEstoque(produtoId, quantidadeTotalProduto);

                if (quantidadeAlterada)
                {
                    return await _pedidosRepositorio.RemoverItemDoPedido(clienteId, produtoId, pedidoId);
                }
                else
                {
                    return false;
                }

            }
            catch
            {
                throw;
            }
        }
        public async void AlterarStatusPedido(int pedidoId, int statusId)
        {
            try
            {
                _pedidosRepositorio.AlterarStatusPedido(pedidoId, statusId);
            }
            catch
            {
                throw;
            }
        }
    }
}
