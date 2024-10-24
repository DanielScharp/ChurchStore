using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchStore.Domain
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        public string? ProdutoNome { get; set; }
        public string? ImagemUrl { get; set; }
        public double ProdutoValor { get; set; }
        public int Quantidade { get; set; }
        public bool Exibir { get; set; }
    }
}
