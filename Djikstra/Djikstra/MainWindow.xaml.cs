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
using Djikstra.model;
using System.IO;

namespace Djikstra
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //tbAusgabe.Text = Einlesen();   
        }

        private void GetShorttestPath()
        {
            List<Knoten> unsortedKnoten = Einlesen();
            Knoten startKnoten = unsortedKnoten.FirstOrDefault(x => x.Bezeichnung.Equals(charastart.Text.ToUpper()));
            Knoten endKnoten = unsortedKnoten.FirstOrDefault(x => x.Bezeichnung.Equals("Z"));
            startKnoten.Gesamtentfernung = 0;

            while (startKnoten != endKnoten)
            {
                foreach (Kante k in startKnoten.Kanten)
                {
                    int gesamt = startKnoten.Gesamtentfernung + k.Abstand;
                    

                    if (!k.ZielKnoten.Besucht && k.ZielKnoten.Gesamtentfernung > gesamt)
                    {
                        k.ZielKnoten.Gesamtentfernung = gesamt;
                        k.ZielKnoten.Vorgaenger = startKnoten;
                    }
                }

                startKnoten.Besucht = true;
                Knoten kuerzester = null;
                foreach (Knoten kn in unsortedKnoten)
                {

                    if (!kn.Besucht && (kuerzester == null || kn.Gesamtentfernung < kuerzester.Gesamtentfernung))
                    {
                        kuerzester = kn;
                    }
                }

                startKnoten = kuerzester;
                libo.Items.Add("-"+startKnoten.Vorgaenger+"->"+ startKnoten + " Distanz: "+ kuerzester.Gesamtentfernung);
            }
            
        }

        private List<Knoten> Einlesen()
        {
            List<Knoten> eingabeList = new List<Knoten>();

            string[] tempArray = File.ReadAllLines("Resourcen\\Knoten.csv");
            foreach (string zeile in tempArray)
            {
                string[] splitStrings = zeile.Split(';');
                if (splitStrings.Length <3)
                {
                    continue;
                }

                string start = splitStrings[0];
                string ziel = splitStrings[1];
               int.TryParse(splitStrings[2], out int laenge);

                Knoten startKnoten = eingabeList.FirstOrDefault(x => x.Bezeichnung.Equals(start));
                if (startKnoten == null)
                {
                    startKnoten = new Knoten(start);
                    eingabeList.Add(startKnoten);
                }
                Knoten zielKnoten = eingabeList.FirstOrDefault(x => x.Bezeichnung.Equals(ziel));
                if (zielKnoten == null)
                {
                    zielKnoten = new Knoten(ziel);
                    eingabeList.Add(zielKnoten);
                }
                startKnoten.Kanten.Add(new Kante(laenge, zielKnoten));
                zielKnoten.Kanten.Add(new Kante(laenge, startKnoten));
            }


            return eingabeList;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            libo.Items.Clear();
            lvKnoten.ItemsSource = Einlesen();
            GetShorttestPath();
        }
    }
}
