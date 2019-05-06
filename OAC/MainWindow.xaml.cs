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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Grid g_global = new Grid();
        public static int cont_window;
        public MainWindow()
        {
            InitializeComponent();
            g_global = g_main;
        }

       

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            g_global.Children.Clear();
            UC_tela1 uc = new UC_tela1();
            g_global.Children.Add(uc);
        }

        private void bt_ajuda_Click(object sender, RoutedEventArgs e)
        {
            g_global.Children.Clear();
            UC_help uc = new UC_help();
            g_global.Children.Add(uc);
        }

        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            g_global.Children.Clear();
            UC_sobre uc = new UC_sobre();
            g_global.Children.Add(uc);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Application.Current.Shutdown();
        }
     
    }
}
