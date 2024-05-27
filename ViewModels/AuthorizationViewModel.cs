using Cars.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cars.ViewModels
{
    public class AuthorizationViewModel : NotificationViewModel
    {
        private string? login;
        private string? password;
        protected User? FindUser { get; set; }
        public CarsContext Context 
        { get => context; set => context = value; }

        public string Login
        {
            set 
            {
                login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        public string Password
        {
            set 
            { 
                password = value; 
                OnPropertyChanged(nameof(Password));
            }
        }

        private string? loginErrorMessage;
        private string? passwordErrorMessage;
        public string? LoginErrorMessage 
        { 
            get => loginErrorMessage;
            set
            {
                loginErrorMessage = value;
                OnPropertyChanged(nameof(LoginErrorMessage));
            }
        } 
        public string? PasswordErrorMessage 
        { 
            get => passwordErrorMessage;
            set
            {
                passwordErrorMessage = value;
                OnPropertyChanged(nameof(PasswordErrorMessage));
            } 
        }

        public AuthorizationViewModel(string login, string password) : this()
        {
            this.login = login;
            this.password = password;
        }

        public AuthorizationViewModel()
        {
            context.Users.Load();
            context.Craftsmen.Load();
            context.Clients.Load();
            context.ServiceCenters.Load();
        }

        public bool LoginValidation()
        {
            FindUser = context.Users
                .Where(x => x.Login.ToLower() == login.ToLower())
                .FirstOrDefault();
            if(FindUser == null)
            {
                LoginErrorMessage = "Пользователя не существует";
                MessageBox.Show("Пользователя с введенными данными не существует", LoginErrorMessage, MessageBoxButton.OK);
                return false;
            }
            LoginErrorMessage = string.Empty;
            return true;
        }

        public bool PasswordValidation() 
        {
            if (LoginValidation())
            {
                if (password != FindUser.Password)
                {
                    PasswordErrorMessage = "Пароль неверный";
                }
                else return true;
            }
            else
            {
                PasswordErrorMessage = string.Empty;
            }
            return false; 
        }

        public bool Authorization()
        {
            if (LoginValidation() && PasswordValidation())
            {
                App.CurrentUser = FindUser;
                return true;
            }
            else 
            {
                return false; 
            }
        }
    }
}
