using Cars.Models;
using Cars.Views;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cars
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

        private void UserCabinetButton_Click(object sender, RoutedEventArgs e)
        {
            UserCabinet userCabinet = new UserCabinet() 
            {
                DataContext = App.CurrentUser
            };
            userCabinet.Owner = this;
            userCabinet.Show();
        }
    }
}