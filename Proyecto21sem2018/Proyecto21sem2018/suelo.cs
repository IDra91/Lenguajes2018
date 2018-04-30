using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto21sem2018
{
    class suelo
    {
        public string nombre;
        public string color;

        public suelo(string nombre, string color)
        {
            this.nombre = nombre;
            this.color = color; 
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
    }
}
