using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto12018
{
    public partial class Form1 : Form
    {
        scanner s = new scanner();

        public Form1()
        {
            InitializeComponent();
        }

        private void consola_TextChanged(object sender, EventArgs e)
        {

        }

        private void consola_KeyPress(object sender, KeyPressEventArgs e)
        {
           
            int maxlines = consola.Lines.Length;
           
            if((int)e.KeyChar == (int)Keys.Enter){
                 if (maxlines > 0)
            {
                string lastline = consola.Lines[maxlines - 1];
                s.Analizador(consola.Text);
                consola.Text = "";
                consola.Text = "mgr>";
            }
            }
        }
    }
}
