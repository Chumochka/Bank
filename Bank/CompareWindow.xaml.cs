using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
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
        int amount = 1000;
        int period = 1;
        double percet1 = 8.85;
        double percet2 = 6.1;
        double percet3 = 6.55;
        public CompareWindow(int sum,double stab_stav,double opt_stav,double standart_stav,int stab_dohod, int opt_dohod, int standart_dohod, int srok)
        {
            InitializeComponent();
            amount = sum;
            period = srok;
            tbl_stab_dohod.Text = stab_dohod.ToString() + " руб.";
            tbl_opt_dohod.Text = opt_dohod.ToString()+ " руб.";
            tbl_standart_dohod.Text = standart_dohod.ToString() + " руб." ;
            tbl_stab_sum.Text = (sum + stab_dohod).ToString() + " руб.";
            tbl_opt_sum.Text = (sum + opt_dohod).ToString() + " руб." ;
            tbl_standart_sum.Text = (sum + standart_dohod).ToString() + " руб.";
            percet1 = stab_stav;
            percet2 = opt_stav;
            percet3 = standart_stav;
            tbl_stab_stav.Text = percet1.ToString() + " %";
            tbl_opt_stavka.Text = percet2.ToString() + " %";
            tbl_standart_stavka.Text = percet3.ToString() + " %";
        }

        private void btnVklad_Click(object sender, RoutedEventArgs e)
        {
            Autorization form = new Autorization(amount, period, percet1);
            form.Show();
            this.Close();
        }

        private void btnVklad3_Click(object sender, RoutedEventArgs e)
        {
            Autorization form = new Autorization(amount, period, percet3);
            form.Show();
            this.Close();
        }

        private void btnVklad2_Click(object sender, RoutedEventArgs e)
        {
            Autorization form = new Autorization(amount, period, percet2);
            form.Show();
            this.Close();
        }
        private void btnExtract_Click(object sender, RoutedEventArgs e)
        {
            UIElement element = gb_screen as UIElement;
            Uri path = new Uri(@"C:\Users\Miller\source\repos\Bank\Bank\Images\screenshot.png");
            CaptureScreen(element, path);
        }
        public void CaptureScreen(UIElement source, Uri destination)
        {
            try
            {
                double Height, renderHeight, Width, renderWidth;

                Height = renderHeight = source.RenderSize.Height;
                Width = renderWidth = source.RenderSize.Width;

                RenderTargetBitmap renderTarget = new RenderTargetBitmap((int)renderWidth, (int)renderHeight, 96, 96, PixelFormats.Pbgra32);
                VisualBrush visualBrush = new VisualBrush(source);
                DrawingVisual drawingVisual = new DrawingVisual();
                using (DrawingContext drawingContext = drawingVisual.RenderOpen())
                {
                    drawingContext.DrawRectangle(visualBrush, null, new Rect(new Point(30,60),new Point(Width-30,Height-60)));
                }
                renderTarget.Render(drawingVisual);
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(renderTarget));
                using (FileStream stream = new FileStream(destination.LocalPath, FileMode.Create, FileAccess.Write))
                {
                    encoder.Save(stream);
                }
                //Create a new PDF document
                PdfDocument doc = new PdfDocument();
                //Add a page to the document
                PdfPage page = doc.Pages.Add();
                //Create PDF ghaphics for the page
                PdfGraphics graphics = page.Graphics;
                //Load the image from the disk
                PdfBitmap image = new PdfBitmap(@"C:\Users\Miller\source\repos\Bank\Bank\Images\screenshot.png");
                //Draw the image
                graphics.DrawImage(image,0,0);
                //Save the document
                doc.Save(@"C:\Users\Miller\source\repos\Bank\Bank\Images\Выписка.pdf");
                //Close the document
                doc.Close(true);
            }catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}
