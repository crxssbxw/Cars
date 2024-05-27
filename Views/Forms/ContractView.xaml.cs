using Cars.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cars.Views.Forms
{
    /// <summary>
    /// Логика взаимодействия для ContractView.xaml
    /// </summary>
    public partial class ContractView : UserControl, INotifyPropertyChanged
    {
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

        private ObservableCollection<ServiceCenter> serviceCenters;
        public ObservableCollection<ServiceCenter> ServiceCenters 
        {
            get => serviceCenters;
            set
            {
                serviceCenters = value;
                OnPropertyChanged(nameof(ServiceCenters));
            }
        }

        private Contract selectedContract;
        public Contract SelectedContract
        {
            get => selectedContract;
            set
            {
                selectedContract = value;
                OnPropertyChanged(nameof(SelectedContract));
            }
        }

        public ContractView()
        {
            InitializeComponent();
            ClientBox.ItemsSource = Admin.ClientViewModel.Clients;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
