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
using System.Windows.Shapes;

namespace Cars.Views.Forms
{
    /// <summary>
    /// Логика взаимодействия для ServiceCenterForm.xaml
    /// </summary>
    public partial class ServiceCenterForm : Window
    {
        public ServiceCenterForm()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var serviceCenter = DataContext as Models.ServiceCenter;
            if
                (
                    string.IsNullOrEmpty(serviceCenter.FirmName) ||
                    string.IsNullOrEmpty(serviceCenter.CenterName) ||
                    string.IsNullOrEmpty(serviceCenter.City) ||
                    string.IsNullOrEmpty(serviceCenter.Street) ||
                    string.IsNullOrEmpty(serviceCenter.Building)
                )
                throw new Exception("Не все данные были введены");
            DialogResult = true;
        }
    }
}
