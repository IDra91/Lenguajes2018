using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto21sem2018
{
    class ventana
    {
        string nombre;
        string tipo;
        int longitud;
        string pared_asoc;

        public ventana(string nombre, string tipo, int longitud, string pared_asoc)
        {
            this.nombre = nombre;
            this.tipo = tipo;
            this.longitud = longitud;
            this.pared_asoc = pared_asoc; 
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
        public int Longitud
        {
            get { return longitud; }
            set { longitud = value; }
        }

   
        public string Pared_Asoc
        {
            get { return pared_asoc; }
            set { pared_asoc = value; }
        }


    }
}
