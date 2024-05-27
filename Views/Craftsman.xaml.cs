using Cars.Models;
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
    /// Логика взаимодействия для Craftsman.xaml
    /// </summary>
    public partial class Craftsman : UserControl
    {
        public static ServiceViewModel ServiceViewModel { get; set; } = Admin.ServiceViewModel;
        public Craftsman()
        {
            InitializeComponent();
            ServiceTable.DataContext = ServiceViewModel;
            AddButton.DataContext = ServiceViewModel;
        }

        private void ServicesSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchString = ServicesSearch.Text.ToLower();
            if (searchString == string.Empty) ServiceViewModel.CollectionView.Filter = (a) => true;
            else
            {
                ServiceViewModel.CollectionView.Filter = (a) =>
                {
                    var b = (Service)a;
                    return
                    b.ServiceType.ToLower().Contains(searchString);
                };
            }
        }
    }
}
