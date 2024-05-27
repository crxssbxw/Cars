using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Cars.ViewModels
{
    public class NotificationViewModel : ContextInheritance, INotifyPropertyChanged
    {
        public CollectionViewSource ViewSource { get; set; } = new();
        public ICollectionView CollectionView { get => ViewSource.View; }

        public event PropertyChangedEventHandler? PropertyChanged;
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
