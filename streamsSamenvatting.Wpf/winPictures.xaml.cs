using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using streamsSamenvatting.Lib;
namespace streamsSamenvatting.Wpf
{
    /// <summary>
    /// Interaction logic for winPictures.xaml
    /// </summary>
    public partial class winPictures : Window
    {
        public winPictures()
        {
            InitializeComponent();
        }
        private void winAfbeelding_Loaded(object sender, RoutedEventArgs e)
        {
            Configuratie.Uitlezen(this);

        }
        private void winAfbeelding_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Configuratie.Bewaren(this);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }


        private void btnImgFromWeb_Click(object sender, RoutedEventArgs e)
        {
            string url = "http://howestofficemanagement.be/wp-content/uploads/2018/05/14520484_1786977631586891_4708730186046345631_n.png";
            WebClient client = new WebClient();
            Stream stream = client.OpenRead(url);
            PngBitmapDecoder pngdecoder = new PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            BitmapSource bitmapSource = pngdecoder.Frames[0];
            imgHowestPNG.Source = bitmapSource;


        }
    }
}
