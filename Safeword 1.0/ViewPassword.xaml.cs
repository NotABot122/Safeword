using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
namespace SafeWord
{
    /// <summary>
    /// Interaction logic for ViewPassword.xaml
    /// </summary>
    public partial class ViewPassword : Window
    {
        public ViewPassword()
        {
            InitializeComponent();
            Results.DataContext = new List<string>();
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<string> elements = Control.Find_Password(Search.Text);
            Results.Items.Clear();
            for (int i = 0; i < elements.Count; i++)
            {
                ListBoxItem item = new ListBoxItem();
                item.Content = elements[i];
                item.Selected += new RoutedEventHandler(On_Selected);
                int index = Results.Items.Add(item);
            }

        }

        private void On_Selected(object sender, RoutedEventArgs e)
        {
            ListBoxItem item = (ListBoxItem)sender;
            string url = (string)item.Content;
            MessageBox.Show(Control.Find_Password_By_URL(url));
            Results.UnselectAll();

        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            Control.home.Show();

        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }



}
