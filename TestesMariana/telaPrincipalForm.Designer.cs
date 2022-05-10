namespace TestesMariana
{
    partial class TelaPrincipalForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menu = new System.Windows.Forms.MenuStrip();
            this.cadastrosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disciplinasMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.materiasMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.questoesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testesEscolaresMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBox = new System.Windows.Forms.ToolStrip();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.labelRodape = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelRegistros = new System.Windows.Forms.Panel();
            this.menu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cadastrosToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(800, 24);
            this.menu.TabIndex = 0;
            this.menu.Text = "menuStrip1";
            // 
            // cadastrosToolStripMenuItem
            // 
            this.cadastrosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.disciplinasMenuItem,
            this.materiasMenuItem,
            this.questoesMenuItem,
            this.testesEscolaresMenuItem});
            this.cadastrosToolStripMenuItem.Name = "cadastrosToolStripMenuItem";
            this.cadastrosToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.cadastrosToolStripMenuItem.Text = "Cadastros";
            // 
            // disciplinasMenuItem
            // 
            this.disciplinasMenuItem.Name = "disciplinasMenuItem";
            this.disciplinasMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.disciplinasMenuItem.Size = new System.Drawing.Size(175, 22);
            this.disciplinasMenuItem.Text = "Disciplinas";
            this.disciplinasMenuItem.ToolTipText = "Disciplinas";
            // 
            // materiasMenuItem
            // 
            this.materiasMenuItem.Name = "materiasMenuItem";
            this.materiasMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.materiasMenuItem.Size = new System.Drawing.Size(175, 22);
            this.materiasMenuItem.Text = "Matérias";
            this.materiasMenuItem.ToolTipText = "Matérias";
            // 
            // questoesMenuItem
            // 
            this.questoesMenuItem.Name = "questoesMenuItem";
            this.questoesMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.questoesMenuItem.Size = new System.Drawing.Size(175, 22);
            this.questoesMenuItem.Text = "Questões";
            this.questoesMenuItem.ToolTipText = "Questões";
            // 
            // testesEscolaresMenuItem
            // 
            this.testesEscolaresMenuItem.Name = "testesEscolaresMenuItem";
            this.testesEscolaresMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.testesEscolaresMenuItem.Size = new System.Drawing.Size(175, 22);
            this.testesEscolaresMenuItem.Text = "Testes escolares";
            this.testesEscolaresMenuItem.ToolTipText = "Testes escolares";
            // 
            // toolBox
            // 
            this.toolBox.Location = new System.Drawing.Point(0, 24);
            this.toolBox.Name = "toolBox";
            this.toolBox.Size = new System.Drawing.Size(800, 25);
            this.toolBox.TabIndex = 1;
            this.toolBox.Text = "toolStrip1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelRodape});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // labelRodape
            // 
            this.labelRodape.Name = "labelRodape";
            this.labelRodape.Size = new System.Drawing.Size(75, 17);
            this.labelRodape.Text = "Label rodapé";
            // 
            // panelRegistros
            // 
            this.panelRegistros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRegistros.Location = new System.Drawing.Point(0, 49);
            this.panelRegistros.Name = "panelRegistros";
            this.panelRegistros.Size = new System.Drawing.Size(800, 379);
            this.panelRegistros.TabIndex = 3;
            // 
            // TelaPrincipalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelRegistros);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolBox);
            this.Controls.Add(this.menu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menu;
            this.Name = "TelaPrincipalForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gerador de Testes Escolares";
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem cadastrosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disciplinasMenuItem;
        private System.Windows.Forms.ToolStripMenuItem materiasMenuItem;
        private System.Windows.Forms.ToolStripMenuItem questoesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testesEscolaresMenuItem;
        private System.Windows.Forms.ToolStrip toolBox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panelRegistros;
        private System.Windows.Forms.ToolStripStatusLabel labelRodape;
    }
}
