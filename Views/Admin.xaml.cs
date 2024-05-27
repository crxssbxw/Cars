using Cars.ExportTools;
using Cars.Models;
using Cars.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : UserControl
    {
        private List<ClientCar> clientCars = new();
        public static ServiceCenterViewModel ServiceCenterViewModel { get; set; } = new();
        public static ServiceViewModel ServiceViewModel { get; set; } = new(ServiceCenterViewModel);
        public static CraftsmanViewModel CraftsmanViewModel { get; set; } = new(ServiceCenterViewModel);
        public static ClientViewModel ClientViewModel { get; set; } = new();
        public static CarViewModel CarViewModel { get; set; } = new(ClientViewModel);
        public static UserViewModel UserViewModel { get; set; } = new();
        public static ContractViewModel ContractViewModel { get; set; } = new();
        public Admin()
        {
            InitializeComponent();
            ServiceCenterTable.DataContext = ServiceCenterViewModel;
            ServiceTable.DataContext = ServiceViewModel;
            CraftsmansTable.DataContext = CraftsmanViewModel;
            ClientTable.DataContext = ClientViewModel;
            CarTable.DataContext = CarViewModel;
            UserTable.DataContext = UserViewModel;
            ButtonsPanel.DataContext = UserViewModel;
            ContractsTab.DataContext = ContractViewModel;
            ContractView.Clients = ClientViewModel.Clients;
            ContractView.ServiceCenters = ServiceCenterViewModel.ServiceCenters;
        }

        private void ServiceCentersSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchString = ServiceCentersSearch.Text.ToLower();
            if (searchString == string.Empty) ServiceCenterViewModel.CollectionView.Filter = (a) => true;
            else
            {
                ServiceCenterViewModel.CollectionView.Filter = (a) =>
                {
                    var b = (ServiceCenter)a;
                    return
                    b.FirmName.ToLower().Contains(searchString) ||
                    b.CenterName.ToLower().Contains(searchString) ||
                    b.City.ToLower().Contains(searchString) ||
                    b.Street.ToLower().Contains(searchString);
                };
            }
        }

        private void ServiceCenterTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ServiceCenter selected = ServiceCenterViewModel.SelectedServiceCenter;
            if (selected == null) 
            { 
                ServiceViewModel.CollectionView.Filter = (a) => true;
                CraftsmanViewModel.CollectionView.Filter = (a) => true;
            }
            else 
            {
                ServiceViewModel.CollectionView.Filter = (a) =>
                {
                    var b = (Service)a;
                    return b.CenterId == selected.CenterId;
                };
                CraftsmanViewModel.CollectionView.Filter = (a) =>
                {
                    var b = (Models.Craftsman)a;
                    return b.CenterId == selected.CenterId;
                };
            }
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

        private void CraftsmenSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchString = CraftsmenSearch.Text.ToLower();
            if (searchString == string.Empty) CraftsmanViewModel.CollectionView.Filter = (a) => true;
            else
            {
                CraftsmanViewModel.CollectionView.Filter = (a) =>
                {
                    var b = (Models.Craftsman)a;
                    return
                    b.FirstName.ToLower().Contains(searchString) ||
                    b.SecondName.ToLower().Contains(searchString) ||
                    b.MiddleName.ToLower().Contains(searchString) ||
                    b.Expirience.ToString().Contains(searchString);
                };
            }
        }

        private void ClientTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Models.Client selected = ClientViewModel.SelectedClient;
            if (selected == null) 
            {
                CarViewModel.CollectionView.Filter = (a) => true;
            }
            else
            {
                foreach (var cc in App.Context.ClientCars)
                {
                    clientCars.Add(cc);
                }
                clientCars = clientCars.Where(a => a.ClientId == selected.ClientId).ToList();
                CarViewModel.CollectionView.Filter = (a) =>
                {
                    var b = (Models.Car)a;
                    return clientCars.Any(a => b.CarId == a.CarId);
                };
            }
        }

        private void ClientsNameSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchString = ClientsNameSearch.Text.ToLower();
            if (searchString == string.Empty) ClientViewModel.CollectionView.Filter = a => true;
            else
            {
                ClientViewModel.CollectionView.Filter = a =>
                {
                    var b = (Models.Client)a;
                    return b.FirstName.ToLower().Contains(searchString) ||
                    b.SecondName.ToLower().Contains(searchString) ||
                    (b.FirstName.ToLower().Contains(searchString) && b.SecondName.ToLower().Contains(searchString));
                };
            }
        }

        private void ClientsPhoneSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchString = ClientsPhoneSearch.Text.ToLower();
            if (searchString == string.Empty) ClientViewModel.CollectionView.Filter = a => true;
            else
            {
                ClientViewModel.CollectionView.Filter = a =>
                {
                    var b = (Models.Client)a;
                    return b.TelNumber.ToLower().Contains(searchString);
                };
            }
        }

        private void CarsSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchString = CarsSearch.Text.ToLower();
            if (searchString == string.Empty) CarViewModel.CollectionView.Filter = a => true;
            else
            {
                CarViewModel.CollectionView.Filter = a =>
                {
                    var b = (Models.Car)a;
                    return b.CarNum.ToLower().Contains(searchString);
                };
            }
        }

        private void ContractsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ContractView.SelectedContract = ContractViewModel.SelectedContract;
            ContractView.ClientBox.ItemsSource = ClientViewModel.Clients;
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            var reportExcel = new ExcelExport().Generate(ServiceCenterViewModel);

            File.WriteAllBytes("Report.xlsx", reportExcel);
            FileInfo fileInfo = new("Report.xlsx");

            string filePath = fileInfo.FullName;

            MessageBox.Show($"Отчет успешно записан в указанный путь:\n{filePath}", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
