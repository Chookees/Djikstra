using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Djikstra.model
{
    class Kante
    {
        public int Abstand { get; set; }

        public Knoten ZielKnoten { get; set; } 

        public Kante(int abstand, Knoten zielKnoten)
        {
            Abstand = abstand;
            ZielKnoten = zielKnoten;
        }
    }
}
