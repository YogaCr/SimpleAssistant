﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.IO;
namespace SimpleAssistant
{
    class Perintah
    {
        string greeting;
        string lokasi = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDoc‌​uments), "SimpleAI");

        public bool ScriptDiam(ComboBox PilihanDiam, Image BalonTeks, Grid Diam)
        {
            BalonTeks.Visibility = Visibility.Hidden;
            Diam.Visibility = Visibility.Hidden;
            if (PilihanDiam.Text == "On")
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public void Ucapan(Label ucap)
        {
            if (DateTime.Now.Hour <= 10)
            {
                greeting = "Selamat Pagi\n";
            }
            else if (DateTime.Now.Hour > 10 && DateTime.Now.Hour < 15)
            {
                greeting = "Selamat Siang\n";
            }
            else if (DateTime.Now.Hour >= 15 && DateTime.Now.Hour <= 18)
            {
                greeting = "Selamat Sore\n";
            }
            else
            {
                greeting = "Selamat Malam\n";
            }
            greeting += Environment.UserName + "\nAda yang bisa saya bantu?";
            ucap.Content = greeting;
            ucap.Visibility = Visibility.Visible;
        }

        public string BukaNote(string nama)
        {
            string kalimat = "";
            try
            {
                using (StreamReader sr = new StreamReader(@lokasi + "/" + nama))
                {
                    kalimat = sr.ReadToEnd();
                }
            }
            catch
            {

            }
            return kalimat;
        }

        public void SimpanNote(RichTextBox Note, string nama)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(@lokasi + "/" + nama))
                {
                    sw.Write(new TextRange(Note.Document.ContentStart, Note.Document.ContentEnd).Text);
                    sw.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void LoadNote(ComboBox c, Grid Menu, Grid Note)
        {
            Menu.Visibility = Visibility.Hidden;
            Note.Visibility = Visibility.Visible;
            c.Items.Clear();
            System.IO.Directory.CreateDirectory(@lokasi);
            DirectoryInfo di = new DirectoryInfo(@lokasi);
            FileInfo[] tf = di.GetFiles("*.txt");
            foreach (var v in tf)
            {
                if (v.Exists)
                {
                    c.Items.Add(v.Name);
                }
            }
        }

        public void GantiNama(string namalama, string namabaru)
        {
            try { File.Move(@lokasi + "/" + namalama, @lokasi + "/" + namabaru); }
            catch
            {
                MessageBox.Show("Gagal merubah nama", "Error");
            }
        }

        public void HapusNote(string nama)
        {
            try
            {
                File.Delete(@lokasi + "/" + nama);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void TutupNote(Image BT, Grid Note)
        {
            BT.Visibility = Visibility.Hidden;
            Note.Visibility = Visibility.Hidden;
        }
    }
}
