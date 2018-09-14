using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Djikstra.model
{
    class Knoten
    {
        public List<Kante> Kanten { get; }

        public bool Besucht { get; set; }

        public string Bezeichnung { get; set; }

        public int Gesamtentfernung { get; set; }

        public Knoten Vorgaenger { get; set; }
        public Knoten(string bezeichnung)
        {
            Gesamtentfernung = int.MaxValue;
            Bezeichnung = bezeichnung;
            Kanten = new List<Kante>();
            Besucht = false;
        }

        public override string ToString()
        {
            return Bezeichnung;
        }
    }
}
