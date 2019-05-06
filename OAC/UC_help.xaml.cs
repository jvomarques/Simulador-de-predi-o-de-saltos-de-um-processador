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
    /// Interaction logic for UC_help.xaml
    /// </summary>
    public partial class UC_help : UserControl
    {
        public UC_help()
        {
            InitializeComponent();
            radioButton1.IsChecked = true;
            tb_intro.Text = "Em processadores de arquitetura Pipeline, as predições de salto são técnicas implementadas que visam reduzir os conflitos de controle através da previsão correta de que os desvios condicionais vão ou não ser tomados na execução de um programa.\n\nO objetivo é que o máximo de decisões corretas sejam tomadas, evitando ao máximo o uso de bolhas (stalls) no processador. As técnicas de predição utilizadas nesse software são:";
        }

        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
            tb_explica.Text = "A informação do último desvio é guardada em um bit. Assim, caso esse bit informe que a decisão foi tomada, a próxima é tomada e vice versa. Ou seja, o próximo desvio é sempre igual ao desvio anterior.";
        }

        private void radioButton2_Checked(object sender, RoutedEventArgs e)
        {
            tb_explica.Text = "Similar a predição por 1 bit, porém a mémoria de saltos contém 2 bits. Desse modo são necessárias duas previsões erradas para que a definição do proximo desvio seja tomada.";
        }

        private void radioButton3_Checked(object sender, RoutedEventArgs e)
        {
            tb_explica.Text = "Em uma predição de saltos realizada da maneira 'adaptativa de dois níveis', um conjunto de padrões de previsões é armanezado. Esses padrões são utilizados para tentar prever os saltos posteriores. Por exemplo, pode-se armazenar a previsão 'T' para o padrão de sequência 'T N'. Assim, sempre que o padrão 'T N' aparecer, a previsão 'T' será usada.";
        }

        private void radioButton4_Checked(object sender, RoutedEventArgs e)
        {
            tb_explica.Text = "Nessa solução, proposta pela equipe, a próxima predição é tomada de acordo com a mais ultilizada anteriormente. Para isso, um contador aumenta se uma decisão for tomada, e descresce caso não seja tomada. Se o contador for positivo, a próximo desvio é tomado, caso contrário, não é tomado.";
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var w = Application.Current.Windows[MainWindow.cont_window];
            w.Hide();
            MainWindow.cont_window = MainWindow.cont_window + 1;
            MainWindow main = new MainWindow();
            main.ShowDialog();
        }
    }
}
