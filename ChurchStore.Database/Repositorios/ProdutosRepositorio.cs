using ChurchStore.Domain;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchStore.Database.Repositorios
{
    public class ProdutosRepositorio
    {
        private readonly string _connMySql;

        public ProdutosRepositorio(string connMySql)
        {
            _connMySql = connMySql;
        }

        public async Task<Produto> Retornar(int id)
        {
            try
            {
                using (var conn = new MySqlConnection(_connMySql))
                {
                    await conn.OpenAsync();

                    var sql = new StringBuilder();
                    sql.Append(" SELECT * ");
                    sql.Append(" FROM produtos ");
                    sql.AppendFormat(" where ProdutoId = {0} ", id);

                    using MySqlCommand command = new(sql.ToString(), conn);

                    using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();

                    var produto = new Produto();

                    if (reader.Read())
                    {
                        produto.ProdutoId = reader.GetInt32(reader.GetOrdinal("ProdutoId"));
                        produto.ProdutoNome = reader[reader.GetOrdinal("ProdutoNome")].ToString();
                        produto.ProdutoValor = reader[reader.GetOrdinal("ProdutoValor")] != DBNull.Value ? reader.GetDouble(reader.GetOrdinal("ProdutoValor")) : 0;
                        produto.Quantidade = reader.GetInt32(reader.GetOrdinal("Quantidade"));
                        produto.ImagemUrl = reader[reader.GetOrdinal("ImagemUrl")].ToString();
                        produto.Exibir = reader[reader.GetOrdinal("Exibir")] != DBNull.Value ? reader.GetBoolean(reader.GetOrdinal("Exibir")) : false;

                    }

                    return produto;
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Produto>> Listar(bool publico = false)
        {
            try
            {
                using (var conn = new MySqlConnection(_connMySql))
                {
                    await conn.OpenAsync();

                    var sql = new StringBuilder();
                    sql.Append(" SELECT * ");
                    sql.Append(" FROM produtos ");
                    if (publico)
                    {
                        sql.Append(" where Exibir = 1 ");
                    }

                    using MySqlCommand command = new(sql.ToString(), conn);

                    using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();

                    var produtos = new List<Produto>();

                    while (reader.Read())
                    {
                        var produto = new Produto();

                        produto.ProdutoId = reader.GetInt32(reader.GetOrdinal("ProdutoId"));
                        produto.ProdutoNome = reader[reader.GetOrdinal("ProdutoNome")].ToString();
                        produto.ProdutoValor = reader[reader.GetOrdinal("ProdutoValor")] != DBNull.Value ? reader.GetDouble(reader.GetOrdinal("ProdutoValor")) : 0;
                        produto.Quantidade = reader.GetInt32(reader.GetOrdinal("Quantidade"));
                        produto.ImagemUrl = reader[reader.GetOrdinal("ImagemUrl")].ToString();
                        produto.Exibir = reader[reader.GetOrdinal("Exibir")] != DBNull.Value ? reader.GetBoolean(reader.GetOrdinal("Exibir")) : false;


                        produtos.Add(produto);
                    }

                    return produtos;
                }
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

                ulong lastInsertedId = 0;
                using (var conn = new MySqlConnection(_connMySql))
                {
                    await conn.OpenAsync();

                    var sql = new StringBuilder();
                    sql.Append(" INSERT INTO produtos ");
                    sql.Append(" (`ProdutoNome`, `ProdutoValor`, `Quantidade`, `ImagemUrl`) ");
                    sql.Append(" VALUES ");
                    sql.AppendFormat(" ('{0}', '{1}', '{2}', '{3}'); ", produto.ProdutoNome, produto.ProdutoValor, produto.Quantidade, produto.ImagemUrl);

                    sql.Append(" SELECT LAST_INSERT_ID();"); // Adicionando a consulta para obter o último ID inserido

                    using MySqlCommand command = new(sql.ToString(), conn);

                    lastInsertedId = (ulong)await command.ExecuteScalarAsync(); // Executa a consulta e obtém o último ID inserido

                    produto.ProdutoId = (int)lastInsertedId;
                }
                return produto;
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

                using (var conn = new MySqlConnection(_connMySql))
                {
                    await conn.OpenAsync();

                    var sql = new StringBuilder();
                    sql.Append(" UPDATE produtos SET ");
                    sql.AppendFormat(" ProdutoNome = '{0}', ProdutoValor = '{1}', Quantidade = '{2}', ImagemUrl = '{3}', Exibir = {4}  ",
                        produto.ProdutoNome, produto.ProdutoValor, produto.Quantidade, produto.ImagemUrl, produto.Exibir
                    );
                    sql.AppendFormat(" WHERE(ProdutoId = '{0}');", produto.ProdutoId);

                    using MySqlCommand command = new(sql.ToString(), conn);

                    using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();

                  
                    return produto;
                   
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
