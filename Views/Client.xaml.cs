using Cars.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cars.Views
{
    /// <summary>
    /// Логика взаимодействия для Client.xaml
    /// </summary>
    public partial class Client : UserControl
    {
        public static ServiceCenterViewModel ServiceCenterViewModel { get; set; } = Admin.ServiceCenterViewModel;
        public static ServiceViewModel ServiceViewModel { get; set; } = Admin.ServiceViewModel;
        public Client()
        {
            InitializeComponent();
            ServiceCenterTable.DataContext = ServiceCenterViewModel;
            ServiceTable.DataContext = ServiceViewModel;
        }
    }
}
