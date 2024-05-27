using System.Configuration;
using System.Data;
using Cars.Models;
using System.Windows;

namespace Cars
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    
    public partial class App : Application
    {
        public static CarsContext Context { get; set; } = new();
        private static User? currentUser { get; set; }
        public static User? CurrentUser 
        {
            get => currentUser; 
            set => currentUser = value; 
        }

    }
}
