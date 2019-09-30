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
using System.Net.Mail;
using System.Net;
using Microsoft.Win32;

namespace Registration
{
    /// <summary>
    /// Логика взаимодействия для RegistarionWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public List<User> userList;
        private FileManager _fileManager = new FileManager();
        private User user;
        private MainWindow _mainWindow;

        public RegistrationWindow(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            userList = new List<User>();
            InitializeComponent();

            if (userList != null)
            {
                userList = _fileManager.LoadUserData();
            }
        }

        private void ConfirmRegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            if (EnterPasswordBox.Password == ConfirmPasswordBox.Password)
            {
                user = new User();

                user.Guid = Guid.NewGuid();

                user.Name = NameTextBox.Text;
                user.Password = EnterPasswordBox.Password;
                user.Birthday = BirthdayTextBox.Text;
                user.Email = EmailTextBox.Text;
                user.ImagePath = user.Guid.ToString() + ".png";
                //AvatarImage.Name = user.Guid.ToString();
                _fileManager.SavePhoto(user, AvatarImage);

                var emailCode = SendEmail(user.Email);
                var checkRegWindow = new CheckRegistration(emailCode, this);
                checkRegWindow.Show();
            }
            else
            {
                MessageBox.Show("Пароли не совпадают!");
            }
        }

        public void EndRegistration()
        {
            userList.Add(user);
            _fileManager.SaveData(userList);
            MessageBox.Show("Регистрация прошла успешно");
        }

        private string SendEmail(string email)
        {
            MailAddress from = new MailAddress("auntentificationcheck@gmail.com", "Check registration");
            MailAddress to = new MailAddress(email);

            MailMessage message = new MailMessage(from, to);

            var random = new Random();
            var code = random.Next(1000, 9999).ToString();

            message.Subject = "Подтверждение регистрации";

            message.Body = "Ваш код регистрации - " + code.ToString();
            message.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

            smtp.Credentials = new NetworkCredential("auntentificationcheck@gmail.com", "regist2609");
            smtp.EnableSsl = true;
            smtp.Send(message);
            return code;
        }

        private void LoadAvatarButton_Click(object sender, RoutedEventArgs e)
        {
            OpenPhoto();
        }

        private void OpenPhoto()
        {
            var open = new OpenFileDialog();
            open.ShowDialog();
            //if (open.ShowDialog() == open.DialogResult.)
            //    return;
            AvatarImage.Source = new BitmapImage(new Uri(open.FileName));
        }
    }
}