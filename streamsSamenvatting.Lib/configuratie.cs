using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;


namespace streamsSamenvatting.Lib
{
    public class Configuratie
    {
        // niet vergeten : referentie maken naar PresentationFramework

        public static void Uitlezen(Window venster)
        {
            string naam = venster.Name;
            string pad = Environment.CurrentDirectory + "\\configuratie.ini";
            string lijn;
            if (File.Exists(pad))
            {
                bool bestandherschrijven = false;
                StringBuilder backup = new StringBuilder();
                StreamReader sr = new StreamReader(pad);
                while ((lijn = sr.ReadLine()) != null)
                {
                    try
                    {
                        string[] hoofddelen = lijn.Split(':');
                        if (hoofddelen[0] == naam)
                        {
                            string[] gegevens = hoofddelen[1].Split('|');
                            venster.WindowState = (WindowState)int.Parse(gegevens[0]);
                            venster.Top = double.Parse(gegevens[1]);
                            venster.Left = double.Parse(gegevens[2]);
                            venster.Height = double.Parse(gegevens[3]);
                            venster.Width = double.Parse(gegevens[4]);
                            backup.Append(lijn + Environment.NewLine);
                        }
                    }
                    catch
                    {
                        // er is iets fout met de ingelezen lijn
                        bestandherschrijven = true;
                    }
                }
                sr.Close();
                if(bestandherschrijven)
                {
                    StreamWriter sw = new StreamWriter(pad);
                    sw.Write(backup.ToString());
                    sw.Flush();
                    sw.Close();

                }
            }

        }
        public static void Bewaren(Window venster)
        {
            // we ontvangen het volledige venster als argument.
            string naam = venster.Name;
            WindowState toestand = venster.WindowState;
            double bovenkant = venster.Top;
            double linkerkant = venster.Left;
            double hoogte = venster.Height;
            double breedte = venster.Width;

            // we zullen deze info bewaren in het tekstbestand configuratie.ini dat we
            // bewaren in dezelfde map waar onze EXE komt te staan
            string pad = Environment.CurrentDirectory + "\\configuratie.ini";

            // omdat we heel wat tekst op moeten slaan gebruiken we een stringbuilder
            // in plaats van steeds strings te gaan concatineren.  Dit is een stuk performanter
            // in nieuwelijn bewaren we alle gegevens van ons venster op 1 lijn volgens de 
            // gemaakte afspraken
            StringBuilder nieuwelijn = new StringBuilder();
            nieuwelijn.Append(naam);
            nieuwelijn.Append(":");
            nieuwelijn.Append((int)toestand);
            nieuwelijn.Append("|");
            nieuwelijn.Append(bovenkant);
            nieuwelijn.Append("|");
            nieuwelijn.Append(linkerkant);
            nieuwelijn.Append("|");
            nieuwelijn.Append(hoogte);
            nieuwelijn.Append("|");
            nieuwelijn.Append(breedte);

            // als het bestand nog niet bestaat, dan volstaat het om het bestand aan te maken
            // en deze ene lijn in dat bestand te bewaren
            if (!System.IO.File.Exists(pad))
            {
                // we maken een streamwriter aan.  Als enige parameter geven we het pad van
                // onze tekstfile op.  Had het bestand wel bestaan, dan zou het nu overschreven
                // worden
                StreamWriter sw = new StreamWriter(pad);
                // de write-methode schrijft in 1 beweging alle tekst 
                // in tegenstelling tot de writeline (zie verder)
                // omdat we straks lijn per lijn gaan uitlezen dienen we er voor te zorgen
                // dat elke waarde op een nieuwe regel komt te staan
                sw.Write(nieuwelijn.ToString() + Environment.NewLine);
                // niet vergeten de streamwriter af te sluiten
                sw.Close();

                // aleternatief
                // System.IO.File.WriteAllText(pad, sb.ToString());
            }
            else
            {
                // het bestand bestaat wel
                // we moeten onderscheid maken tussen een nieuwe lijn toevoegen 
                // wanneer het de eerste keer is dat we het venster bewaren
                // of een bestaande lijn vervangen
                // we gaan dus lijn per lijn uitlezen en tijdelijk bijhouden in de
                // stringbuilder volledigBestand
                StringBuilder volledigBestand = new StringBuilder();
                string bestaandelijn;
                bool lijngevonden = false;
                // de streamreader wordt geopend
                StreamReader sr = new StreamReader(pad);
                // we lezen lijn per lijn tot het einde
                while ((bestaandelijn = sr.ReadLine()) != null)
                {
                    if (bestaandelijn != "")
                    {
                        // we splitten onze lijn
                        string[] hoofddelen = bestaandelijn.Split(':');
                        if (hoofddelen[0] == naam)
                        {
                            // we merken dat er al een lijn bestaat voor dit venster
                            lijngevonden = true;
                            volledigBestand.Append(nieuwelijn.ToString() + Environment.NewLine);
                        }
                        else
                            volledigBestand.Append(bestaandelijn + Environment.NewLine);
                    }

                }
                // niet vergeten, streamreader sluiten
                sr.Close();
                if (!lijngevonden)
                {
                    // als het venster niet werd gevonden, dan kunnen we onze nieuwe
                    // lijn achteraan het bestand toevoegen.
                    // we doen dit door aan onze streamwriter als tweede parameter de waarde
                    // true mee te geven.  Hierdoor wordt toegevoegd, niet overschreven
                    StreamWriter sw = new StreamWriter(pad, true);
                    sw.WriteLine(nieuwelijn.ToString() + Environment.NewLine);
                    sw.Close();
                }
                else
                {
                    // werd het venster wel gevonden, dan overschrijven we gewoon het 
                    // volledige bestand met de inhoud van de stringbuilder die we gebruikten
                    // tijdens het overlopen van het bestand
                    StreamWriter sw = new StreamWriter(pad);
                    sw.Write(volledigBestand.ToString());
                    sw.Flush();
                    sw.Close();

                }

            }

        }
    }
}
