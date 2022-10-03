using System;

namespace pedronogueira_d7_avaliacao.Models
{
    /// <summary>
    /// Modelo para um usuário a ser escrito no banco de dados
    /// </summary>
    internal class User
    {
        public Guid IdUser { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }

    /// <summary>
    /// Modelo para dados públicos do usuário
    /// </summary>
    internal class publicUser
    {
        public Guid IdUser { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
