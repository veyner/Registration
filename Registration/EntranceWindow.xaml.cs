using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Registration
{
    /// <summary>
    /// Логика взаимодействия для EntranceWindow.xaml
    /// </summary>
    public partial class EntranceWindow : Window
    {
        private IEnumerable<User> _userList;

        public EntranceWindow(IEnumerable<User> userList)
        {
            InitializeComponent();
            _userList = userList;
        }

        private void GoToRegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            //new RegistrationWindow().Show();
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (User user in _userList)
            {
                if (user.Name == EnterNameTextBox.Text && user.Password == EnterPasswordBox.Password)
                {
                    new ExitWindow().Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Имя пользователя или пароль введены неверно");
                }
            }
        }
    }
}