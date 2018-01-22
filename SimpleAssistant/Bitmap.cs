using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace SimpleAssistant
{
    class Bitmap
    {
        List<BitmapImage> kiri = new List<BitmapImage>();
        List<BitmapImage> kanan = new List<BitmapImage>();

        BitmapImage ClickedKiri = new BitmapImage(new Uri("ClickedKiri.png", UriKind.Relative));
        BitmapImage ClickedKanan = new BitmapImage(new Uri("ClickedKanan.png", UriKind.Relative));
        public Bitmap() {
            kiri.Add(new BitmapImage(new Uri("kiri1.png", UriKind.Relative)));
            kiri.Add(new BitmapImage(new Uri("kiri2.png", UriKind.Relative)));
            kiri.Add(new BitmapImage(new Uri("kiri3.png", UriKind.Relative)));
            kiri.Add(new BitmapImage(new Uri("kiri4.png", UriKind.Relative)));
            kiri.Add(new BitmapImage(new Uri("kiri5.png", UriKind.Relative)));
            kiri.Add(new BitmapImage(new Uri("kiri6.png", UriKind.Relative)));
            kanan.Add(new BitmapImage(new Uri("kanan1.png", UriKind.Relative)));
            kanan.Add(new BitmapImage(new Uri("kanan2.png", UriKind.Relative)));
            kanan.Add(new BitmapImage(new Uri("kanan3.png", UriKind.Relative)));
            kanan.Add(new BitmapImage(new Uri("kanan4.png", UriKind.Relative)));
            kanan.Add(new BitmapImage(new Uri("kanan5.png", UriKind.Relative)));
            kanan.Add(new BitmapImage(new Uri("kanan6.png", UriKind.Relative)));
        }
        public BitmapImage getbitmapkiri(int x) {
            return kiri[x];
        }
        public BitmapImage getbitmapkanan(int x)
        {
            return kanan[x];
        }
        public BitmapImage getclickedkiri()
        {
            return ClickedKiri;
        }
        public BitmapImage getclickedkanan()
        {
            return ClickedKanan;
        }

    }
}
