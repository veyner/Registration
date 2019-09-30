using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Registration
{
    public class User : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string Birthday { get; set; }
        public string Password { get; set; }
        public Guid Guid { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }

        public BitmapImage Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Image)));
            }
        }

        private BitmapImage _image;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}