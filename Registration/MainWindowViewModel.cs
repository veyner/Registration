using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<User> UserList { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel()
        {
            UserList = new ObservableCollection<User>();
        }
    }
}