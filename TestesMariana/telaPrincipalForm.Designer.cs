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
            this.toolbox = new System.Windows.Forms.ToolStrip();
            this.btnInserir = new System.Windows.Forms.ToolStripButton();
            this.btnEditar = new System.Windows.Forms.ToolStripButton();
            this.btnExcluir = new System.Windows.Forms.ToolStripButton();
            this.btnVisualizarDetalhadamente = new System.Windows.Forms.ToolStripButton();
            this.btnDuplicar = new System.Windows.Forms.ToolStripButton();
            this.btnGerarPdf = new System.Windows.Forms.ToolStripButton();
            this.btnGerarPDFGabarito = new System.Windows.Forms.ToolStripButton();
            this.labelTipoCadastro = new System.Windows.Forms.ToolStripLabel();
            this.statusStripRodape = new System.Windows.Forms.StatusStrip();
            this.labelRodape = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelRegistros = new System.Windows.Forms.Panel();
            this.menu.SuspendLayout();
            this.toolbox.SuspendLayout();
            this.statusStripRodape.SuspendLayout();
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
            this.disciplinasMenuItem.Click += new System.EventHandler(this.disciplinasMenuItem_Click);
            // 
            // materiasMenuItem
            // 
            this.materiasMenuItem.Name = "materiasMenuItem";
            this.materiasMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.materiasMenuItem.Size = new System.Drawing.Size(175, 22);
            this.materiasMenuItem.Text = "Matérias";
            this.materiasMenuItem.ToolTipText = "Matérias";
            this.materiasMenuItem.Click += new System.EventHandler(this.materiasMenuItem_Click);
            // 
            // questoesMenuItem
            // 
            this.questoesMenuItem.Name = "questoesMenuItem";
            this.questoesMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.questoesMenuItem.Size = new System.Drawing.Size(175, 22);
            this.questoesMenuItem.Text = "Questões";
            this.questoesMenuItem.ToolTipText = "Questões";
            this.questoesMenuItem.Click += new System.EventHandler(this.questoesMenuItem_Click);
            // 
            // testesEscolaresMenuItem
            // 
            this.testesEscolaresMenuItem.Name = "testesEscolaresMenuItem";
            this.testesEscolaresMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.testesEscolaresMenuItem.Size = new System.Drawing.Size(175, 22);
            this.testesEscolaresMenuItem.Text = "Testes escolares";
            this.testesEscolaresMenuItem.ToolTipText = "Testes escolares";
            this.testesEscolaresMenuItem.Click += new System.EventHandler(this.testesEscolaresMenuItem_Click);
            // 
            // toolbox
            // 
            this.toolbox.Enabled = false;
            this.toolbox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnInserir,
            this.btnEditar,
            this.btnExcluir,
            this.btnVisualizarDetalhadamente,
            this.btnDuplicar,
            this.btnGerarPdf,
            this.btnGerarPDFGabarito,
            this.labelTipoCadastro});
            this.toolbox.Location = new System.Drawing.Point(0, 24);
            this.toolbox.Name = "toolbox";
            this.toolbox.Size = new System.Drawing.Size(800, 41);
            this.toolbox.TabIndex = 1;
            this.toolbox.Text = "toolStrip1";
            // 
            // btnInserir
            // 
            this.btnInserir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnInserir.DoubleClickEnabled = true;
            this.btnInserir.Image = global::TestesMariana.WinApp.Properties.Resources.Insert;
            this.btnInserir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnInserir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnInserir.Name = "btnInserir";
            this.btnInserir.Padding = new System.Windows.Forms.Padding(5);
            this.btnInserir.Size = new System.Drawing.Size(38, 38);
            this.btnInserir.Text = "Inserir";
            this.btnInserir.Click += new System.EventHandler(this.btnInserir_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEditar.DoubleClickEnabled = true;
            this.btnEditar.Image = global::TestesMariana.WinApp.Properties.Resources.Edit;
            this.btnEditar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnEditar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Padding = new System.Windows.Forms.Padding(5);
            this.btnEditar.Size = new System.Drawing.Size(38, 38);
            this.btnEditar.Text = "Editar";
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnExcluir
            // 
            this.btnExcluir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExcluir.DoubleClickEnabled = true;
            this.btnExcluir.Image = global::TestesMariana.WinApp.Properties.Resources.Delete;
            this.btnExcluir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnExcluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Padding = new System.Windows.Forms.Padding(5);
            this.btnExcluir.Size = new System.Drawing.Size(38, 38);
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // btnVisualizarDetalhadamente
            // 
            this.btnVisualizarDetalhadamente.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnVisualizarDetalhadamente.Image = global::TestesMariana.WinApp.Properties.Resources.Visualizar_detalhadamente;
            this.btnVisualizarDetalhadamente.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnVisualizarDetalhadamente.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnVisualizarDetalhadamente.Name = "btnVisualizarDetalhadamente";
            this.btnVisualizarDetalhadamente.Padding = new System.Windows.Forms.Padding(5);
            this.btnVisualizarDetalhadamente.Size = new System.Drawing.Size(38, 38);
            this.btnVisualizarDetalhadamente.Text = "toolStripButton1";
            this.btnVisualizarDetalhadamente.Visible = false;
            this.btnVisualizarDetalhadamente.Click += new System.EventHandler(this.btnVisualizarDetalhadamente_Click);
            // 
            // btnDuplicar
            // 
            this.btnDuplicar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDuplicar.DoubleClickEnabled = true;
            this.btnDuplicar.Image = global::TestesMariana.WinApp.Properties.Resources.Duplicate;
            this.btnDuplicar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDuplicar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDuplicar.Name = "btnDuplicar";
            this.btnDuplicar.Padding = new System.Windows.Forms.Padding(5);
            this.btnDuplicar.Size = new System.Drawing.Size(38, 38);
            this.btnDuplicar.Text = "Duplicar teste";
            this.btnDuplicar.Visible = false;
            this.btnDuplicar.Click += new System.EventHandler(this.btnDuplicar_Click);
            // 
            // btnGerarPdf
            // 
            this.btnGerarPdf.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGerarPdf.DoubleClickEnabled = true;
            this.btnGerarPdf.Image = global::TestesMariana.WinApp.Properties.Resources.PDF;
            this.btnGerarPdf.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnGerarPdf.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGerarPdf.Name = "btnGerarPdf";
            this.btnGerarPdf.Padding = new System.Windows.Forms.Padding(5);
            this.btnGerarPdf.Size = new System.Drawing.Size(38, 38);
            this.btnGerarPdf.Text = "Gerar PDF";
            this.btnGerarPdf.Visible = false;
            this.btnGerarPdf.Click += new System.EventHandler(this.btnGerarPdf_Click);
            // 
            // btnGerarPDFGabarito
            // 
            this.btnGerarPDFGabarito.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGerarPDFGabarito.Image = global::TestesMariana.WinApp.Properties.Resources.Gabarito;
            this.btnGerarPDFGabarito.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnGerarPDFGabarito.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGerarPDFGabarito.Name = "btnGerarPDFGabarito";
            this.btnGerarPDFGabarito.Padding = new System.Windows.Forms.Padding(5);
            this.btnGerarPDFGabarito.Size = new System.Drawing.Size(38, 38);
            this.btnGerarPDFGabarito.Text = "Gerar PDF com gabarito";
            this.btnGerarPDFGabarito.Visible = false;
            this.btnGerarPDFGabarito.Click += new System.EventHandler(this.btnGerarPDFGabarito_Click);
            // 
            // labelTipoCadastro
            // 
            this.labelTipoCadastro.Font = new System.Drawing.Font("Segoe UI", 9F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point);
            this.labelTipoCadastro.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelTipoCadastro.Name = "labelTipoCadastro";
            this.labelTipoCadastro.Size = new System.Drawing.Size(94, 38);
            this.labelTipoCadastro.Text = "toolStripLabel1";
            // 
            // statusStripRodape
            // 
            this.statusStripRodape.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelRodape});
            this.statusStripRodape.Location = new System.Drawing.Point(0, 428);
            this.statusStripRodape.Name = "statusStripRodape";
            this.statusStripRodape.Size = new System.Drawing.Size(800, 22);
            this.statusStripRodape.TabIndex = 2;
            this.statusStripRodape.Text = "statusStrip1";
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
            this.panelRegistros.Location = new System.Drawing.Point(0, 65);
            this.panelRegistros.Name = "panelRegistros";
            this.panelRegistros.Size = new System.Drawing.Size(800, 363);
            this.panelRegistros.TabIndex = 3;
            this.panelRegistros.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.panelRegistros_ControlAdded);
            // 
            // TelaPrincipalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelRegistros);
            this.Controls.Add(this.statusStripRodape);
            this.Controls.Add(this.toolbox);
            this.Controls.Add(this.menu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menu;
            this.Name = "TelaPrincipalForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gerador de Testes Escolares";
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.toolbox.ResumeLayout(false);
            this.toolbox.PerformLayout();
            this.statusStripRodape.ResumeLayout(false);
            this.statusStripRodape.PerformLayout();
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
        private System.Windows.Forms.ToolStrip toolbox;
        private System.Windows.Forms.StatusStrip statusStripRodape;
        private System.Windows.Forms.Panel panelRegistros;
        private System.Windows.Forms.ToolStripStatusLabel labelRodape;
        private System.Windows.Forms.ToolStripButton btnInserir;
        private System.Windows.Forms.ToolStripButton btnEditar;
        private System.Windows.Forms.ToolStripButton btnExcluir;
        private System.Windows.Forms.ToolStripLabel labelTipoCadastro;
        private System.Windows.Forms.ToolStripButton btnGerarPdf;
        private System.Windows.Forms.ToolStripButton btnDuplicar;
        private System.Windows.Forms.ToolStripButton btnGerarPDFGabarito;
        private System.Windows.Forms.ToolStripButton btnVisualizarDetalhadamente;
    }
}
