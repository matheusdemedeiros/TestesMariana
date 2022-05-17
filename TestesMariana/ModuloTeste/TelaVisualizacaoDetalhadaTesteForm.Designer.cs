namespace TestesMariana.WinApp.ModuloTeste
{
    partial class TelaVisualizacaoDetalhadaTesteForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBoxTeste = new System.Windows.Forms.ListBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxTeste
            // 
            this.listBoxTeste.FormattingEnabled = true;
            this.listBoxTeste.ItemHeight = 15;
            this.listBoxTeste.Location = new System.Drawing.Point(12, 12);
            this.listBoxTeste.Name = "listBoxTeste";
            this.listBoxTeste.Size = new System.Drawing.Size(646, 349);
            this.listBoxTeste.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(298, 367);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // TelaVisualizacaoDetalhadaTesteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 402);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.listBoxTeste);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TelaVisualizacaoDetalhadaTesteForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Visualização detalhada de um teste";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxTeste;
        private System.Windows.Forms.Button btnOK;
    }
}