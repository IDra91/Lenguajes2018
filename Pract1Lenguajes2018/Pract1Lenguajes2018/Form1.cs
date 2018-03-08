using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pract1Lenguajes2018
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void nuevaPestañaToolStripMenuItem_Click(object sender, EventArgs e)
        {

            tabControl1.TabPages.Add("Nueva Pestaña");
            

            
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Creado por Manuel Rivera, 201212747");
        }
    }
}
