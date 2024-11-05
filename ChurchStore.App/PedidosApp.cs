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

        public async Task<List<Pedido>> Listar(Pedido filtro)
        {
            try
            {
                return await _pedidosRepositorio.Listar(filtro);
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

        public async Task<Pedido> AdicionarItemAoPedido(PedidoItem request)
        {
            try
            {
                Pedido pedido = new Pedido();
                int estoqueRestante = 0;
                pedido.PedidoValor = request.ProdutoValor * request.Quantidade;
                pedido.PedidoId = await _pedidosRepositorio.RetornaIdPedidoAberto(request.ClienteId);

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
                    if (pedido.PedidoId > 0)
                    {
                        PedidoItem verificaSeProdutoJaEstaNoPedido = await _pedidosRepositorio.VerificaSeProdutoJaEstaNoPedido(pedido.PedidoId, request.ProdutoId);
                        var alteraQuantidade = verificaSeProdutoJaEstaNoPedido.Quantidade;
                        request.Quantidade = alteraQuantidade + request.Quantidade;

                        double valorAtual = await _pedidosRepositorio.RetornarValorPedido(pedido.PedidoId);
                        pedido.PedidoValor = pedido.PedidoValor + valorAtual;
                        _pedidosRepositorio.AlterarValorPedido(pedido.PedidoId, pedido.PedidoValor);

                        if (verificaSeProdutoJaEstaNoPedido.ProdutoId > 0)
                        {
                            _pedidosRepositorio.AlterarQuantidadeItensPedido(pedido.PedidoId, request.ProdutoId, request.Quantidade);
                        }
                        else
                        {
                            _pedidosRepositorio.InserirPedidoItem(pedido.PedidoId, request.ClienteId, request.ProdutoId, request.Quantidade);
                        }
                    }
                    else
                    {
                        pedido.PedidoId = await _pedidosRepositorio.AdicionarPedido(request.ClienteId, pedido.PedidoValor);


                        _pedidosRepositorio.InserirPedidoItem(pedido.PedidoId, request.ClienteId, request.ProdutoId, request.Quantidade);
                    }
                    pedido = await _pedidosRepositorio.Retornar(pedido.PedidoId);

                }

                return pedido;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Pedido> RemoverItemDoPedido(PedidoItem request)
        {
            try
            {
                Pedido pedido = new Pedido();
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
                     _pedidosRepositorio.RemoverItemDoPedido(request.ClienteId, request.ProdutoId, request.PedidoId);
                }

                pedido = await _pedidosRepositorio.Retornar(request.PedidoId);

                return pedido;

            }
            catch
            {
                throw;
            }
        }

        public async Task AlterarStatusPedido(int pedidoId, int statusId)
        {
            try
            {
                await _pedidosRepositorio.AlterarStatusPedido(pedidoId, statusId);
            }
            catch
            {
                throw;
            }
        }
    }
}
