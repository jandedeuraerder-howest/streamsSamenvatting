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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using streamsSamenvatting.Lib;


namespace streamsSamenvatting.Wpf
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

        private void btnGekendeMappen_Click(object sender, RoutedEventArgs e)
        {
            lstTest.Items.Clear();
            lstTest.Items.Add("Applicatiemap (1) = " + Environment.CurrentDirectory);
            lstTest.Items.Add("Applicatiemap (2) = " + System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location));
            lstTest.Items.Add("Mijn documenten = " + Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            lstTest.Items.Add("Mijn bureaublad = " + Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            lstTest.Items.Add("Startmenu = " + Environment.GetFolderPath(Environment.SpecialFolder.StartMenu));

           
        }

        private void btnSubmappenVanWindows_Click(object sender, RoutedEventArgs e)
        {
            lstTest.Items.Clear();

            DirectoryInfo dirInfo = new DirectoryInfo(@"c:\windows");
            foreach(DirectoryInfo dir in dirInfo.GetDirectories())
            {
                lstTest.Items.Add(dir.Name);
            }


        }
        private void btnNieuweMap_Click(object sender, RoutedEventArgs e)
        {
            string map = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (!Directory.Exists(map + "\\nieuwemap"))
                Directory.CreateDirectory(map + "\\nieuwemap");

        }
        private void btnBestandenVanWindows_Click(object sender, RoutedEventArgs e)
        {
            lstTest.Items.Clear();

            DirectoryInfo dirInfo = new DirectoryInfo(@"c:\windows");
            foreach (FileInfo fi in dirInfo.GetFiles())
            {
                lstTest.Items.Add(fi.Name + " " + fi.Length);
            }

        }

        private void btnNieuwBestand_Click(object sender, RoutedEventArgs e)
        {
            string map = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\nieuwemap";
            if (!Directory.Exists(map))
                Directory.CreateDirectory(map);
            if (!File.Exists(map + "\\test.txt"))
                File.Create(map + "\\test.txt");

        }

        private void btnOpenBestand_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog dialoog = new OpenFileDialog();
            dialoog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\nieuwemap";
            dialoog.Filter = "Tekstestanden (*.txt)|*.txt|Alle bestanden (*.*)|*.*";
            if (dialoog.ShowDialog() == true)
            {
                string pad = dialoog.FileName;
                txtTest.Text = File.ReadAllText(pad);
            }

        }

        private void btnBewaarBestand_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialoog = new SaveFileDialog();
            string map = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            map += "\\nieuwemap";
            dialoog.FileName = map + "\\test.txt";
            dialoog.Filter = "Tekstbestand (*.txt)|*.txt|C# bestand (*.cs)|*.cs";
            if (dialoog.ShowDialog() == true)
            {
                map = dialoog.FileName;
                File.WriteAllText(map, txtTest.Text);
            }
        }
        private void winMain_Loaded(object sender, RoutedEventArgs e)
        {
            Configuratie.Uitlezen(this);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Configuratie.Bewaren(this);

        }

        private void btnNaarFromPic_Click(object sender, RoutedEventArgs e)
        {
            winPictures venster = new winPictures();
            venster.ShowDialog();
        }
    }
}
