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
    /// Логика взаимодействия для ResetPassword.xaml
    /// </summary>
    public partial class ResetPassword : Window
    {
        public string Password { get; set; }
        private string PasswordErrorMessage { get; set; }
        public string RepeatedPassword { get; set; }
        public ResetPassword()
        {
            InitializeComponent();
            DataContext = this;
        }

        public bool PasswordValidation()
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

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            Password = FirstPassword.Password;
            RepeatedPassword = RepeatPassword.Password;
            if (PasswordValidation())
            {
                DialogResult = true;
            }
            else
            {
                MessageBox.Show($"{PasswordErrorMessage}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }
    }
}
