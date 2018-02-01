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

namespace SimpleAssistant
{
    /// <summary>
    /// Interaction logic for InputForm.xaml
    /// </summary>
    public partial class InputForm : Window
    {
        public InputForm(string title)
        {
            InitializeComponent();
            this.Title = title;
            Label.Content = title;
        }
        public string hasil() {
            return textBoxNama.Text+".txt";
        }

        private void Ok(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
