namespace Proyecto12018
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.consola = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // consola
            // 
            this.consola.BackColor = System.Drawing.SystemColors.MenuText;
            this.consola.ForeColor = System.Drawing.Color.Lime;
            this.consola.Location = new System.Drawing.Point(-3, 1);
            this.consola.Name = "consola";
            this.consola.Size = new System.Drawing.Size(492, 239);
            this.consola.TabIndex = 0;
            this.consola.Text = "mgr> ";
            this.consola.TextChanged += new System.EventHandler(this.consola_TextChanged);
            this.consola.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.consola_KeyPress);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 239);
            this.Controls.Add(this.consola);
            this.Name = "Form1";
            this.Text = "Consola";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox consola;
    }
}

