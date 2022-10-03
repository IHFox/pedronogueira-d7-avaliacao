using pedronogueira_d7_avaliacao.Interfaces;
using pedronogueira_d7_avaliacao.Models;
using System;
using System.Data.SqlClient;

namespace pedronogueira_d7_avaliacao.Repositories
{
    /// <summary>
    /// Repositório responsável pela manipulação da entidade usuário
    /// </summary>
    internal class UserRepository : IUser
    {
        /// <summary>
        /// String de conexão com o banco de dados que recebe os parâmetros
        /// Data Source = Nome do servidor
        /// initial catalog = Nome do banco de dados
        /// integrated security=true = Faz a autenticação com o usuário do sistema
        /// </summary>
        private readonly string stringConexao = "Data source=localhost\\SQLEXPRESS; initial catalog=pedronogueira-d7-avaliacao; integrated security=true;";
        SecurityRepository _security = new();

        /// <summary>
        /// Realiza conexão com banco de dados e verifica a correspondência de usuário e senhas passados
        /// </summary>
        /// <param name="login">Usuário (e-mail)</param>
        /// <param name="password">Senha em formato "plain text"</param>
        /// <returns>Dados públicos de usuário (caso haja correspondência) ou null caso contrário</returns>
        public publicUser UserConnect(string login, string password)
        {
            // Declara a instrução a ser executada
            string querySelect = "SELECT user_name, user_password, user_id FROM Users WHERE user_email= @email";

            // Declara a SqlConnection con passando a string de conexão como parâmetro
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara o SqlCommand cmd passando a query que será executada e a conexão como parâmetros
                SqlCommand cmd = new SqlCommand(querySelect, con);
                cmd.Parameters.AddWithValue("@email", login);

                // Abre a conexão com o banco de dados
                con.Open();

                // Declara o SqlDataReader rdr para percorrer a tabela do banco de dados
                SqlDataReader rdr;

                using (cmd)
                {
                    // Executa a query e armazena os dados no rdr
                    rdr = cmd.ExecuteReader();

                    // Retorna o usuário público
                    while (rdr.Read())
                    {
                        if (_security.PasswordCompare(rdr["user_password"].ToString(), password))
                        {
                            publicUser user = new()
                            {
                                // Atribui à propriedade IdUser o valor da coluna "user_id" da tabela do banco de dados
                                IdUser = Guid.Parse((string)rdr["user_id"]),

                                // Atribui à propriedade nome o valor da coluna "user_name" da tabela do banco de dados
                                Name = rdr["user_name"].ToString()
                            };

                            return user;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    return null;
                }
            }
        }
    }
}
