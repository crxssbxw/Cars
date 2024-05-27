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
    /// Логика взаимодействия для ClientForm.xaml
    /// </summary>
    public partial class ClientForm : Window
    {
        public ClientForm()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var client = DataContext as Models.Client;
            if
                (
                    string.IsNullOrEmpty(client.FirstName) ||
                    string.IsNullOrEmpty(client.SecondName) ||
                    string.IsNullOrEmpty(client.TelNumber)
                )
                throw new Exception("Не все данные были введены");
            DialogResult = true;
        }
    }
}
