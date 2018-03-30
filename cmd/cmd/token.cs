using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmd
{
    class token
    {
        public string lexema;
        public string tipo;

        public token(string lexema, string tipo)
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
