using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchStore.Domain
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string? Nome { get; set; }
        public string? Role { get; set; }
        public string? Email { get; set; }
        public string? Apelido { get; set; }
        public string? Senha { get; set; }
        public string? Token { get; set; }
    }
}
