using Cars.Commands;
using Cars.Models;
using Cars.Views.Forms;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Cars.ViewModels
{
    public class ServiceCenterViewModel : NotificationViewModel
    {
        private ObservableCollection<ServiceCenter> serviceCenters = new();
        public ObservableCollection<ServiceCenter> tempServiceCenters;
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
        public ServiceCenterViewModel()
        {
            context.Craftsmen.Load();
            context.Services.Load();
            foreach(var service in context.ServiceCenters)
            {
                serviceCenters.Add(service);
            }
            tempServiceCenters = serviceCenters;
            ViewSource.Source = serviceCenters;
        }

        public ObservableCollection<ServiceCenter> ServiceCenters 
        { 
            get => serviceCenters; 
            set => serviceCenters = value; 
        }

        #region Commands
        private RelayCommand add;
        public RelayCommand Add
        {
            get
            {
                return add ?? (add = new RelayCommand(obj =>
                {
                    ServiceCenter serviceCenter = new();
                    ServiceCenterForm serviceCenterForm = new()
                    {
                        Title = "Новый сервисный центр",
                        DataContext = serviceCenter
                    };
                    serviceCenterForm.AddButton.Content = "Применить";
                    try
                    {
                        if (serviceCenterForm.ShowDialog() == true)
                        {
                            ServiceCenters.Add(serviceCenter);
                            context.ServiceCenters.Add(serviceCenter);
                            context.SaveChanges();
                            tempServiceCenters = ServiceCenters;
                        } 
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show(e.Message);
                        serviceCenterForm.Close();
                    }
                }, (obj => true)));
            }
        }

        private RelayCommand edit;
        public RelayCommand Edit
        {
            get
            {
                return edit ?? (edit = new RelayCommand(obj =>
                {
                    ServiceCenter serviceCenter = SelectedServiceCenter;
                    ServiceCenterForm serviceCenterForm = new()
                    {
                        Title = "Изменить сервисный центр",
                        DataContext = serviceCenter
                    };
                    try
                    {
                        if (serviceCenterForm.ShowDialog() == true)
                        {
                            var sc = ServiceCenters.Where(a => a.CenterId == serviceCenter.CenterId).FirstOrDefault();
                            sc = serviceCenter;
                            var editableSC = context.ServiceCenters.Where(a => a.CenterId == serviceCenter.CenterId).FirstOrDefault();
                            context.ServiceCenters.Entry(editableSC).CurrentValues.SetValues(serviceCenter);
                            context.SaveChanges();
                            tempServiceCenters = ServiceCenters;
                        }
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show(e.Message);
                        serviceCenterForm.Close();
                    }
                }, (obj => SelectedServiceCenter != null)));
            }
        }

        private RelayCommand delete;
        public RelayCommand Delete
        {
            get
            {
                return delete ?? (delete = new(obj =>
                {
                    MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить данный сервисный центр:\n" +
                        $"{SelectedServiceCenter.FirmName} {SelectedServiceCenter.CenterName}?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        ServiceCenter removable = SelectedServiceCenter;
                        ServiceCenters.Remove(SelectedServiceCenter);
                        context.ServiceCenters.Remove(removable);
                        context.SaveChanges();
                        tempServiceCenters = ServiceCenters;
                    }
                }, (obj => SelectedServiceCenter != null)));
            }
        }
        #endregion
    }
}
