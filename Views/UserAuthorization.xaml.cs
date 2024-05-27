using Cars.ViewModels;
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
    /// Логика взаимодействия для UserAuthorization.xaml
    /// </summary>
    public partial class UserAuthorization : Window
    {
        private AuthorizationViewModel authorizationViewModel = new();
        public AuthorizationViewModel AuthorizationViewModel 
        { 
            get => authorizationViewModel; 
            set => authorizationViewModel = value; 
        }

        public UserAuthorization()
        {
            DataContext = AuthorizationViewModel;
            InitializeComponent();
        }

        private void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            if (AuthorizationViewModel.Authorization())
            {
                MainWindow mainWindow = new MainWindow();

                switch (App.CurrentUser.Role)
                {
                    case "Admin":
                        mainWindow.AdminControl.Visibility = Visibility.Visible;
                        break;
                    case "Client":
                        mainWindow.ClientControl.Visibility = Visibility.Visible;
                        break;
                    case "Craftsman":
                        mainWindow.CraftsmanControl.Visibility = Visibility.Visible;
                        break;
                }

                mainWindow.Show();
                this.Close();
            }
        }

        private void PasswordField_PasswordChanged(object sender, RoutedEventArgs e)
        {
            AuthorizationViewModel.Password = PasswordField.Password;
        }
    }
}