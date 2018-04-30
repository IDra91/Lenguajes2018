using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto21sem2018
{
    class pared
    {
        public string nombre;
        public string color;
        public int alto;
        public int inicioX;
        public int inicioY;
        public int finX;
        public int finY;

        public pared(string nombre, string color, int alto, int inicioX, int inicioY, int finX, int finY)
        {
            this.nombre = nombre;
            this.color = color;
            this.alto = alto;
            this.inicioX = inicioX;
            this.inicioY = inicioY;
            this.finX = finX;
            this.finY = finY; 
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public string Color
        {
            get { return color; }
            set { color = value; }
        }

        public int Alto
        {
            get { return alto; }
            set { alto = value; }
        }

        public int InicioX
        {
            get { return inicioX; }
            set { inicioX = value; }
        }

        public int InicioY
        {
            get { return inicioY; }
            set { inicioY = value; }
        }

        public int FinX
        {
            get { return finX; }
            set { finX = value; }
        }

        public int FinY
        {
            get { return finY; }
            set { finY = value; }
        }
    }
}
