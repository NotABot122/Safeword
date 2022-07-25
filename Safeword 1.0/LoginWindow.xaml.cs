using System.Windows;

namespace SafeWord
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }
        private void Login_Confirm(object sender, RoutedEventArgs e)
        {
            Control ctrl = new Control();
            string user = Username.Text;
            string pass = Password.Text;
            if (ctrl.Confirm_Login(user, pass))
            {
                Control.home.Show();
                Hide();
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
