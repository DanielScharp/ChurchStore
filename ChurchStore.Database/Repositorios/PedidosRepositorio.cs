using ChurchStore.Domain;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchStore.Database.Repositorios
{
    public class PedidosRepositorio
    {
        private readonly string _connMySql;

        public PedidosRepositorio(string connMySql)
        {
            _connMySql = connMySql;
        }

        public async Task<List<Pedido>> Retornar()
        {
            try
            {
                using (var conn = new MySqlConnection(_connMySql))
                {
                    await conn.OpenAsync();

                    var sql = new StringBuilder();
                    sql.Append(" SELECT t1.*, t2.Status, T3.* ");
                    sql.Append(" FROM pedidos t1");
                    sql.Append(" LEFT JOIN pedidos_status t2 ON t2.StatusId = t1.StatusId ");
                    sql.Append(" LEFT JOIN clientes t3 ON t3.ClienteId = t1.ClienteId ");

                    using MySqlCommand command = new(sql.ToString(), conn);

                    using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();

                    var pedidos = new List<Pedido>();

                    while (reader.Read())
                    {
                        var pedido = new Pedido();

                        pedido.PedidoId = reader.GetInt32(reader.GetOrdinal("PedidoId"));
                        pedido.ClienteId = reader.GetInt32(reader.GetOrdinal("ClienteId"));
                        pedido.ClienteNome = reader[reader.GetOrdinal("ClienteNome")].ToString();
                        pedido.ClienteTel = reader[reader.GetOrdinal("Telefone")].ToString();
                        pedido.StatusId = reader.GetInt32(reader.GetOrdinal("StatusId"));
                        pedido.StatusNome = reader[reader.GetOrdinal("Status")].ToString();
                        pedido.PedidoData = reader[reader.GetOrdinal("PedidoData")] != DBNull.Value ? reader.GetDateTime(reader.GetOrdinal("PedidoData")) : new DateTime();
                        pedido.PedidoValor = reader[reader.GetOrdinal("PedidoValor")] != DBNull.Value ? reader.GetDouble(reader.GetOrdinal("PedidoValor")) : 0;

                        pedidos.Add(pedido);
                    }

                    return pedidos;
                }
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<Pedido>> Listar()
        {
            try
            {
                using (var conn = new MySqlConnection(_connMySql))
                {
                    await conn.OpenAsync();

                    var sql = new StringBuilder();
                    sql.Append(" SELECT t1.*, t2.Status, t3.* ");
                    sql.Append(" FROM pedidos t1");
                    sql.Append(" LEFT JOIN pedidos_status t2 ON t2.StatusId = t1.StatusId ");
                    sql.Append(" LEFT JOIN usuarios t3 ON t3.UsuarioId = t1.ClienteId ");

                    using MySqlCommand command = new(sql.ToString(), conn);

                    using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();

                    var pedidos = new List<Pedido>();

                    while (reader.Read())
                    {
                        var pedido = new Pedido();

                        pedido.PedidoId = reader.GetInt32(reader.GetOrdinal("PedidoId"));
                        pedido.ClienteId = reader.GetInt32(reader.GetOrdinal("UsuarioId"));
                        pedido.ClienteNome = reader[reader.GetOrdinal("Nome")].ToString();
                        pedido.ClienteTel = reader[reader.GetOrdinal("Telefone")].ToString();
                        pedido.StatusId = reader.GetInt32(reader.GetOrdinal("StatusId"));
                        pedido.StatusNome = reader[reader.GetOrdinal("Status")].ToString();
                        pedido.PedidoData = reader[reader.GetOrdinal("PedidoData")] != DBNull.Value ? reader.GetDateTime(reader.GetOrdinal("PedidoData")) : new DateTime();
                        pedido.PedidoValor = reader[reader.GetOrdinal("PedidoValor")] != DBNull.Value ? reader.GetDouble(reader.GetOrdinal("PedidoValor")) : 0;

                        pedidos.Add(pedido);
                    }

                    return pedidos;
                }
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
                using (var conn = new MySqlConnection(_connMySql))
                {
                    await conn.OpenAsync();

                    var sql = new StringBuilder();
                    sql.Append(" SELECT t1.ProdutoId, t1.ClienteId, t1.PedidoId, SUM(t1.Quantidade) as 'Quantidade', SUM(t1.Quantidade * t2.ProdutoValor) as 'Total', ");
                    sql.Append(" t2.ProdutoNome, t2.ProdutoValor, t2.ImagemUrl, t3.Nome ");
                    sql.Append(" FROM pedidos_itens t1 ");
                    sql.Append(" left join produtos t2 on t2.ProdutoId = t1.ProdutoId ");
                    sql.Append(" LEFT JOIN usuarios t3 ON t3.UsuarioId = t1.ClienteId ");
                    sql.AppendFormat(" where t1.ClienteId ='{0}' ", clienteId);
                    sql.Append(" GROUP BY t1.PedidoId, t1.ProdutoId");

                    using MySqlCommand command = new(sql.ToString(), conn);

                    using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();

                    var pedidos = new List<PedidoItem>();

                    while (reader.Read())
                    {
                        var pedido = new PedidoItem();

                        pedido.ProdutoId = reader.GetInt32(reader.GetOrdinal("ProdutoId"));
                        pedido.PedidoId = reader.GetInt32(reader.GetOrdinal("PedidoId"));
                        pedido.ClienteId = reader.GetInt32(reader.GetOrdinal("ClienteId"));
                        pedido.ClienteNome = reader[reader.GetOrdinal("Nome")].ToString();
                        pedido.Quantidade = reader.GetInt32(reader.GetOrdinal("Quantidade"));
                        pedido.ProdutoNome = reader[reader.GetOrdinal("ProdutoNome")].ToString();
                        pedido.ImagemUrl = reader[reader.GetOrdinal("ImagemUrl")].ToString();
                        pedido.ProdutoValor = reader[reader.GetOrdinal("ProdutoValor")] != DBNull.Value ? reader.GetDouble(reader.GetOrdinal("ProdutoValor")) : 0;
                        pedido.Total = reader[reader.GetOrdinal("Total")] != DBNull.Value ? reader.GetDouble(reader.GetOrdinal("Total")) : 0;

                        pedidos.Add(pedido);
                    }

                    return pedidos;
                }
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
                using (var conn = new MySqlConnection(_connMySql))
                {
                    await conn.OpenAsync();

                    var sql = new StringBuilder();
                    sql.Append(" SELECT t1.ProdutoId, t1.ClienteId, t1.PedidoId,t1.Quantidade, ");
                    sql.Append(" t2.ProdutoNome, t2.ProdutoValor, t2.ImagemUrl, t3.Nome ");
                    sql.Append(" FROM pedidos_itens t1 ");
                    sql.Append(" left join produtos t2 on t2.ProdutoId = t1.ProdutoId ");
                    sql.Append(" LEFT JOIN usuarios t3 ON t3.UsuarioId = t1.ClienteId ");
                    sql.AppendFormat(" where t1.PedidoId ='{0}' ", pedidoId);

                    using MySqlCommand command = new(sql.ToString(), conn);

                    using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();

                    var pedidos = new List<PedidoItem>();

                    while (reader.Read())
                    {
                        var pedido = new PedidoItem();

                        pedido.ProdutoId = reader.GetInt32(reader.GetOrdinal("ProdutoId"));
                        pedido.PedidoId = reader.GetInt32(reader.GetOrdinal("PedidoId"));
                        pedido.ClienteId = reader.GetInt32(reader.GetOrdinal("ClienteId"));
                        pedido.ClienteNome = reader[reader.GetOrdinal("Nome")].ToString();
                        pedido.Quantidade = reader.GetInt32(reader.GetOrdinal("Quantidade"));
                        pedido.ProdutoNome = reader[reader.GetOrdinal("ProdutoNome")].ToString();
                        pedido.ImagemUrl = reader[reader.GetOrdinal("ImagemUrl")].ToString();
                        pedido.ProdutoValor = reader[reader.GetOrdinal("ProdutoValor")] != DBNull.Value ? reader.GetDouble(reader.GetOrdinal("ProdutoValor")) : 0;
                        pedido.Total = pedido.ProdutoValor * pedido.Quantidade;

                        pedidos.Add(pedido);
                    }

                    return pedidos;
                }
            }
            catch
            {
                throw;
            }
        }


        private async Task<int> AdicionarPedido(int clienteId)
        {
            try
            {

                int lastInsertedId = 0;
                using (var conn = new MySqlConnection(_connMySql))
                {
                    await conn.OpenAsync();

                    var sql = new StringBuilder();
                    sql.Append(" INSERT INTO pedidos ");
                    sql.Append(" (`ClienteId`, `StatusId`, `PedidoData`) ");
                    sql.Append(" VALUES ");
                    sql.AppendFormat(" ('{0}', '{1}', '{2}'); ", clienteId, 1, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    sql.Append(" SELECT LAST_INSERT_ID() as UltimoId;"); // Adicionando a consulta para obter o último ID inserido

                    using MySqlCommand command = new(sql.ToString(), conn);

                    using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();

                    if(reader.Read())
                    {
                        lastInsertedId = reader.GetInt32(reader.GetOrdinal("UltimoId"));// Executa a consulta e obtém o último ID inserido
                    }
                }
                return lastInsertedId;
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
                int pedidoId = await RetornaIdPedidoAberto(clienteId);

                int estoqueProduto = await RetornaQuantidadeProduto(produtoId);

                int estoqueRestante = estoqueProduto - quantidade;

                AlterarQuantidadeEstoque(produtoId, estoqueRestante);

                if(pedidoId > 0)
                {
                    PedidoItem verificaSeProdutoJaEstaNoPedido = await VerificaSeProdutoJaEstaNoPedido(pedidoId, produtoId);
                    var alteraQuantidade = verificaSeProdutoJaEstaNoPedido.Quantidade;
                    quantidade = alteraQuantidade + quantidade;
                    if (verificaSeProdutoJaEstaNoPedido.ProdutoId > 0)
                    {
                        AlterarQuantidadeItensPedido(pedidoId, produtoId, quantidade);
                    }
                    else
                    {
                        InserirPedidoItem(pedidoId,clienteId,produtoId,quantidade);
                    }
                }
                else
                {
                    pedidoId = await AdicionarPedido(clienteId);

                    InserirPedidoItem(pedidoId, clienteId, produtoId, quantidade);
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
                int qtdAtualProduto = await RetornaQuantidadeProduto(produtoId);
                int qtdProdutosRetorno = await RetornaQuantidadeProdutoPorCliente(clienteId, produtoId, pedidoId);
                int quantidadeTotalProduto = qtdAtualProduto + qtdProdutosRetorno;
                AlterarQuantidadeEstoque(produtoId, quantidadeTotalProduto);

                using (var conn = new MySqlConnection(_connMySql))
                {
                    await conn.OpenAsync();

                    var sql = new StringBuilder();
                    sql.Append(" DELETE FROM pedidos_itens ");
                    sql.AppendFormat(" WHERE PedidoId = '{0}' AND ClienteId = '{1}' AND ProdutoId = '{2}' ", pedidoId, clienteId, produtoId);

                    using MySqlCommand command = new(sql.ToString(), conn);

                    using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();

                    return true;
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
                using (var conn = new MySqlConnection(_connMySql))
                {
                    await conn.OpenAsync();

                    var sql = new StringBuilder();
                    sql.Append(" UPDATE pedidos ");
                    sql.AppendFormat(" set StatusId = {0} ", statusId);
                    sql.AppendFormat(" where PedidoId = {0} ", pedidoId);

                    using MySqlCommand command = new(sql.ToString(), conn);

                    using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();

                }
            }
            catch
            {
                throw;
            }
        }

        private async Task<int> RetornaIdPedidoAberto(int clienteId)
        {
            try
            {
                using (var conn = new MySqlConnection(_connMySql))
                {
                    await conn.OpenAsync();

                    var sql = new StringBuilder();
                    sql.Append(" SELECT * FROM pedidos ");
                    sql.Append(" where StatusId = 1 ");
                    sql.AppendFormat(" and ClienteId ={0}; ", clienteId);

                    using MySqlCommand command = new(sql.ToString(), conn);

                    using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();

                    int response = 0;

                    if (reader.Read())
                    {
                        response = reader.GetInt32(reader.GetOrdinal("PedidoId"));
                    }
                    return response;
                }
            }
            catch
            {
                throw;
            }
        }
        private async Task<int> RetornaQuantidadeProduto(int produtoId)
        {
            try
            {
                using (var conn = new MySqlConnection(_connMySql))
                {
                    await conn.OpenAsync();

                    var sql = new StringBuilder();
                    sql.Append(" SELECT Quantidade FROM produtos ");
                    sql.AppendFormat(" where ProdutoId = {0} ", produtoId);

                    using MySqlCommand command = new(sql.ToString(), conn);

                    using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();

                    int response = 0;

                    if (reader.Read())
                    {
                        response = reader.GetInt32(reader.GetOrdinal("Quantidade"));
                    }
                    return response;
                }
            }
            catch
            {
                throw;
            }
        }

        private async Task<PedidoItem> VerificaSeProdutoJaEstaNoPedido(int pedidoId, int produtoId)
        {
            try
            {
                using (var conn = new MySqlConnection(_connMySql))
                {
                    await conn.OpenAsync();

                    var sql = new StringBuilder();
                    sql.Append(" SELECT * FROM pedidos_itens ");
                    sql.AppendFormat(" where PedidoId = {0} AND ProdutoId = {1} ", pedidoId, produtoId);

                    using MySqlCommand command = new(sql.ToString(), conn);

                    using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();

                    var pedido = new PedidoItem();

                    if (reader.Read())
                    {
                        pedido.ProdutoId = reader.GetInt32(reader.GetOrdinal("ProdutoId"));
                        pedido.PedidoId = reader.GetInt32(reader.GetOrdinal("PedidoId"));
                        pedido.ClienteId = reader.GetInt32(reader.GetOrdinal("ClienteId"));
                        pedido.Quantidade = reader.GetInt32(reader.GetOrdinal("Quantidade"));
                    }
                    return pedido;
                }
            }
            catch
            {
                throw;
            }
        }

        private async Task<int> RetornaQuantidadeProdutoPorCliente(int clienteId,int produtoId, int pedidoId)
        {
            try
            {
                using (var conn = new MySqlConnection(_connMySql))
                {
                    await conn.OpenAsync();

                    var sql = new StringBuilder();
                    sql.Append(" select SUM(Quantidade) as Quantidade");
                    sql.Append(" FROM pedidos_itens ");
                    sql.AppendFormat(" where ClienteId = {0} and ProdutoId = {1} AND PedidoId = {2} ", clienteId, produtoId, pedidoId);

                    using MySqlCommand command = new(sql.ToString(), conn);

                    using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();

                    int response = 0;

                    if (reader.Read())
                    {
                        response = reader.GetInt32(reader.GetOrdinal("Quantidade"));
                    }
                    return response;
                }
            }
            catch
            {
                throw;
            }
        }

        private async void AlterarQuantidadeEstoque(int produtoId, int quantidade)
        {
            try
            {
                using (var conn = new MySqlConnection(_connMySql))
                {
                    await conn.OpenAsync();

                    var sql = new StringBuilder();
                    sql.Append(" UPDATE produtos ");
                    sql.AppendFormat(" set quantidade = {0} ", quantidade);
                    sql.AppendFormat(" where ProdutoId = {0} ", produtoId);

                    using MySqlCommand command = new(sql.ToString(), conn);

                    using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();

                }
            }
            catch
            {
                throw;
            }
        }
        private async void InserirPedidoItem(int pedidoId, int clienteId, int produtoId, int quantidade)
        {
            try
            {
                using (var conn = new MySqlConnection(_connMySql))
                {
                    await conn.OpenAsync();

                    var sql = new StringBuilder();
                    sql.Append(" INSERT INTO pedidos_itens ");
                    sql.Append(" (`PedidoId`, `ClienteId`, `ProdutoId`, `Quantidade`) ");
                    sql.Append(" VALUES ");
                    sql.AppendFormat(" ('{0}', '{1}', '{2}', '{3}'); ", pedidoId, clienteId, produtoId, quantidade);

                    using MySqlCommand command = new(sql.ToString(), conn);

                    using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                }
            }
            catch
            {
                throw;
            }
        }
        
        private async void AlterarQuantidadeItensPedido(int pedidoId, int produtoId, int quantidade)
        {
            try
            {
                using (var conn = new MySqlConnection(_connMySql))
                {
                    await conn.OpenAsync();

                    var sql = new StringBuilder();
                    sql.Append(" UPDATE pedidos_itens ");
                    sql.AppendFormat(" set Quantidade = {0} ", quantidade);
                    sql.AppendFormat(" where PedidoId = {0} ", pedidoId);
                    sql.AppendFormat(" AND ProdutoId = {0} ", produtoId);

                    using MySqlCommand command = new(sql.ToString(), conn);

                    using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();

                }
            }
            catch
            {
                throw;
            }
        }

    }
}
