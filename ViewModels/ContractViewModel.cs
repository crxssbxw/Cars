using Cars.Models;
using Cars.Models.CustomViews;
using Cars.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.ViewModels
{
    public class ContractViewModel : NotificationViewModel
    {
        private ObservableCollection<Contract> contracts = new();
        public ObservableCollection<Contract> Contracts
        {
            get => contracts;
            set
            {
                contracts = value;
                OnPropertyChanged(nameof(Contract));
            }
        }
        
        private ObservableCollection<ServiceCenter> serviceCenters = new();
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



        public Models.Client ContractClient
        {
            get => SelectedContract.Client;
        }

        public List<ClientCar> ClientCars
        {
            get => ContractClient.ClientCars.ToList();
        }

        public ObservableCollection<Car> Cars 
        {
            get
            {
                ObservableCollection<Car> cars = new();
                foreach(var item in ClientCars)
                {
                    cars.Add(item.Car);
                }
                return cars;
            }
        }

        private ServiceCenter selectedServiceCenter;
        public ServiceCenter SelectedServiceCenter
        {
            get => selectedServiceCenter;
            set
            {
                selectedServiceCenter = value;
                OnPropertyChanged(nameof(SelectedServiceCenter));
            }
        }

        public ContractViewModel()
        {
            context.ContractCars.Load();
            context.ContractServices.Load();
            context.Services.Load();
            context.Cars.Load();
            context.ServiceCenters.Load();
            context.ClientCars.Load();
            context.Clients.Load();
            foreach(var contract in context.Contracts)
            {
                Contracts.Add(contract);
            }
            foreach(var serviceCenter in context.ServiceCenters)
            {
                ServiceCenters.Add(serviceCenter);
            }
            ViewSource.Source = Contracts;
        }
    }
}
