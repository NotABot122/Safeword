using System.Windows;


namespace SafeWord
{
    /// <summary>
    /// Interaction logic for SignupWindow.xaml
    /// </summary>
    public partial class SignupWindow : Window
    {
        public SignupWindow()
        {
            InitializeComponent();
        }
        private void Signup_Confirm(object sender, RoutedEventArgs e)
        {
            string email = Email.Text;
            string username = Username.Text;
            string password = Password.Text;
            Control control = new Control();
            control.Confirm_Signup(email, username, password, this);

        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
