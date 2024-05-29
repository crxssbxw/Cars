using Cars.Models.CustomViews;
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
    /// Логика взаимодействия для ServiceForm.xaml
    /// </summary>
    public partial class ServiceForm : Window
    {
        public ServiceForm()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

            var service = DataContext as CustomViewService;

            if (string.IsNullOrEmpty(service.ServiceType)) throw new Exception("Название услуги не может быть пустым");
            if (string.IsNullOrEmpty(service.Cost.ToString())) throw new Exception("Цена не может быть пустой");
            else if (CostField.Text.Any(c => char.IsLetter(c))) throw new Exception("Цена должна быть числом");
            if (service.ServiceId == null) throw new Exception("Услуга должна относиться к сервисному центру");

            DialogResult = true;
        }
    }
}
