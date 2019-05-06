using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OAC
{
    /// <summary>
    /// Interaction logic for UC_sobre.xaml
    /// </summary>
    public partial class UC_sobre : UserControl
    {
        public UC_sobre()
        {
            InitializeComponent();
        }

        private void bt_voltar_Click(object sender, RoutedEventArgs e)
        {
            var w = Application.Current.Windows[MainWindow.cont_window];
            w.Hide();
            MainWindow.cont_window = MainWindow.cont_window + 1;
            MainWindow main = new MainWindow();
            main.ShowDialog();
        }
    }
}
