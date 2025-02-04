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

namespace KTPMUD
{
    /// <summary>
    /// Interaction logic for caphuyen.xaml
    /// </summary>
    public partial class caphuyen : Window
    {
        public caphuyen()
        {
            InitializeComponent();
        }

        private void truynhapcapxa(object sender, RoutedEventArgs e)
        {
             dongvat dongvat = new dongvat();
            dongvat.Show();
            this.Hide();
        }

        private void cososanxuat(object sender, RoutedEventArgs e)
        {
            quanlygo quanlygo = new quanlygo();
            quanlygo.Show();
            this.Hide();
        }

        private void cosoluutru(object sender, RoutedEventArgs e)
        {
            giongcaytrong giongcaytrong = new giongcaytrong();
            giongcaytrong.Show();
            this.Hide();
        }
    }
}
