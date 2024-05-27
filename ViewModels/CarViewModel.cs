using Cars.Commands;
using Cars.Models;
using Cars.Models.CustomViews;
using Cars.Views;
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
    public class CarViewModel : NotificationViewModel
    {
        private List<Models.Client> clients = new();
        public static List<string> BodyTypeList { get; set; } = new()
        {
            "Кроссовер", "Внедорожник", "Пикап", "Хетчбэк", "Седан", "Минивен", "Родстер", "Кабриолет", "Купе", "Лимузин"
        };
        private ObservableCollection<Car> cars = new();
        public ObservableCollection<Car> Cars 
        { 
            get => cars; 
            set 
            {
                cars = value;
                OnPropertyChanged(nameof(Cars));
            }
        }

        private Car selectedCar;

        public CarViewModel()
        {
            context.ClientCars.Load();
            context.ContractCars.Load();
            context.Clients.Load();
            context.Contracts.Load();
            foreach(var car in context.Cars)
            {
                Cars.Add(car);
            }
            ViewSource.Source = Cars;
        }

        public CarViewModel(List<Models.Client> clients) : this()
        {
            this.clients = clients;
        }

        public CarViewModel(ClientViewModel clientViewModel) : this()
        {
            clients = clientViewModel.Clients.ToList();
        }

        public Car SelectedCar 
        { 
            get => selectedCar;
            set
            {
                selectedCar = value;
                OnPropertyChanged(nameof(SelectedCar));
            }
        }

        #region Commands

        private RelayCommand about;
        public RelayCommand About
        {
            get
            {
                return about ?? (about = new(obj =>
                {
                    CustomViewCar car = new();
                    car.ShallowCopy(SelectedCar);
                    string clients = "";
                    foreach (var client in car.Owners) 
                    {
                        clients += client.FirstName + "\n";
                    }

                    MessageBox.Show(clients);
                },
                obj => SelectedCar != null));
            }
        }

        private RelayCommand add;
        public RelayCommand Add
        {
            get
            {
                return add ?? (add = new(obj =>
                {
                    CustomViewCar customCar = new();
                    customCar.Clients = clients;
                    CarsForm carsForm = new()
                    {
                        Title = "Новый автомобиль",
                        DataContext = customCar
                    };
                    carsForm.BodyTypeBox.ItemsSource = BodyTypeList;
                    foreach(var c in context.Clients)
                    {
                        carsForm.Clients.Add(c);
                    }
                    try 
                    {
                        if (carsForm.ShowDialog() == true)
                        {
                            customCar.Owners = carsForm.Owners.ToList();
                            Car car = new();
                            car.CopyFromCustomView(customCar);

                            Cars.Add(car);
                            context.Cars.Add(car);
                            context.SaveChanges();
                            SelectedCar = car;
                            CollectionView.Refresh();
                        } 
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show(e.Message);
                        carsForm.Close();
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
                    CustomViewCar customCar = new();
                    customCar.ShallowCopy(SelectedCar);
                    CarsForm carsForm = new()
                    {
                        Title = "Изменить автомобиль",
                        DataContext = customCar
                    };
                    carsForm.BodyTypeBox.ItemsSource = BodyTypeList;
                    foreach (var c in context.Clients)
                    {
                        carsForm.Clients.Add(c);
                    }
                    foreach (var c in customCar.Owners)
                    {
                        carsForm.Owners.Add(c);
                    }
                    carsForm.AddOwnerButton.IsEnabled = false;
                    carsForm.OwnerBox.IsEnabled = false;
                    try
                    {
                        if (carsForm.ShowDialog() == true)
                        {
                            Car car = new();
                            car = Cars.Where(a => customCar.CarId == a.CarId).FirstOrDefault();
                            car.CopyFromCustomView(customCar);
                            SelectedCar = car;
                            var editableCar = context.Cars.Where(a => a.CarId == car.CarId).FirstOrDefault();
                            context.Cars.Entry(editableCar).CurrentValues.SetValues(car);
                            context.SaveChanges();
                            CollectionView.Refresh();
                        }
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show(e.Message);
                        carsForm.Close();
                    }
                },
                obj => SelectedCar != null));
            }
        }

        private RelayCommand delete;
        public RelayCommand Delete
        {
            get
            {
                return delete ?? (delete = new(obj =>
                {
                    MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить данный автомобиль?\n" +
                        $"Серия:\t\t{SelectedCar.Serial}\nГос. Номер:\t{SelectedCar.CarNum}", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        Car removable = SelectedCar;
                        Cars.Remove(SelectedCar);
                        context.Cars.Remove(removable);
                        context.SaveChanges();
                        CollectionView.Refresh();
                    }
                },
                obj => SelectedCar != null));
            }
        }

        #endregion

    }
}
