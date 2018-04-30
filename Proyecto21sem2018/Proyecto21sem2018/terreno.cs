using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto21sem2018
{
    class terreno
    {
        public int ancho;
        public int largo;

        public terreno(int ancho, int largo)
        {
            this.ancho = ancho;
            this.largo = largo; 
        }

        public int Ancho
        {
            get { return ancho; }
            set { ancho = value; }
        }

        public int Largo
        {
            get { return largo; }
            set { largo = value; }
        }
    }
}
