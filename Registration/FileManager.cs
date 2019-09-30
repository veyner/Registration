using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Newtonsoft.Json;
using System.IO;

namespace Registration
{
    public class FileManager
    {
        /// <summary>
        /// Сохранение информации о пользователе в файл json
        /// </summary>
        /// <param name="user">текущий пользователь</param>
        public void SaveData(List<User> userList)
        {
            var fullSavePath = Path.Combine(Properties.Settings.Default.PathToData, "Data.json");
            using (var writer = new StreamWriter(fullSavePath))
            {
                var json = JsonConvert.SerializeObject(userList);
                writer.Write(json);
            }
        }

        /// <summary>
        /// Загрузка списка пользователей
        /// </summary>
        /// <returns>объект со списками информации</returns>
        public List<User> LoadUserData()
        {
            if (!Directory.Exists(Properties.Settings.Default.PathToData))
            {
                Directory.CreateDirectory(Properties.Settings.Default.PathToData);
            }
            var userList = new List<User>();
            var fullDataPath = Path.Combine(Properties.Settings.Default.PathToData, "Data.json");
            if (!File.Exists(fullDataPath))
            {
                var dataFile = File.Create(fullDataPath);
                SaveData(userList);
                dataFile.Dispose();
            }

            using (var reader = new StreamReader(fullDataPath))
            {
                var json = reader.ReadToEnd();
                userList = JsonConvert.DeserializeObject<List<User>>(json);
            }
            foreach (User user in userList)
            {
                var path = Path.Combine(Properties.Settings.Default.PathToPhoto, user.Guid.ToString() + ".png");
                var image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(path, UriKind.Relative);
                image.EndInit();
                image.Freeze(); //commenting this shows the image if the routine is called from the proper thread.
                user.Image = image;
            }
            return userList;
        }

        /// <summary>
        /// Загрузка фото
        /// </summary>
        /// <param name="user"></param>
        /// <param name="avatar"></param>
        public void LoadFoto(User user, Image avatar)
        {
            avatar.Source = new BitmapImage(new Uri(Path.Combine(Properties.Settings.Default.PathToPhoto, user.Guid.ToString() + ".png")));
        }

        /// <summary>
        /// загрузка дефолтного фото
        /// </summary>
        /// <param name = "avatar" ></ param >
        //public void LoadDefaultPhoto(Image avatar)
        //{
        //    avatar.ImageLocation = Path.Combine(Properties.Settings.Default.PathToPhoto, "DefaultPhoto.png");
        //    avatar.Name = "Default";
        //}

        /// <summary>
        /// сохранение фото
        /// </summary>
        /// <param name="user"></param>
        /// <param name="avatar"></param>
        public void SavePhoto(User user, Image avatar)
        {
            if (!Directory.Exists(Properties.Settings.Default.PathToPhoto))
            {
                Directory.CreateDirectory(Properties.Settings.Default.PathToPhoto);
            }
            var encoder = new PngBitmapEncoder();
            var path = Path.Combine(Properties.Settings.Default.PathToPhoto, user.Guid.ToString() + ".png");
            encoder.Frames.Add(BitmapFrame.Create((BitmapSource)avatar.Source));
            using (FileStream stream = new FileStream(path, FileMode.Create))
                encoder.Save(stream);
        }
    }
}