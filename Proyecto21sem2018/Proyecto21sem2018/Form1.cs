using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Proyecto21sem2018
{
    public partial class Form1 : Form
    {
        string filename = "Manual_Tecnico.pdf";
        string filename2 = "Manual_Usuario.pdf";
        scanner s = new scanner();
        public Form1()
        {
            InitializeComponent();
        }

        private RichTextBox getRichTextBox()
        {
            RichTextBox rt = null;
            TabPage tp = tabControl1.SelectedTab;
            if (tp != null)
            {
                rt = tp.Controls[0] as RichTextBox;

            }

            return rt;
        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var lastIndex = this.tabControl1.TabCount - 1;
            TabPage tp = new TabPage();
            int tc = (tabControl1.TabCount);
            tp.Text = "Nueva Pestaña" + tc.ToString();
            tabControl1.TabPages.Insert(tc, tp);
            RichTextBox rt = new RichTextBox();
            rt.Size = new System.Drawing.Size(665, 192);
            rt.Location = new System.Drawing.Point(0, 0);
            tp.Controls.Add(rt);
            this.tabControl1.SelectedIndex = lastIndex;
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Creado por Manuel Rivera, 201212747");
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream;
            OpenFileDialog of = new OpenFileDialog();
            of.DefaultExt = "*.design";
            of.Filter = "DESIGN Files|*.design";
            if (of.ShowDialog() == System.Windows.Forms.DialogResult.OK && of.FileName.Length > 0)
            {
                if ((myStream = of.OpenFile()) != null)
                {
                    string flname = of.FileName;
                    string filetext = File.ReadAllText(flname);
                    getRichTextBox().Text = filetext;
                }


            }
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            if (sf.ShowDialog() == DialogResult.OK)
            {
                using (Stream s = File.Open(sf.FileName, FileMode.CreateNew))
                using (StreamWriter sw = new StreamWriter(s))
                {
                    sw.Write(getRichTextBox().Text);
                }

            }
        }

        private void compilarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            s.Analizador_Lexico(getRichTextBox().Text);
        }

        private void erroresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            s.generarHTMLErrores();
        }

        private void tokensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            s.generarHTMLTokens();
        }

        private void manualTécnicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(filename);
        }

        private void manualDeUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(filename2);
        }
    }
}
