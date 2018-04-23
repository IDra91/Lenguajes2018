using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto21sem2018
{
    class errores
    {
        public string lexema;
        public string tipo;

        public errores(string lexema, string tipo)
        {
            this.lexema = lexema;
            this.tipo = tipo;
        }

        public string Lexema
        {
            get { return lexema; }
            set { lexema = value; }
        }

        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }

        }
    }
}
