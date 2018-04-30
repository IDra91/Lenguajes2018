using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto21sem2018
{
    class puerta
    {
        public string nombre;
        public int alto;
        public double ancho;
        public string pared_asoc;
        public string color;

        public puerta(string nombre, int alto, double ancho, string pared_asoc, string color)
        {
            this.nombre = nombre;
            this.alto = alto;
            this.ancho = ancho;
            this.pared_asoc = pared_asoc;
            this.color = color; 
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public int Alto
        {
            get { return alto; }
            set { alto = value; }
        }
        public double Ancho
        {
            get { return ancho; }
            set { ancho = value; }
        }
        public string Pared_Asoc
        {
            get { return pared_asoc; }
            set { pared_asoc = value; }
        }
        public string Color
        {
            get { return color; }
            set { color = value; }
        }
    }
}
