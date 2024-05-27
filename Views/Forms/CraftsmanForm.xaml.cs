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
    /// Логика взаимодействия для CraftsmanForm.xaml
    /// </summary>
    public partial class CraftsmanForm : Window
    {
        public CraftsmanForm()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var craftsman = DataContext as Models.Craftsman;
            if
                (
                    string.IsNullOrEmpty(craftsman.FirstName) ||
                    string.IsNullOrEmpty(craftsman.SecondName) ||
                    string.IsNullOrEmpty(craftsman.Expirience.ToString())
                )
                throw new Exception("Не все данные были введены");
            DialogResult = true;
        }
    }
}
