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
    public class CraftsmanViewModel : NotificationViewModel
    {
        private ObservableCollection<Craftsman> craftsmen = new();
        private List<ServiceCenter> serviceCenters = new();
        private Craftsman selectedCraftsman;
        public Craftsman SelectedCraftsman 
        { 
            get => selectedCraftsman;
            set
            {
                selectedCraftsman = value;
                OnPropertyChanged(nameof(SelectedCraftsman));
            }
        }
        public ObservableCollection<Craftsman> Craftsmen 
        { 
            get => craftsmen;
            set 
            { 
                craftsmen = value; 
                OnPropertyChanged(nameof(Craftsmen));
            }
        }

        public CraftsmanViewModel() 
        {
            context.ServiceCenters.Load();
            foreach(var craftsman in context.Craftsmen)
            {
                craftsmen.Add(craftsman);
            }
            ViewSource.Source = craftsmen;
        }

        public CraftsmanViewModel(ServiceCenterViewModel scvm) : this()
        {
            serviceCenters = scvm.ServiceCenters.ToList();
        }
        #region Commands

        private RelayCommand add;
        public RelayCommand Add
        {
            get
            {
                return add ?? (add = new(obj =>
                {
                    CustomViewCraftsman customCraftsman = new();
                    CraftsmanForm craftsmanForm = new()
                    {
                        Title = "Новый мастер",
                        DataContext = customCraftsman
                    };
                    craftsmanForm.ServiceCenterBox.ItemsSource = context.ServiceCenters.ToList();
                    craftsmanForm.ServiceCenterBox.SelectedValue = serviceCenters[0];
                    try
                    {
                        if (craftsmanForm.ShowDialog() == true)
                        {
                            ServiceCenter serviceCenter = (ServiceCenter)craftsmanForm.ServiceCenterBox.SelectedValue;
                            customCraftsman.ServiceCenter = serviceCenter;

                            Craftsman craftsman = new();
                            craftsman.CopyFromCustomView(customCraftsman);
                            Craftsmen.Add(craftsman);
                            context.Craftsmen.Add(craftsman);
                            context.SaveChanges();
                            SelectedCraftsman = craftsman;
                            CollectionView.Refresh();
                        } 
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show(e.Message);
                        craftsmanForm.Close();
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
                    CustomViewCraftsman customCraftsman = new();
                    customCraftsman.ShallowCopy(SelectedCraftsman, serviceCenters);
                    CraftsmanForm craftsmanForm = new()
                    {
                        Title = "Изменить мастера",
                        DataContext = customCraftsman
                    };
                    craftsmanForm.ServiceCenterBox.ItemsSource = context.ServiceCenters.ToList();
                    craftsmanForm.ServiceCenterBox.SelectedValue = customCraftsman.ServiceCenter;
                    craftsmanForm.AddButton.Content = "Принять";

                    if(craftsmanForm.ShowDialog() == true)
                    {
                        ServiceCenter serviceCenter = (ServiceCenter)craftsmanForm.ServiceCenterBox.SelectedValue;
                        customCraftsman.ServiceCenter = serviceCenter;

                        var craftsman = Craftsmen.Where(obj => obj.CraftsmanId == customCraftsman.CraftsmanId).FirstOrDefault();
                        craftsman.CopyFromCustomView(customCraftsman);
                        var editableCraftsman = context.Craftsmen.Where(a => a.CraftsmanId == craftsman.CraftsmanId).FirstOrDefault();
                        context.Craftsmen.Entry(editableCraftsman).CurrentValues.SetValues(craftsman);
                        context.SaveChanges();
                        SelectedCraftsman = craftsman;
                        CollectionView.Refresh();
                    }
                }, 
                obj => SelectedCraftsman != null));
            }
        }

        private RelayCommand delete;
        public RelayCommand Delete
        {
            get
            {
                return delete ?? (delete = new(obj =>
                {
                    MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить данного мастера:\n" +
                        $"{SelectedCraftsman.SecondName} {SelectedCraftsman.FirstName}?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        Craftsman removable = SelectedCraftsman;
                        Craftsmen.Remove(SelectedCraftsman);
                        context.Craftsmen.Remove(removable);
                        context.SaveChanges();
                        CollectionView.Refresh();
                    }
                },
                obj => SelectedCraftsman != null));
            }
        }
        #endregion
    }
}
