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

using System.IO;

namespace OAC
{
    /// <summary>
    /// Interaction logic for UC_tela1.xaml
    /// </summary>
    public partial class UC_tela1 : UserControl
    {
        List<String> lista_entrada = new List<String>();
        String entrada;
        String entrada_txt;
        String path;
        TextBox tb_backup;
        bool checou_resultado;
        double percentual_1bit, percentual_2bits, percentual_maisfrequente, percentual_adaptativo;

        Dictionary<string, string> historico_adp = new Dictionary<string,string>();

        public UC_tela1()
        {
            InitializeComponent();
            g_saida.Visibility = Visibility.Hidden;
            gb_resultados.Visibility = Visibility.Hidden;
            
            historico_adp.Add("TT", "");
            historico_adp.Add("TN", "");
            historico_adp.Add("NT", "");
            historico_adp.Add("NN", "");
        }

        public String valorMaisFrequente(List<String> lista)
        {
            int contT = 0;
            int contN = 0;

            for (int i = 0; i < lista.Count(); i++)
            {
                if (lista[i] == "T")
                    contT++;
                else
                    contN++;
            }

   

            if (contT > contN)
                return "T";
            else if(contN > contT)
                return "N";
            else if (rd_true.IsChecked == true)
                return "T";
            else
                return "N";
                
        }

        private void bt_escolherarquivo_Click(object sender, RoutedEventArgs e)
        {
            lista_entrada = new List<String>();

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                path = dlg.FileName;
            }

            using (StreamReader sr = new StreamReader(path))
            {
                tblock_entrada.Text = sr.ReadToEnd();

                if (tblock_entrada.Text[0] != ' ')
                    tblock_entrada.Text = " " + tblock_entrada.Text;

                entrada_txt = tblock_entrada.Text;

                for (int i = 0; i < entrada_txt.Length; i++)
                {
                    if (entrada_txt[i].Equals('T'))
                        lista_entrada.Add(entrada_txt[i].ToString());
                    else if (entrada_txt[i].Equals('N'))
                        lista_entrada.Add(entrada_txt[i].ToString());
                }
            }

            try
            {
                g_saida.Visibility = Visibility.Hidden;
                gb_resultados.Visibility = Visibility.Hidden;
                bt_calcular.IsEnabled = true;
            }
            catch (Exception ex)
            {

            }
            
        }

        private void img_true_MouseDown(object sender, MouseButtonEventArgs e)
        {
            img_true.Visibility = Visibility.Hidden;
            img_false.Visibility = Visibility.Visible;

            lb_entrada1.Content = "N";

            lb_entrada1.Foreground = Brushes.Red;

        }

        private void img_false_MouseDown(object sender, MouseButtonEventArgs e)
        {
            img_false.Visibility = Visibility.Hidden;
            img_true.Visibility = Visibility.Visible;
            lb_entrada1.Content = "T";
            
            //#FF05CE05
            lb_entrada1.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x05, 0xCE, 0x05));
        }

        private void bt_add_Click(object sender, RoutedEventArgs e)
        {
            g_saida.Visibility = Visibility.Hidden;
            gb_resultados.Visibility = Visibility.Hidden;
            bt_calcular.IsEnabled = true;
            
            if (!checou_resultado)
            {
                tblock_entrada.Text = tblock_entrada.Text + " " + lb_entrada1.Content;
                checou_resultado = false;
                rd_1bit.IsChecked = false;
                rd_2bits.IsChecked = false;
                rd_adaptativo.IsChecked = false;
                rd_memoria.IsChecked = false;

            }
            else
            {
                tblock_entrada = tb_backup;
                tblock_entrada.Text = entrada + " " + lb_entrada1.Content;
                checou_resultado = false;
                rd_1bit.IsChecked = false;
                rd_2bits.IsChecked = false;
                rd_adaptativo.IsChecked = false;
                rd_memoria.IsChecked = false;
            }

            lista_entrada.Add(lb_entrada1.Content.ToString());

        }

        /*BOTAO REMOVE*/
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            g_saida.Visibility = Visibility.Hidden;
            gb_resultados.Visibility = Visibility.Hidden;
            bt_calcular.IsEnabled = true;

            if (lista_entrada.Count() != 0)
            {
                if (!checou_resultado)
                {
                    String novo_valor = tblock_entrada.Text.Substring(0, lista_entrada.Count() * 2 - 1);
                    tblock_entrada.Text = novo_valor;

                    int ultima_posicao = lista_entrada.Count() - 1;
                    lista_entrada.RemoveAt(ultima_posicao);
                    checou_resultado = false;
                    rd_1bit.IsChecked = false;
                    rd_2bits.IsChecked = false;
                    rd_adaptativo.IsChecked = false;
                    rd_memoria.IsChecked = false;
                }
                else
                {
                    String novo_valor = entrada.Substring(0, lista_entrada.Count() * 2 - 1);
                    tblock_entrada.Text = novo_valor;

                    int ultima_posicao = lista_entrada.Count() - 1;
                    lista_entrada.RemoveAt(ultima_posicao);
                    checou_resultado = false;
                    rd_1bit.IsChecked = false;
                    rd_2bits.IsChecked = false;
                    rd_adaptativo.IsChecked = false;
                    rd_memoria.IsChecked = false;
                }
            }
            else
                MessageBox.Show("Valor de entrada vazio!");
        }
        /*FIM - BOTAO REMOVE*/

        private void bt_calcular_Click(object sender, RoutedEventArgs e)
        {
            rd_1bit.IsChecked = false;
            rd_2bits.IsChecked = false;
            rd_adaptativo.IsChecked = false;
            rd_memoria.IsChecked = false;
            bt_calcular.IsEnabled = false;
            tb_backup = tblock_entrada;
            
            if (lista_entrada.Count != 0)
            {
                String valor = "";

                int acertou = 0, errou = 0;

                /*PREDICAO DE 1 BIT*/
                if (rd_true.IsChecked == true)
                    valor = "T";
                else
                    valor = "N";

                for (int i = 0; i < lista_entrada.Count(); i++)
                {
                    if (lista_entrada[i] == valor)
                        acertou++;

                    valor = lista_entrada[i];
                }
                Convert.ToDouble(lista_entrada.Count());
                percentual_1bit = Convert.ToDouble(Convert.ToDouble(acertou) / Convert.ToDouble(lista_entrada.Count()));
                lb_1bit.Content = Math.Round(percentual_1bit * 100, 3) + "%";
                /*FIM - PREDICAO DE 1 BIT*/


                /*PREDICAO DE 2 BITS*/
                if (rd_true.IsChecked == true)
                    valor = "T";
                else
                    valor = "N";

                acertou = 0;
                errou = 0;

                for (int i = 0; i < lista_entrada.Count(); i++)
                {
                    if (lista_entrada[i] == valor)
                    {
                        acertou++;
                        errou = 0;
                    }
                    else
                    {
                        errou++;
                        if (errou == 2)
                        {
                           valor = lista_entrada[i];
                           errou = 0;
                        }
                    }
                }
                percentual_2bits = Convert.ToDouble(Convert.ToDouble(acertou) / Convert.ToDouble(lista_entrada.Count()));
                lb_2bits.Content = Math.Round(percentual_2bits * 100, 3) + "%";
                /*FIM - PREDICAO DE 2 BITS*/


                /*PREDICAO DE ACORDO COM O VALOR MAIS FREQUENTE*/
                if (rd_true.IsChecked == true)
                    valor = "T";
                else
                    valor = "N";

                acertou = 0;
                List<string> valores_lidos = new List<string>();
                for (int i = 0; i < lista_entrada.Count(); i++)
                {
                    if (lista_entrada[i] == valor)
                        acertou++;

                    valores_lidos.Add(lista_entrada[i]);
                    valor = valorMaisFrequente(valores_lidos);
                }
                
                percentual_maisfrequente = Convert.ToDouble(Convert.ToDouble(acertou) / Convert.ToDouble(lista_entrada.Count()));
                lb_memoria.Content = Math.Round(percentual_maisfrequente * 100, 3) + "%";
                /*FIM - PREDICAO DE ACORDO COM O VALOR MAIS FREQUENTE*/

                /*ADAPTATIVA DE DOIS NÍVEIS*/
                if (rd_true.IsChecked == true)
                {
                    historico_adp["TT"] = "T";
                    historico_adp["TN"] = "T";
                    historico_adp["NT"] = "T";
                    historico_adp["NN"] = "T";
                    valor = "T";
                }
                else
                {
                    historico_adp["TT"] = "N";
                    historico_adp["TN"] = "N";
                    historico_adp["NT"] = "N";
                    historico_adp["NN"] = "N";
                    valor = "N";
                }

                acertou = 0;

                for (int i = 0; i < lista_entrada.Count(); i++)
                {
                    if (i < 2)
                    {
                        if (lista_entrada[i] == valor)
                            acertou++;
                        else
                            errou++;
                    }
                    else
                    {
                        string key = lista_entrada[i-1] + lista_entrada[i-2];

                        if (lista_entrada[i] == historico_adp[key])
                            acertou++;
                        
                        historico_adp[key] = lista_entrada[i];
                    }
                 }
                
                percentual_adaptativo = Convert.ToDouble(Convert.ToDouble(acertou) / Convert.ToDouble(lista_entrada.Count()));
                lb_adaptativo.Content = Math.Round(percentual_adaptativo * 100, 3) + "%";
                /*FIM ADAPTATIVA DE DOIS NÍVEIS*/


                entrada = tblock_entrada.Text;
                g_saida.Visibility = Visibility.Visible;
                gb_resultados.Visibility = Visibility.Visible;


                string melhor_tipo = "";
                double melhor = Math.Max(percentual_1bit, Math.Max(percentual_2bits, Math.Max(percentual_adaptativo, percentual_maisfrequente)));

                if (melhor == percentual_1bit)
                    melhor_tipo = "1 bit / ";

                if (melhor == percentual_2bits)
                    melhor_tipo += "2 bits / ";

                if (melhor == percentual_adaptativo)
                    melhor_tipo += "Adaptativo de dois níveis / ";

                if (melhor == percentual_maisfrequente)
                    melhor_tipo += "Memória de saltos / ";

                lb_melhortipo.Content = "Melhores tipos de acordo com a entrada: " + melhor_tipo;
            }
            else
            {
                MessageBox.Show("Insira um valor de entrada!");
            }
        }

        private void bt_limpar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.g_global.Children.Clear();
            MainWindow.g_global.Children.Add(new UC_tela1());
        }
        
        private void rd_1bit_Checked(object sender, RoutedEventArgs e)
        {
            checou_resultado = true;
            //tblock_entrada.Text = entrada;
            tblock_entrada.Text = "";
            String valor = "";

            /*PREDICAO DE 1 BIT*/
            if (rd_true.IsChecked == true)
                valor = "T";
            else
                valor = "N";

            for (int i = 0; i < lista_entrada.Count(); i++)
            {
                if (lista_entrada[i] == valor)
                    tblock_entrada.Text = tblock_entrada.Text + lista_entrada[i] + "☑  ";
                else
                    tblock_entrada.Text = tblock_entrada.Text + lista_entrada[i] + "☒  ";

                valor = lista_entrada[i];
            }
            /*FIM - PREDICAO DE 1 BIT*/
        }

        private void rd_2bits_Checked(object sender, RoutedEventArgs e)
        {
            checou_resultado = true;
            //tblock_entrada.Text = entrada;
            tblock_entrada.Text = "";
            String valor = "";

            /*PREDICAO DE 2 BITS*/
            if (rd_true.IsChecked == true)
                valor = "T";
            else
                valor = "N";

            int errou = 0;

            for (int i = 0; i < lista_entrada.Count(); i++)
            {
                if (lista_entrada[i] == valor)
                {
                    tblock_entrada.Text = tblock_entrada.Text + lista_entrada[i] + "☑  ";
                    errou = 0;
                }
                else
                {
                    errou++;
                    tblock_entrada.Text = tblock_entrada.Text + lista_entrada[i] + "☒  ";
                    if (errou == 2)
                    {
                        valor = lista_entrada[i];
                        errou = 0;
                    }
                }
            }
        }


        private void rd_memoria_Checked(object sender, RoutedEventArgs e)
        {
            checou_resultado = true;
            //tblock_entrada.Text = entrada;
            tblock_entrada.Text = "";

            String valor = "";
            int acertou = 0;

            /*PREDICAO DE ACORDO COM O VALOR MAIS FREQUENTE*/
            if (rd_true.IsChecked == true)
                valor = "T";
            else
                valor = "N";

            acertou = 0;
            List<string> valores_lidos = new List<string>();

            for (int i = 0; i < lista_entrada.Count(); i++)
            {
                if (lista_entrada[i] == valor)
                    tblock_entrada.Text = tblock_entrada.Text + lista_entrada[i] + "☑  ";
                else
                    tblock_entrada.Text = tblock_entrada.Text + lista_entrada[i] + "☒  ";

                valores_lidos.Add(lista_entrada[i]);
                valor = valorMaisFrequente(valores_lidos);
            }
            /*FIM - PREDICAO DE ACORDO COM O VALOR MAIS FREQUENTE*/
        }

        private void rd_adaptativo_Checked(object sender, RoutedEventArgs e)
        {
            checou_resultado = true;
            //tblock_entrada.Text = entrada;
            tblock_entrada.Text = "";

            String valor = "";
            int acertou = 0;
            int errou = 0;

            /*ADAPTATIVA DE DOIS NÍVEIS*/

            if (rd_true.IsChecked == true)
            {
                historico_adp["TT"] = "T";
                historico_adp["TN"] = "T";
                historico_adp["NT"] = "T";
                historico_adp["NN"] = "T";
                valor = "T";
            }
            else
            {
                historico_adp["TT"] = "N";
                historico_adp["TN"] = "N";
                historico_adp["NT"] = "N";
                historico_adp["NN"] = "N";
                valor = "N";
            }

            acertou = 0;

            for (int i = 0; i < lista_entrada.Count(); i++)
            {
                if (i < 2)
                {
                    if (lista_entrada[i] == valor)
                        tblock_entrada.Text = tblock_entrada.Text + lista_entrada[i] + "☑  ";
                    else
                        tblock_entrada.Text = tblock_entrada.Text + lista_entrada[i] + "☒  ";
                }
                else
                {
                    string key = lista_entrada[i - 1] + lista_entrada[i - 2];

                    if (lista_entrada[i] == historico_adp[key])
                        tblock_entrada.Text = tblock_entrada.Text + lista_entrada[i] + "☑  ";
                    else
                        tblock_entrada.Text = tblock_entrada.Text + lista_entrada[i] + "☒  ";

                    historico_adp[key] = lista_entrada[i];
                }
            }
            /*FIM ADAPTATIVA DE DOIS NÍVEIS*/
        }

        private void bt_voltar_Click(object sender, RoutedEventArgs e)
        {
            var w = Application.Current.Windows[MainWindow.cont_window];
            w.Hide();
            MainWindow.cont_window = MainWindow.cont_window + 1;
            MainWindow main = new MainWindow();
            main.ShowDialog();
        }

        private void rd_true_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                g_saida.Visibility = Visibility.Hidden;
                gb_resultados.Visibility = Visibility.Hidden;
                tblock_entrada.Text = entrada;
                bt_calcular.IsEnabled = true;
            }
            catch (Exception ex) { 
            
            }
        }

        private void rd_false_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                g_saida.Visibility = Visibility.Hidden;
                gb_resultados.Visibility = Visibility.Hidden;
                tblock_entrada.Text = entrada;
                bt_calcular.IsEnabled = true;
            }
            catch (Exception ex)
            {

            }
        }

        private void bt_aleatorio_Click(object sender, RoutedEventArgs e)
        {
            bt_calcular.IsEnabled = true;

            lista_entrada = new List<string>();
            tblock_entrada.Text = "";
            Random randNum = new Random();
            int num;

            for (int i = 0; i < 10; i++) {
                num = randNum.Next(2);
                if (num == 0)
                {
                    tblock_entrada.Text = tblock_entrada.Text + " " + "N";
                    lista_entrada.Add("N");
                }
                else
                {
                    tblock_entrada.Text = tblock_entrada.Text + " " + "T";
                    lista_entrada.Add("T");
                }
            }
            String a = "";
            foreach (string b in lista_entrada) { 
                a += " " + b;
            }



            bt_calcular_Click(this, null);
            Clipboard.SetText(a + "/" + Math.Round(percentual_adaptativo * 100, 3) + "/" + Math.Round(percentual_1bit * 100, 3) + "/" + Math.Round(percentual_2bits * 100, 3) + "/" + Math.Round(percentual_maisfrequente * 100, 3));
        }
    }
}