using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto21sem2018
{
    class variables
    {
        public string nombre;
        public string tipo;
        public string valor;

        public variables(string nombre, string tipo, string valor)
        {
            this.nombre = nombre;
            this.tipo = tipo;
            this.valor = valor; 
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

        public string Valor
        {
            get { return valor; }
            set { valor = value; }
        }
    }
}
