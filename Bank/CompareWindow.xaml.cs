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
    /// Логика взаимодействия для CompareWindow.xaml
    /// </summary>
    public partial class CompareWindow : Window
    {
        public CompareWindow(int sum,string stab_stav,string opt_stav,string standart_stav,int stab_dohod, int opt_dohod, int standart_dohod)
        {
            InitializeComponent();
            tbl_stab_dohod.Text = stab_dohod.ToString() + " руб.";
            tbl_opt_dohod.Text = opt_dohod.ToString()+ " руб.";
            tbl_standart_dohod.Text = standart_dohod.ToString() + " руб." ;
            tbl_stab_sum.Text = (sum + stab_dohod).ToString() + " руб.";
            tbl_opt_sum.Text = (sum + opt_dohod).ToString() + " руб." ;
            tbl_standart_sum.Text = (sum + standart_dohod).ToString() + " руб.";
            tbl_stab_stav.Text = stab_stav;
            tbl_opt_stavka.Text = opt_stav;
            tbl_standart_stavka.Text = standart_stav;
        }

        private void btnVklad_Click(object sender, RoutedEventArgs e)
        {
            Autorization form = new Autorization();
            form.Show();
            this.Close();
        }

        private void btnExtract_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
