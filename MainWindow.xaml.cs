using System.Windows;
using pedronogueira_d7_avaliacao.Models;
using pedronogueira_d7_avaliacao.Repositories;

namespace pedronogueira_d7_avaliacao
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ValidateAccess_Click(object sender, RoutedEventArgs e)
        {


            string login;
            string password;
            publicUser user = new();

            UserRepository _user = new();
            SecurityRepository _security = new();

            login = txtUser.Text;
            password = txtPass.Password.ToString(); // Estudar usar propriedade SecurePassword ao invés de Password



            if (true)// (_security.IsValidEmail(login)) // Checar se é um endereço de e-mail
            {

                user = _user.UserConnect(login, password); // Acessar banco de dados
                if (user != null) // Checar acesso
                {
                    txtResult.Content = "Usuário Autenticado!";
                }
                else
                {
                    txtResult.Content = "Credenciais Inválidas!";
                }
            }

        }
    }
}
