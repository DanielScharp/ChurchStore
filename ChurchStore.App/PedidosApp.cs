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
                return await _pedidosRepositorio.AdicionarItemAoPedido( clienteId, produtoId, quantidade);
                 
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
                return await _pedidosRepositorio.RemoverItemDoPedido(clienteId, produtoId, pedidoId);
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
