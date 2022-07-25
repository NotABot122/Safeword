using System;
using System.Windows;


namespace SafeWord
{
    /// <summary>
    /// Interaction logic for AddPasswordWindow.xaml
    /// </summary>
    public partial class AddPasswordWindow : Window
    {
        public AddPasswordWindow()
        {
            InitializeComponent();
        }
        public void Add_Password_Confirm(object sender, RoutedEventArgs e)
        {
            Control crtl = new Control();
            string web = Website.Text;
            string user = Username.Text;
            string pass = Password.Text;
            if (crtl.Add_Password(web, user, pass))
            {
                Hide();
                Control.home.Show();
            }


            else
            {
                throw new ArgumentException("Parameter cannot be null", nameof(sender));
            }

        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }

}
