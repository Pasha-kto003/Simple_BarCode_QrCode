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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Simple_BarCode_QrCode
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void QRCodeClick(object sender, RoutedEventArgs e)
        {
            Zen.Barcode.CodeQrBarcodeDraw codeQrBarcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            var qrimage = codeQrBarcode.Draw(txtQrcode.Text, 50);
            pictureBox.Source = ConvertDrawingImageToWPFImage(qrimage);
            //var uri = new Uri(@"C:\Users\User\source\repos\WpfApp12\WpfApp12\pictureQR.png");
            //var bitmap = new BitmapImage(uri);

            // Save to file.

            //RenderTargetBitmap rtb = new RenderTargetBitmap((int)pictureBox.Source.Width, (int)pictureBox.Source.Height, 96d, 96d, PixelFormats.Default);

            //BmpBitmapEncoder encoder = new BmpBitmapEncoder();
            //encoder.Frames.Add(BitmapFrame.Create(rtb));

            //FileStream fs = File.Open(@"C:\Users\User\source\repos\WpfApp12\WpfApp12\qrcode.jpg", FileMode.Create);
            //encoder.Save(fs);
            //fs.Close();
            int count = 0;
            count++;
            if(count > 10)
            {
                return;
            }
            string filePath = $@"C:\Users\User\source\repos\Simple_BarCode_QrCode\Simple_BarCode_QrCode\qrCode{count}.jpg";
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create((BitmapSource)pictureBox.Source));
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
                encoder.Save(stream); 
        }

        private void BtnBarcodeClick(object sender, RoutedEventArgs e)
        {
            Zen.Barcode.Code128BarcodeDraw barcode128 = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
            var barimage = barcode128.Draw(txtBarcode.Text, 50);
            pictureBox.Source = ConvertDrawingImageToWPFImage(barimage);
        }

        private ImageSource ConvertDrawingImageToWPFImage(System.Drawing.Image gdiImg)
        {
            Image img = new Image();

            //convert System.Drawing.Image to WPF image
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(gdiImg);
            IntPtr hBitmap = bmp.GetHbitmap();
            ImageSource WpfBitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

            img.Source = WpfBitmap;
            img.Width = 403;
            img.Height = 145;
            img.Stretch = Stretch.Fill;
            return WpfBitmap;
        }
    }
}
