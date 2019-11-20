using System;
using System.Collections.Generic;
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
using Newtonsoft.Json;
using System.IO;
using Microsoft.Win32;

namespace streamsSamenvatting.Wpf
{
    /// <summary>
    /// Interaction logic for winJson.xaml
    /// </summary>
    public partial class winJson : Window
    {
        public winJson()
        {
            InitializeComponent();
        }

        private void btnPresidenten_Click(object sender, RoutedEventArgs e)
        {
            // https://mysafeinfo.com/api/data?list=presidents&format=jsonp

            var json = new WebClient().DownloadString("https://mysafeinfo.com/api/data?list=presidents&format=json");
            List<President> presidenten = new List<President>();
            presidenten = JsonConvert.DeserializeObject<List<President>>(json);
            dgrPresident.ItemsSource = presidenten;

        
        }


        List<PostNummers> waarden;
        private void btnPostcodes_Click(object sender, RoutedEventArgs e)
        {
        
            var json = new WebClient().DownloadString("https://raw.githubusercontent.com/jief/zipcode-belgium/master/zipcode-belgium.json");
            waarden = new List<PostNummers>();
            waarden = JsonConvert.DeserializeObject<List<PostNummers>>(json);
            dgrPost.ItemsSource = waarden;
        }

        private void btnSavePostnrs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialoog = new SaveFileDialog();
            string map = Environment.CurrentDirectory;
            dialoog.InitialDirectory = map;
            dialoog.FileName = "postnummers.json";
            dialoog.Filter = "json-file (*.json)|*.json";
            if (dialoog.ShowDialog() == true)
            {
                using (StreamWriter file = File.CreateText(dialoog.FileName))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, waarden);
                }
            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FormSettings frms = new FormSettings();
            frms.SetSettings(this);
            List<FormSettings> settings = new List<FormSettings>();
            settings.Add(frms);
            string json = JsonConvert.SerializeObject(settings.ToArray());
            File.WriteAllText(Environment.CurrentDirectory + "\\formsettings.json", json);
        }

        private void winJson1_Loaded(object sender, RoutedEventArgs e)
        {
            string bestand = Environment.CurrentDirectory + "\\formsettings.json";
            if (File.Exists(bestand))
            {
                List<FormSettings> settings = new List<FormSettings>();
                String inhoud = File.ReadAllText(bestand);
                settings = JsonConvert.DeserializeObject<List<FormSettings>>(inhoud);
                settings[0].GetSettings(this);
            }
        }
    }
}
