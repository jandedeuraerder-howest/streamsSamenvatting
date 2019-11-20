using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace streamsSamenvatting.Lib
{


    public class FormSettings
    {
        public string Naam { get; set; }
        public WindowState Toestand { get; set; }
        public double Bovenkant { get; set; }
        public double Linkerkant { get; set; }
        public double Hoogte { get; set; }
        public double Breedte { get; set; }

        public FormSettings()
        {

        }
        public void SetSettings(Window venster)
        {
            Naam = venster.Name;
            Toestand = venster.WindowState;
            Bovenkant = venster.Top;
            Linkerkant = venster.Left;
            Hoogte = venster.Height;
            Breedte = venster.Width;
        }
        public void GetSettings(Window venster)
        {
            venster.WindowState = Toestand;
            venster.Top = Bovenkant;
            venster.Left = Linkerkant;
            venster.Height = Hoogte;
            venster.Width = Breedte;
        }
    }
}
