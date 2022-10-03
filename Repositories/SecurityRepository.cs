using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace pedronogueira_d7_avaliacao.Repositories
{
    internal class SecurityRepository
    {
        /// <summary>
        /// Verifica se o e-mail digitado é um e-mail válido (possui @ e .)
        /// </summary>
        /// <param name="email">E-mail a ser verificado</param>
        /// <returns>True caso o parâmetro email seja um e-mail válido</returns>
        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normaliza o domínio
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examina a parte de domínio do e-mail e normaliza ela
                string DomainMapper(Match match)
                {
                    // Usa classe IdnMapping para converter nomes de domínio em Unicode
                    var idn = new IdnMapping();

                    // Pega e processa o nome de domínio (dispara ArgumentException quando inválido)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        /// <summary>
        /// Geração da Hash com o BCrypt
        /// </summary>
        /// <param name="password">senha em formato "plain text"</param>
        /// <returns>Senha em formato Hash</returns>
        public string HashGeneration(string password)
        {
            int workfactor = 10; // 2^10 = 1024 iterações

            string salt = BCrypt.Net.BCrypt.GenerateSalt(workfactor);
            string hash = BCrypt.Net.BCrypt.HashPassword(password, salt);

            return hash;
        }

        /// <summary>
        /// Comparação de hash com senha em "plain text"
        /// </summary>
        /// <param name="hash">senha em formato hash</param> 
        /// <param name="password">senha em formato "plain text"</param> 
        /// <returns>True caso a senha em formato "plain text" seja verificada com a senha em formato hash</returns>
        public bool PasswordCompare(string hash, string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}