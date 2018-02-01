using System;
using System.Drawing;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.IO;
namespace SimpleAssistant
{
    public partial class MainWindow : Window
    {

        DispatcherTimer waktu=new DispatcherTimer();
        DispatcherTimer tunggu = new DispatcherTimer();

        Perintah p = new Perintah();
        Bitmap b = new Bitmap();
        Random rand = new Random();

        bool kiri = true, gerak = true, klik = true, pindah = false,BUgerak;
        int jarak,tempuh;
        byte r,x,t=0;

        string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDoc‌​uments), "SimpleAI");

        public MainWindow()
        {
            InitializeComponent();

            this.Top = SystemParameters.WorkArea.Height - this.Height;
            this.Topmost = true;
            
            waktu.Tick += new EventHandler(waktu_tick);
            waktu.Interval = new TimeSpan(0, 0, 0, 0, 50);
            tunggu.Tick += new EventHandler(tunggu_tick);
            tunggu.Interval = new TimeSpan(0, 0, 1);
            
            this.Left= SystemParameters.WorkArea.Width;
            jarak = (int)SystemParameters.WorkArea.Width / 2;
            waktu.Start();
        }

        private void NotePilih(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedItem != null)
            {
                if ((sender as ComboBox) == ComboNoteKanan)
                {
                    TextKanan.Document.Blocks.Clear();
                    TextKanan.Document.Blocks.Add(new Paragraph(new Run(p.BukaNote(ComboNoteKanan.SelectedItem.ToString()))));
                }
                if ((sender as ComboBox) == ComboNoteKiri)
                {
                    TextKiri.Document.Blocks.Clear();
                    TextKiri.Document.Blocks.Add(new Paragraph(new Run(p.BukaNote(ComboNoteKiri.SelectedItem.ToString()))));
                }
            }
        }

        private void TutupNote(object sender, RoutedEventArgs e)
        {
            if ((sender as Button) == TutupNoteKanan)
            {
                p.TutupNote(BTKanan, NoteKanan);
            }
            else if ((sender as Button) == TutupNoteKiri)
            {
                p.TutupNote(BTKiri, NoteKiri);
            }
            gerak = BUgerak;
            tentu();
        }

        private void SimpanNote(object sender, KeyEventArgs e)
        {
            try
            {
                if (TextKanan.Visibility == Visibility.Visible)
                {
                    p.SimpanNote(TextKanan, ComboNoteKanan.SelectedItem.ToString());
                }
                else if (TextKiri.Visibility == Visibility.Visible)
                {
                    p.SimpanNote(TextKiri, ComboNoteKiri.SelectedItem.ToString());
                }
            }
            catch
            {
                MessageBox.Show("ERROR");
            }
        }

        private void TambahNote(object sender, RoutedEventArgs e)
        {
            InputForm i = new InputForm("Tambah Note");
            if (i.ShowDialog() == true) {
                using (StreamWriter sw = new StreamWriter(@path +"/"+ i.hasil())) {
                    sw.Close();
                }
                if ((sender as Button) == TambahNoteKanan)
                {
                    p.LoadNote(ComboNoteKanan, MenuKanan, NoteKanan);
                }
                if ((sender as Button) == TambahNoteKiri)
                {
                    p.LoadNote(ComboNoteKiri, MenuKiri, NoteKiri);
                }
            }
        }

        private void HapusNote(object sender, RoutedEventArgs e)
        {
            if ((sender as Button) == HapusKanan)
            {
                p.HapusNote(ComboNoteKanan.SelectedItem.ToString());
                p.LoadNote(ComboNoteKanan, MenuKanan, NoteKanan);
            }
            else if ((sender as Button) == HapusKiri)
            {
                p.HapusNote(ComboNoteKiri.SelectedItem.ToString());
                p.LoadNote(ComboNoteKiri, MenuKiri, NoteKiri);
            }
        }

        private void GantiNote(object sender, RoutedEventArgs e)
        {
            InputForm i = new InputForm("Inputkan Nama Baru");
            if (i.ShowDialog() == true) {
                if ((sender as Button).Name == "GantiNamaKanan")
                {
                    p.GantiNama(ComboNoteKanan.SelectedItem.ToString(), i.hasil());
                    p.LoadNote(ComboNoteKanan, MenuKanan, NoteKanan);
                }
                if ((sender as Button).Name == "GantiNamaKiri")
                {
                    p.GantiNama(ComboNoteKiri.SelectedItem.ToString(), i.hasil());
                    p.LoadNote(ComboNoteKiri, MenuKiri, NoteKiri);
                }
            }
        }

        private void Diam(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Name == "buttonDiamKanan")
            {
                gerak = p.ScriptDiam(DiamSwitchKanan, BTKanan, DiamKanan);
            }
            else if ((sender as Button).Name == "buttonDiamKiri") {
                gerak = p.ScriptDiam(DiamSwitchKiri, BTKiri, DiamKiri);
            }
            klik = true;
            if ((this.Left + this.Width / 2) < SystemParameters.WorkArea.Width / 2)
            {
                image.Source = b.getbitmapkanan(0);
            }
            else
            {
                image.Source = b.getbitmapkiri(0);
            }
        }

        private void Pilih(object sender, RoutedEventArgs e)
        {
            klik = false;
            if ((sender as Button).Name == "PilihKanan")
            {
                if (PilihanKanan.SelectedIndex == 0)
                {
                    MenuKanan.Visibility = Visibility.Hidden;
                    PindahKanan.Visibility = Visibility.Visible;
                }
                else if (PilihanKanan.SelectedIndex == 1)
                {
                    MenuKanan.Visibility = Visibility.Hidden;
                    DiamKanan.Visibility = Visibility.Visible;
                    if (gerak) { DiamSwitchKanan.SelectedIndex = 1; }
                    else { DiamSwitchKanan.SelectedIndex = 0; }
                }
                else if (PilihanKanan.SelectedIndex == 2)
                {
                    p.LoadNote(ComboNoteKanan, MenuKanan, NoteKanan);
                    waktu.Stop();
                    BUgerak = gerak;
                    gerak = false;
                    tunggu.Stop();
                }
            }
            else if ((sender as Button).Name == "PilihKiri")
            {
                if (PilihanKiri.SelectedIndex == 0)
                {
                    MenuKiri.Visibility = Visibility.Hidden;
                    PindahKiri.Visibility = Visibility.Visible;
                }
                else if (PilihanKiri.SelectedIndex == 1)
                {
                    MenuKiri.Visibility = Visibility.Hidden;
                    DiamKiri.Visibility = Visibility.Visible;
                    if (gerak) { DiamSwitchKiri.SelectedIndex = 1; }
                    else { DiamSwitchKiri.SelectedIndex = 0; }
                }
                else if (PilihanKiri.SelectedIndex == 2)
                {
                    p.LoadNote(ComboNoteKiri, MenuKiri, NoteKiri);
                    waktu.Stop();
                    BUgerak = gerak;
                    gerak = false;
                    tunggu.Stop();
                }
            }
            else if ((sender as Button).Name == "PindahKananButton") {
                pindah = true;
                BUgerak = gerak;
                gerak = false;
                tunggu.Stop();
                waktu.Start();
                BTKanan.Visibility = Visibility.Hidden;
                PindahKanan.Visibility = Visibility.Hidden;
            }
            else if ((sender as Button).Name == "PindahKiriButton")
            {
                pindah = true;
                BUgerak = gerak;
                gerak = false;
                tunggu.Stop();
                waktu.Start();
                BTKiri.Visibility = Visibility.Hidden;
                PindahKiri.Visibility = Visibility.Hidden;
            }
        }

        private void Klik(object sender, MouseButtonEventArgs e)
        {
            waktu.Stop();
            t = 0;
            tunggu.Start();
            if (klik)
            {
                if ((this.Left + this.Width / 2) < SystemParameters.WorkArea.Width / 2)
                {
                    p.Ucapan(GreetingKanan);
                    image.Source = b.getclickedkanan();
                    MenuKanan.Visibility = Visibility.Visible;
                    BTKanan.Visibility = Visibility.Visible;
                    
                }
                else
                {
                    p.Ucapan(GreetingKiri);
                    image.Source = b.getclickedkiri();
                    BTKiri.Visibility = Visibility.Visible;
                    MenuKiri.Visibility = Visibility.Visible;
                }
            }
            if (pindah) {
                x = 0;
                pindah = false;
                gerak = BUgerak;
                klik = true;
                tunggu.Start();
            }
        }

        private void waktu_tick(object o, EventArgs e) {
            if (gerak)
            {
                if (kiri)
                {
                    if (tempuh < jarak)
                    {   image.Source = b.getbitmapkiri(x);
                        this.Left -= 10;
                        tempuh += 10;}
                    else
                    {   image.Source = b.getbitmapkiri(0);
                        tunggu.Start();}
                }
                else
                {
                    if (tempuh < jarak)
                    {   image.Source = b.getbitmapkanan(x);
                        this.Left += 10;
                        tempuh += 10;}
                    else
                    {   image.Source = b.getbitmapkanan(0);
                        tunggu.Start();}
                }
                if (x < 5){x++;}
                else{x = 0;}
            }
            else if (pindah) {
                double p = System.Windows.Forms.Cursor.Position.X;
                    if (p < this.Left + this.Width / 2)
                    {   image.Source = b.getbitmapkiri(x);
                        this.Left -= 30;}
                    else
                    {   image.Source = b.getbitmapkanan(x);
                        this.Left += 30;}
                    if (x < 5){x++;}
                    else{x = 0;}
            }
        }

        private void tunggu_tick(object o, EventArgs e) {
            waktu.Stop();
            if (t < 5){t++;}
            else {
                BTKanan.Visibility = Visibility.Hidden;
                MenuKanan.Visibility = Visibility.Hidden;
                BTKiri.Visibility = Visibility.Hidden;
                MenuKiri.Visibility = Visibility.Hidden;
                DiamKiri.Visibility = Visibility.Hidden;
                if (!gerak) {
                    if ((this.Left + this.Width / 2) < SystemParameters.WorkArea.Width / 2){image.Source = b.getbitmapkanan(0);}
                    else{image.Source = b.getbitmapkiri(0);}
                }
                tentu();
            }
        }

        private void tentu() {
            tunggu.Stop();
            klik = true;
            pindah = false;
            if (gerak)
            {
                r = (byte)rand.Next(0, 5);
                if ((r <= 2 && this.Left > 0) || (this.Left + this.Width) > this.Width)
                {
                    kiri = true;
                    jarak = rand.Next(0, (int)this.Left) + 100;
                }
                else if ((r > 2 && (this.Left + this.Width) < this.Width) || this.Left < 0)
                {
                    kiri = false;
                    jarak = rand.Next(0, (int)(SystemParameters.WorkArea.Width - (this.Left + this.Width))) + 100;
                }
                x = 0;
                tempuh = 0;
                t = 0;
                waktu.Start();
            }
        }
    }
}
