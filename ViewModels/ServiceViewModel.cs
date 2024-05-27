using Cars.Commands;
using Cars.Models;
using Cars.Models.CustomViews;
using Cars.Views.Forms;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cars.ViewModels
{
    public class ServiceViewModel : NotificationViewModel
    {
        private List<ServiceCenter> serviceCenters;
        private ObservableCollection<Service> services = new();
        private Service selectedService;
        public Service SelectedService 
        {
            get => selectedService; 
            set 
            { 
                selectedService = value; 
                OnPropertyChanged(nameof(SelectedService));
            }
        }

        public ServiceViewModel()
        {
            context.Contracts.Load();
            context.ContractServices.Load();
            context.ServiceCenters.Load();
            context.Craftsmen.Load();
            context.Users.Load();
            foreach(var service in context.Services)
            {
                services.Add(service);
            }
            ViewSource.Source = services;
        }

        public ServiceViewModel(ServiceCenterViewModel scvm) : this()
        {
            serviceCenters = scvm.ServiceCenters.ToList();
        }

        public ObservableCollection<Service> Services 
        { 
            get => services;
            set
            {
                services = value;
                OnPropertyChanged(nameof(Services));
            }
        }

        #region Commands

        private RelayCommand add;
        public RelayCommand Add
        {
            get
            {
                return add ?? (add = new(obj =>
                {
                    CustomViewService customService = new();
                    ServiceForm serviceForm = new()
                    {
                        Title = "Новая услуга",
                        DataContext = customService
                    };
                    serviceForm.ServiceCenterBox.ItemsSource = context.ServiceCenters.ToList();
                    serviceForm.ServiceCenterBox.SelectedValue = serviceCenters[0];

                    if(App.CurrentUser.Role == "Craftsman" && App.CurrentUser.Craftsman != null)
                    {
                        serviceForm.ServiceCenterBox.IsEnabled = false;
                        serviceForm.ServiceCenterBox.SelectedValue = App.CurrentUser.Craftsman.Center;
                    }

                    try
                    {
                        if (serviceForm.ShowDialog() == true)
                        {
                            ServiceCenter serviceCenter = (ServiceCenter)serviceForm.ServiceCenterBox.SelectedValue;
                            customService.ServiceCenter = serviceCenter;

                            Service service = new();
                            service.CopyFromCustomView(customService);
                            Services.Add(service);
                            context.Services.Add(service);
                            context.SaveChanges();
                            SelectedService = service;
                            CollectionView.Refresh();
                        } 
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show(e.Message);
                        serviceForm.Close();
                    }
                }, 
                obj => true));
            }
        }

        private RelayCommand edit;
        public RelayCommand Edit
        {
            get
            {
                return edit ?? (edit = new(obj =>
                {
                    CustomViewService customService = new();
                    customService.ShallowCopy(SelectedService, serviceCenters);
                    ServiceForm serviceForm = new()
                    {
                        Title = "Изменить услугу",
                        DataContext = customService
                    };
                    serviceForm.AddButton.Content = "Применить";
                    serviceForm.ServiceCenterBox.ItemsSource = context.ServiceCenters.ToList();
                    serviceForm.ServiceCenterBox.SelectedValue = customService.ServiceCenter;
                    serviceForm.ServiceCenterBox.IsEnabled = false;

                    try 
                    {
                        if (serviceForm.ShowDialog() == true)
                        {
                            var service = Services.Where(obj => obj.ServiceId == customService.ServiceId).FirstOrDefault();
                            service.CopyFromCustomView(customService);
                            var editableService = context.Services.Where(a => a.ServiceId == service.ServiceId).FirstOrDefault();
                            context.Services.Entry(editableService).CurrentValues.SetValues(service);
                            context.SaveChanges();
                            SelectedService = service;
                            CollectionView.Refresh();
                        } 
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show(e.Message);
                        serviceForm.Close();
                    }
                },
                obj => SelectedService != null));
            }
        }

        private RelayCommand delete;
        public RelayCommand Delete
        {
            get
            {
                return delete ?? (delete = new(obj =>
                {
                    MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить данную услугу:\n" +
                        $"{SelectedService.ServiceType}?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if(result == MessageBoxResult.Yes)
                    {
                        Service Removable = SelectedService;
                        Services.Remove(SelectedService);
                        context.Services.Remove(Removable);
                        context.SaveChanges();
                        CollectionView.Refresh();
                    }
                },
                obj => SelectedService != null));
            }
        }

        #endregion
    }
}
