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

namespace Bank
{
    /// <summary>
    /// Логика взаимодействия для CalculateWindow.xaml
    /// </summary>
    public partial class CalculateWindow : Window
    {
        double percet1 = 8.85;
        double percet2 = 6.1;
        double percet3 = 6.55;
        public CalculateWindow()
        {
            InitializeComponent();
        }

        private void btnCompare_Click(object sender, RoutedEventArgs e)
        {
            CompareWindow form = new CompareWindow(Convert.ToInt32(tb_sum.Text), percet1, percet2, percet3, Convert.ToInt32(tbl_stab_result.Text), Convert.ToInt32(tbl_opt_result.Text),Convert.ToInt32(tbl_standart_result.Text), Convert.ToInt32(tb_srok.Text));
            form.Show();
            this.Close();
        }

        private void tb_sum_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                int n1 = 0;
                if (tb_sum.Text != "")
                {
                    n1 = Convert.ToInt32(tb_sum.Text);
                }
                int n2 = Convert.ToInt32(e.Text);
                n1 = n1 * 10 + n2;
                if(n1 >= 0 && n1 <= 10000000)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            catch (FormatException)
            {
                e.Handled = true;
            }
        }

        private void tb_srok_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                int n1 = 0;
                if (tb_srok.Text != "")
                {
                    n1 = Convert.ToInt32(tb_srok.Text);
                }
                int n2 = Convert.ToInt32(e.Text);
                n1 = n1 * 10 + n2;
                if (n1 >= 1 && n1 <= 60)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            catch (FormatException)
            {
                e.Handled = true;
            }
        }

        private void tb_popoln_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                int n1 = 0;
                if (tb_popoln.Text != "")
                {
                    n1 = Convert.ToInt32(tb_popoln.Text);
                }
                int n2 = Convert.ToInt32(e.Text);
                n1 = n1 * 10 + n2;
                if (n1 >= 0 && n1 <= 5000000)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            catch (FormatException)
            {
                e.Handled = true;
            }
        }

        private void sl_sum_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            tb_sum.Text = sl_sum.Value.ToString();
        }

        private void tb_sum_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string[] words = tb_sum.Text.Split(new[] { " " }, System.StringSplitOptions.RemoveEmptyEntries);
                string text = String.Join("", words);
                int sum = Convert.ToInt32(text);
                if (sl_sum != null)
                {
                    if (text == "")
                    {
                        sl_sum.Value = 0;
                    }
                    else
                    {
                        sl_sum.Value = sum;
                    }
                }
                calculate_result();
            }
            catch (FormatException)
            {

            }
            
        }

        private void sl_srok_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            tb_srok.Text = sl_srok.Value.ToString();
        }

        private void sl_popoln_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            tb_popoln.Text = sl_popoln.Value.ToString();
        }

        private void tb_srok_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string[] words = tb_srok.Text.Split(new[] { " " }, System.StringSplitOptions.RemoveEmptyEntries);
                string text = String.Join("", words);
                int srok = Convert.ToInt32(text);
                if (sl_srok != null)
                {
                    if (text == "")
                    {
                        sl_srok.Value = 0;
                    }
                    else
                    {
                        sl_srok.Value = srok;
                    }
                }
                calculate_result();
            }
            catch (FormatException)
            {

            }
            
        }

        private void tb_popoln_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string[] words = tb_popoln.Text.Split(new[] { " " }, System.StringSplitOptions.RemoveEmptyEntries);
                string text = String.Join("", words);
                int popoln = Convert.ToInt32(text);
                if (sl_popoln != null)
                {
                    if (text == "")
                    {
                        sl_popoln.Value = 0;
                    }
                    else
                    {
                        sl_popoln.Value = popoln;
                    }
                }
                calculate_result();
            }
            catch (FormatException)
            {

            }
            
        }
        private void calculate_result()
        {
            try
            {
                int sum = Convert.ToInt32(tb_sum.Text);
                int srok = 12;
                if (tb_srok != null)
                {
                    srok = Convert.ToInt32(tb_srok.Text);
                }
                int popoln = 0;
                if(tb_popoln != null)
                {
                    popoln = Convert.ToInt32(tb_popoln.Text);
                }
                int stab_dohod = (int)Math.Round(sum * percet1/100 * srok/12);
                tbl_stab_result.Text = stab_dohod.ToString();
                int optimal_dohod = (int)Math.Round(sum * Math.Pow(1 + percet2/100 / 12, srok) - sum + srok*popoln);
                tbl_opt_result.Text = optimal_dohod.ToString();
                double standart_dohod = Math.Round(sum * percet3/100);
                for (int i = 0; i < srok; i++)
                {
                    standart_dohod = standart_dohod + popoln;
                    standart_dohod = standart_dohod + Math.Round(standart_dohod * percet3/100);
                }
                standart_dohod = (int)standart_dohod*srok/24;
                tbl_standart_result.Text = standart_dohod.ToString();
            }
            catch (FormatException)
            {

            }
            
        }
    }
}
