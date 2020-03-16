using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace App_Cancella
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        CancellationTokenSource ct1 = new CancellationTokenSource();
        CancellationTokenSource ct2 = new CancellationTokenSource();
        CancellationTokenSource ct3 = new CancellationTokenSource();

        private void Btn_Start1_Click(object sender, RoutedEventArgs e)
        {
            if (ct1.Token.IsCancellationRequested)
                ct1 = new CancellationTokenSource();

            Task.Factory.StartNew(()=>Dowork(100,1000,lbl_Count1, ct1));
        }

        private void Btn_Start2_Click(object sender, RoutedEventArgs e)
        {
            if (ct2.Token.IsCancellationRequested)
                ct2 = new CancellationTokenSource();

            int max = Convert.ToInt32(txt_Max1.Text);
            Task.Factory.StartNew(() => Dowork(max,1000,lbl_Count2, ct2));
        }

        private void Btn_Start3_Click(object sender, RoutedEventArgs e)
        {
            if (ct3.Token.IsCancellationRequested)
                ct3 = new CancellationTokenSource();

            int max = Convert.ToInt32(txt_Max2.Text);
            int delay = Convert.ToInt32(txt_Delay.Text);
            Task.Factory.StartNew(() => Dowork(max,delay,lbl_Count3, ct3));
        }


        private void Dowork(int max, int delay, Label lbl, CancellationTokenSource ct)
        {
            for (int i = 0; i < 101; i++)
            {
                if (ct.Token.IsCancellationRequested)
                    break;

                Dispatcher.Invoke(() => UpdateUI(i,lbl));
                Thread.Sleep(delay);
            }
        }

        private void UpdateUI(int i, Label lbl)
        {
            lbl.Content = i.ToString();
        }

        private void btn_Stop_Click(object sender, RoutedEventArgs e)
        {
            ct1.Cancel();
            ct2.Cancel();
            ct3.Cancel();
        }

        private void btn_Stop1_Click(object sender, RoutedEventArgs e)
        {
            ct1.Cancel();
        }

        private void btn_Stop2_Click(object sender, RoutedEventArgs e)
        {
            ct2.Cancel();
        }

        private void btn_Stop3_Click(object sender, RoutedEventArgs e)
        {
            ct3.Cancel();
        }

    }
}
