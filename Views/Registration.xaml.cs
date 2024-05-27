using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Cars.Views
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public Models.Client Client { get; set; }
        public Models.Craftsman Craftsman { get; set; }
        public string RepeatedPassword { get; set; }
        private string LoginErrorMessage { get; set; }
        private string PasswordErrorMessage { get; set; }
        public Registration()
        {
            InitializeComponent();
            DataContext = this;
        }

        private bool LoginValidation()
        {
            LoginErrorMessage = string.Empty;
            if (string.IsNullOrWhiteSpace(Login))
            { 
                LoginErrorMessage = "Поле Логин не может быть пустым!";
                return false;
            }
            else
            {
                if (App.Context.Users.Any(a => a.Login == Login)) 
                { 
                    LoginErrorMessage = "Пользователь с данным логином существует!";
                    return false;
                }
                if (Login.Length < 6)
                {
                    LoginErrorMessage = "Длина логина минимум 6 символов!";
                    return false;
                }
            }
            return true;
        }

        private bool PasswordValidation()
        {
            char[] numbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            char[] specsymbols = { '$', '!', '#', '@', '%', '^' };
            PasswordErrorMessage = string.Empty;
            if (string.IsNullOrWhiteSpace(Password))
            {
                PasswordErrorMessage = "Поле Пароль не может быть пустым!";
                return false;
            }
            else
            {
                if (Password.Length < 6)
                {
                    PasswordErrorMessage = "Длина пароля минимум 6 символов";
                    return false;
                }
                if (!Password.Any(a => numbers.Contains(a)))
                {
                    PasswordErrorMessage = "Пароль должен содержать минимум одну цифру";
                    return false;
                }
                if (!Password.Any(a => specsymbols.Contains(a)))
                {
                    PasswordErrorMessage = "Пароль должен содержать минимум один спец-символ";
                    return false;
                }
                if (!Password.Any(a => char.IsAsciiLetter(a) && char.IsUpper(a)))
                {
                    PasswordErrorMessage = "Пароль должен содержать минимум одну прописную букву";
                    return false;
                }
                if (Password != RepeatedPassword)
                {
                    PasswordErrorMessage = "Повторите пароль";
                    return false;
                }
            }
            return true;
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            Password = FirstPassword.Password;
            RepeatedPassword = RepeatPassword.Password;
            if (LoginValidation() && PasswordValidation())
            {
                DialogResult = true;
            }
            else
            {
                MessageBox.Show($"{LoginErrorMessage}\n{PasswordErrorMessage}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        private void RoleBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Role == "Client")
            {
                CraftsmanBox.Visibility = Visibility.Collapsed;
                ClientBox.Visibility = Visibility.Visible;
            }
            if (Role == "Craftsman")
            {
                ClientBox.Visibility = Visibility.Collapsed;
                CraftsmanBox.Visibility = Visibility.Visible;
            }
            if (Role == "Admin")
            {
                ClientBox.Visibility = Visibility.Collapsed;
                CraftsmanBox.Visibility = Visibility.Collapsed;
            }
        }
    }
}
