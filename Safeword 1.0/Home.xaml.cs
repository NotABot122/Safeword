using System.Windows;

namespace SafeWord
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();
        }
        public void Add_Password_Click(object sender, RoutedEventArgs e)
        {
            Control.addpassword.Show();
            Hide();
        }
        public void View_Password_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            Control.viewpassword.Show();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }


}
