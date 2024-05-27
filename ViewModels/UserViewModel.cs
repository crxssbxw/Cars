using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Cars.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cars.Commands;
using Cars.Views;
using Microsoft.EntityFrameworkCore;

namespace Cars.ViewModels
{
    public class UserViewModel : NotificationViewModel
    {
        private User selectedUser;
        public User SelectedUser
        {
            get => selectedUser;
            set
            {
                selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }

        private ObservableCollection<User> users = new();
        public ObservableCollection<User> Users
        {
            get => users; 
            set 
            { 
                users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public List<string> roles = new()
        {
            "Admin", "Client", "Craftsman"
        };

        public UserViewModel()
        {
            context.Clients.Load();
            context.Craftsmen.Load();
            foreach(var user in context.Users)
            {
                Users.Add(user);
            }
            ViewSource.Source = Users;
        }

        #region Commands

        private RelayCommand registration;
        public RelayCommand Registration
        {
            get
            {
                return registration ?? (registration = new(obj =>
                {
                    User newUser = new();
                    Registration registration = new();
                    registration.RoleBox.ItemsSource = roles;

                    ObservableCollection<Models.Client> clients = Admin.ClientViewModel.Clients;
                    ObservableCollection<Models.Craftsman> craftsmen = Admin.CraftsmanViewModel.Craftsmen;

                    registration.ClientBox.ItemsSource = clients;
                    registration.CraftsmanBox.ItemsSource = craftsmen;

                    if(registration.ShowDialog() == true)
                    {
                        newUser.Login = registration.Login;
                        newUser.Role = registration.Role;
                        newUser.Username = registration.Login + "_Username";
                        newUser.Password = registration.Password;
                        newUser.Client = registration.Client;
                        newUser.Craftsman = registration.Craftsman;
                        if(newUser.Client != null)
                        { newUser.ClientId = registration.Client.ClientId; }
                        if(newUser.Craftsman != null)
                        { newUser.CraftsmanId = registration.Craftsman.CraftsmanId; }

                        Users.Add(newUser);
                        context.Users.Add(newUser);
                        context.SaveChanges();
                    }
                },
                obj => true));
            }
        }

        public RelayCommand resetPassword;
        public RelayCommand ResetPassword
        {
            get
            {
                return resetPassword ?? (resetPassword = new(obj =>
                {
                    Views.ResetPassword resetPassword = new();

                    if(resetPassword.ShowDialog() == true) 
                    {
                        SelectedUser.Password = resetPassword.Password;
                        User user = Users.Where(a => a == SelectedUser).FirstOrDefault();

                        var editableUser = context.Users.Where(a => a.UserId == user.UserId).FirstOrDefault();
                        context.Users.Entry(editableUser).CurrentValues.SetValues(user);
                        context.SaveChanges();

                        user = SelectedUser;
                    }
                },
                obj => SelectedUser != null));
            }
        }

        #endregion
    }
}
