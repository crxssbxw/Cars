using Cars.Commands;
using Cars.Models;
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
    public class ClientViewModel : NotificationViewModel
    {
        private ObservableCollection<Client> clients = new();
        private Client selectedClient;
        public Client SelectedClient 
        { 
            get => selectedClient; 
            set 
            { 
                selectedClient = value;
                OnPropertyChanged(nameof(SelectedClient));
            } 
        }

        public ClientViewModel()
        {
            context.ClientCars.Load();
            context.Cars.Load();
            context.Contracts.Load();
            foreach(var client in context.Clients)
            {
                Clients.Add(client);
            }
            ViewSource.Source = Clients;
        }

        public ObservableCollection<Client> Clients 
        { 
            get => clients; 
            set 
            { 
                clients = value;
                OnPropertyChanged(nameof(Clients));
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
                    Client client = new();
                    ClientForm clientForm = new()
                    {
                        Title = "Новый клиент",
                        DataContext = client
                    };
                    try
                    {
                        if (clientForm.ShowDialog() == true)
                        {
                            Clients.Add(client);
                            context.Clients.Add(client);
                            context.SaveChanges();

                            SelectedClient = client;
                            CollectionView.Refresh();
                        } 
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show(e.Message);
                        clientForm.Close();
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
                    Client client = SelectedClient;
                    ClientForm clientForm = new()
                    {
                        Title = "Изменить клиента",
                        DataContext = client
                    };
                    clientForm.AddButton.Content = "Применить";
                    try
                    {
                        if (clientForm.ShowDialog() == true)
                        {
                            var c = Clients.Where(a => a.ClientId == client.ClientId).FirstOrDefault();
                            c = client;
                            var editableClient = context.Clients.Where(a => a.ClientId == client.ClientId).FirstOrDefault();
                            context.Clients.Entry(editableClient).CurrentValues.SetValues(client);
                            context.SaveChanges();
                            SelectedClient = client;
                            CollectionView.Refresh();
                        } 
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show(e.Message);
                        clientForm.Close();
                    }
                },
                obj => SelectedClient != null));
            }
        }

        private RelayCommand delete;
        public RelayCommand Delete
        {
            get
            {
                return delete ?? (delete = new(obj =>
                {
                    MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить данного клиента:\n" +
                        $"{SelectedClient.SecondName} {SelectedClient.FirstName}?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        Client removable = SelectedClient;
                        Clients.Remove(SelectedClient);
                        context.Clients.Remove(removable);
                        context.SaveChanges();
                        CollectionView.Refresh();
                    }
                },
                obj => SelectedClient != null));
            }
        }

        #endregion

    }
}
