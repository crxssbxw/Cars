using Cars.Models.CustomViews;
using Cars.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Логика взаимодействия для CarsForm.xaml
    /// </summary>
    public partial class CarsForm : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private ObservableCollection<Models.Client> clients = new();
        public ObservableCollection<Models.Client> Clients
        {
            get => clients;
            set
            {
                clients = value;
                OnPropertyChanged(nameof(Clients));
            }
        }

        private ObservableCollection<Models.Client> owners = new();

        public ObservableCollection<Models.Client> Owners 
        { 
            get => owners; 
            set
            {
                owners = value;
                OnPropertyChanged(nameof(Owners));
            }
        }
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public CarsForm()
        {
            InitializeComponent();
            OwnersList.ItemsSource = Owners;
            OwnerBox.ItemsSource = Clients;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var car = DataContext as CustomViewCar;
            if
            (
                string.IsNullOrEmpty(car.Serial) ||
                string.IsNullOrEmpty(car.Manufacturer) ||
                string.IsNullOrEmpty(car.BodyType) ||
                string.IsNullOrEmpty(car.CarNum) ||
                string.IsNullOrEmpty(car.Color)
            )
                throw new Exception("Не все данные были введены");
            DialogResult = true;
        }

        private void AddOwnerButton_Click(object sender, RoutedEventArgs e)
        {
            Models.Client owner = new();
            owner = OwnerBox.SelectedValue as Models.Client;
            Owners.Add(owner);
            Clients.Remove(owner);
            OwnersList.ItemsSource = Owners;
        }
    }
}
