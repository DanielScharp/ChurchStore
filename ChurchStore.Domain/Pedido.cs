using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchStore.Domain
{
    public class Pedido
    {
        public int PedidoId { get; set; }
        public int ClienteId { get; set; }
        public string? ClienteNome { get; set; }
        public string? ClienteTel { get; set; }
        public int StatusId { get; set; }
        public string? StatusNome { get; set; }
        public DateTime PedidoData { get; set; }
        public double PedidoValor { get; set; }
    }

    public class PedidoItem
    {
        public int ItemId { get; set; }
        public int PedidoId { get; set; }
        public int ClienteId { get; set; }
        public string? ClienteNome { get; set; }
        public int ProdutoId { get; set; }
        public string? ProdutoNome { get; set; }
        public double ProdutoValor { get; set; }
        public string? ImagemUrl { get; set; }
        public int Quantidade { get; set; }
        public double Total { get; set; }

    }
}
