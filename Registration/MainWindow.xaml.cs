using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Windows.Navigation;

namespace Registration
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FileManager _fileManager = new FileManager();
        private MainWindowViewModel _mainWindowViewModel;

        public MainWindow()
        {
            InitializeComponent();
            _mainWindowViewModel = new MainWindowViewModel();
            this.DataContext = _mainWindowViewModel;

            foreach (User user in _fileManager.LoadUserData())
            {
                _mainWindowViewModel.UserList.Add(user);
            }
            //if (_mainWindowViewModel.UserList != null)
            //{
            //    LoadPhoto();
            //}
        }

        private void GoToRegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            new RegistrationWindow(this).Show();
        }

        private void UserList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            new EntranceWindow(_mainWindowViewModel.UserList).Show();
        }

        private void LoadPhoto()
        {
            foreach (User user in _mainWindowViewModel.UserList)
            {
                var path = Path.Combine(Properties.Settings.Default.PathToPhoto, user.Guid.ToString() + ".png");
                user.Image = new BitmapImage(new Uri(path, UriKind.Relative));
            }
        }
    }
}