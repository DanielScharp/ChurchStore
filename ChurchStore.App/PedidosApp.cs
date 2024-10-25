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

        public async Task<int> AdicionarItemAoPedido(AdicionarItemPedidoRequest request)
        {
            try
            {
                int estoqueRestante = 0;
                double valorPedido = request.Valor * request.Quantidade;
                int pedidoId = await _pedidosRepositorio.RetornaIdPedidoAberto(request.ClienteId);

                int estoqueProduto = await _pedidosRepositorio.RetornaQuantidadeProdutoNoEstoque(request.ProdutoId);

                if (estoqueProduto >= request.Quantidade)
                {
                    estoqueRestante = estoqueProduto - request.Quantidade;
                }
                else {
                    throw new Exception("Erro: Não há estoque suficiente!");
                }

                bool estoqueAlterado = await _pedidosRepositorio.AlterarQuantidadeEstoque(request.ProdutoId, estoqueRestante);

                if (estoqueAlterado) {
                    if (pedidoId > 0)
                    {
                        PedidoItem verificaSeProdutoJaEstaNoPedido = await _pedidosRepositorio.VerificaSeProdutoJaEstaNoPedido(pedidoId, request.ProdutoId);
                        var alteraQuantidade = verificaSeProdutoJaEstaNoPedido.Quantidade;
                        request.Quantidade = alteraQuantidade + request.Quantidade;

                        double valorAtual = await _pedidosRepositorio.RetornarValorPedido(pedidoId);
                        valorPedido = valorPedido + valorAtual;
                        _pedidosRepositorio.AlterarValorPedido(pedidoId, valorPedido);

                        if (verificaSeProdutoJaEstaNoPedido.ProdutoId > 0)
                        {
                            _pedidosRepositorio.AlterarQuantidadeItensPedido(pedidoId, request.ProdutoId, request.Quantidade);
                        }
                        else
                        {
                            _pedidosRepositorio.InserirPedidoItem(pedidoId, request.ClienteId, request.ProdutoId, request.Quantidade);
                        }

                    }
                    else
                    {
                        pedidoId = await _pedidosRepositorio.AdicionarPedido(request.ClienteId, valorPedido);

                        _pedidosRepositorio.InserirPedidoItem(pedidoId, request.ClienteId, request.ProdutoId, request.Quantidade);
                    }
                }
                

                return estoqueRestante;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> RemoverItemDoPedido(RemoverItemDoPedidoRequest request)
        {
            try
            {
                int qtdAtualProduto = await _pedidosRepositorio.RetornaQuantidadeProdutoNoEstoque(request.ProdutoId);

                int qtdProdutosRetorno = await _pedidosRepositorio.RetornaQuantidadeProdutoPorCliente(request.ClienteId, request.ProdutoId, request.PedidoId);

                int quantidadeTotalProduto = qtdAtualProduto + qtdProdutosRetorno;

                double valorPedido = qtdProdutosRetorno * request.ProdutoValor;
                double valorAtual = await _pedidosRepositorio.RetornarValorPedido(request.PedidoId);
                valorPedido = valorAtual - valorPedido;
                _pedidosRepositorio.AlterarValorPedido(request.PedidoId, valorPedido);


                bool quantidadeAlterada = await _pedidosRepositorio.AlterarQuantidadeEstoque(request.ProdutoId, quantidadeTotalProduto);

                if (quantidadeAlterada)
                {
                    return await _pedidosRepositorio.RemoverItemDoPedido(request.ClienteId, request.ProdutoId, request.PedidoId);
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
