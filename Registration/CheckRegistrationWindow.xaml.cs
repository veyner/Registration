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

namespace Registration
{
    /// <summary>
    /// Логика взаимодействия для CheckRegistration.xaml
    /// </summary>
    public partial class CheckRegistration : Window
    {
        private string _emailCode;
        public bool AllowRegistration;
        private RegistrationWindow _regWindow;
        public CheckRegistration(string code, RegistrationWindow registrationWindow)
        {
            _regWindow = registrationWindow;

            AllowRegistration = false;
            _emailCode = code;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_emailCode == EmailCodeTextBox.Text)
            {
                Close();
                _regWindow.EndRegistration();
                _regWindow.Close();
            }
            else
            {
                MessageBox.Show("Код введен неверно");
            }
        }
    }
}
