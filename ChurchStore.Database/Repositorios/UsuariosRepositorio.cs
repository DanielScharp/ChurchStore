using ChurchStore.Domain;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchStore.Database.Repositorios
{
    public class UsuariosRepositorio
    {
        private readonly string _connMySql;

        public UsuariosRepositorio(string connMySql)
        {
            _connMySql = connMySql;
        }

        public async Task<Usuario> RetornarPorApelidoSenha(string apelido, string senha = "")
        {
            try
            {
                using (var conn = new MySqlConnection(_connMySql))
                {
                    await conn.OpenAsync();

                    var sql = new StringBuilder();
                    sql.Append(" SELECT * FROM church_store.usuarios ");
                    sql.AppendFormat(" where login = '{0}' ", apelido);
                    if (!String.IsNullOrEmpty(senha))
                    {
                        sql.AppendFormat(" and Senha = MD5('{0}') ", senha);
                    }

                    using MySqlCommand command = new(sql.ToString(), conn);

                    using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();

                    var usuario = new Usuario();

                    if (reader.Read())
                    {

                        usuario.UsuarioId = reader.GetInt32(reader.GetOrdinal("usuarioId"));
                        usuario.Nome = reader[reader.GetOrdinal("nome")].ToString();
                        usuario.Apelido = reader[reader.GetOrdinal("login")].ToString();
                        usuario.Role = reader[reader.GetOrdinal("role")].ToString();
                    }

                    return usuario;
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<Usuario> RetornarPorApelidoEmail(string apelido, string email)
        {
            try
            {
                using (var conn = new MySqlConnection(_connMySql))
                {
                    await conn.OpenAsync();

                    var sql = new StringBuilder();
                    sql.Append(" SELECT * FROM church_store.usuarios ");
                    sql.AppendFormat(" where login = '{0}' ", apelido);
                    sql.AppendFormat(" and email = '{0}' ", email);

                    using MySqlCommand command = new(sql.ToString(), conn);

                    using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();

                    var usuario = new Usuario();

                    if (reader.Read())
                    {

                        usuario.UsuarioId = reader.GetInt32(reader.GetOrdinal("usuarioId"));
                        usuario.Nome = reader[reader.GetOrdinal("nome")].ToString();
                        usuario.Email = reader[reader.GetOrdinal("email")].ToString();
                        usuario.Apelido = reader[reader.GetOrdinal("login")].ToString();
                    }

                    return usuario;
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Usuario>> ListarUsuarios()
        {
            try
            {
                using (var conn = new MySqlConnection(_connMySql))
                {
                    await conn.OpenAsync();

                    var sql = new StringBuilder();
                    sql.Append(" SELECT * FROM usuarios; ");

                    using MySqlCommand command = new(sql.ToString(), conn);

                    using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();

                    var listaUsuarios = new List<Usuario>();


                    while (reader.Read())
                    {
                        var usuario = new Usuario();

                        usuario.UsuarioId = reader.GetInt32(reader.GetOrdinal("usuarioId"));
                        usuario.Nome = reader[reader.GetOrdinal("nome")].ToString();

                        listaUsuarios.Add(usuario);
                    }

                    return listaUsuarios;
                }
            }
            catch
            {
                throw;
            }
        }

        public async void Cadastrar(Login user)
        {
            try
            {
                using (var conn = new MySqlConnection(_connMySql))
                {
                    await conn.OpenAsync();

                    var sql = new StringBuilder();
                    sql.Append(" insert into usuarios ");
                    sql.Append(" (Nome, Telefone, Senha) ");
                    sql.Append(" values ");

                    using MySqlCommand command = new(sql.ToString(), conn);

                    using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                }
            }
            catch
            {
                throw;
            }
        }

        public async void Alterar(Usuario user)
        {
            try
            {
                using (var conn = new MySqlConnection(_connMySql))
                {
                    await conn.OpenAsync();

                    var sql = new StringBuilder();
                    sql.Append(" UPDATE usuarios set ");
                    sql.AppendFormat(" Nome='{0}' ", user.Nome);
                    sql.AppendFormat(" where UsuarioId = '{0}' ", user.UsuarioId);

                    using MySqlCommand command = new(sql.ToString(), conn);

                    using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> AlterarSenha(Usuario user)
        {
            try
            {
                using (var conn = new MySqlConnection(_connMySql))
                {
                    await conn.OpenAsync();

                    var sql = new StringBuilder();
                    sql.Append(" UPDATE usuarios SET ");
                    sql.AppendFormat(" Senha= md5('{0}') ", user.Senha);
                    sql.AppendFormat(" where UsuarioId = '{0}' ", user.UsuarioId);

                    using MySqlCommand command = new(sql.ToString(), conn);

                    var result = await command.ExecuteNonQueryAsync();

                    return result > 0;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}

