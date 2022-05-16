namespace TestesMariana.WinApp.ModuloTeste
{
    partial class TelaCadastroTesteForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTituloTeste = new System.Windows.Forms.TextBox();
            this.comboBoxSerie = new System.Windows.Forms.ComboBox();
            this.comboBoxDisciplinas = new System.Windows.Forms.ComboBox();
            this.comboBoxMaterias = new System.Windows.Forms.ComboBox();
            this.btnGerarTeste = new System.Windows.Forms.Button();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.labelMaxQuestoesEncontradas = new System.Windows.Forms.Label();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.checkBoxTesteDisciplinaInteira = new System.Windows.Forms.CheckBox();
            this.numericUpDownQtdQuestoes = new System.Windows.Forms.NumericUpDown();
            this.btnGravar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQtdQuestoes)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(70, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Título:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(75, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Série:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(49, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Disciplina:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(60, 161);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Matéria:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(153, 221);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(147, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "QTD desejada de questões:";
            // 
            // txtTituloTeste
            // 
            this.txtTituloTeste.Location = new System.Drawing.Point(121, 42);
            this.txtTituloTeste.Name = "txtTituloTeste";
            this.txtTituloTeste.Size = new System.Drawing.Size(247, 23);
            this.txtTituloTeste.TabIndex = 0;
            this.txtTituloTeste.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // comboBoxSerie
            // 
            this.comboBoxSerie.AutoCompleteCustomSource.AddRange(new string[] {
            "1ª Série - EF",
            "2ª Série - EF",
            "3ª Série - EF ",
            "4ª Série - EF",
            "5ª Série - EF",
            "6ª Série - EF",
            "7ª Série - EF",
            "8ª Série - EF",
            "9ª Série - EF",
            "1ª Série - EM",
            "2ª Série - EM",
            "3ª Série - EM"});
            this.comboBoxSerie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSerie.FormattingEnabled = true;
            this.comboBoxSerie.Items.AddRange(new object[] {
            "1ª Série - EF",
            "2ª Série - EF",
            "3ª Série - EF ",
            "4ª Série - EF",
            "5ª Série - EF",
            "6ª Série - EF",
            "7ª Série - EF",
            "8ª Série - EF",
            "9ª Série - EF",
            "1ª Série - EM",
            "2ª Série - EM",
            "3ª Série - EM"});
            this.comboBoxSerie.Location = new System.Drawing.Point(121, 71);
            this.comboBoxSerie.Name = "comboBoxSerie";
            this.comboBoxSerie.Size = new System.Drawing.Size(247, 23);
            this.comboBoxSerie.TabIndex = 1;
            // 
            // comboBoxDisciplinas
            // 
            this.comboBoxDisciplinas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDisciplinas.FormattingEnabled = true;
            this.comboBoxDisciplinas.Location = new System.Drawing.Point(121, 105);
            this.comboBoxDisciplinas.Name = "comboBoxDisciplinas";
            this.comboBoxDisciplinas.Size = new System.Drawing.Size(247, 23);
            this.comboBoxDisciplinas.TabIndex = 2;
            this.comboBoxDisciplinas.SelectedIndexChanged += new System.EventHandler(this.comboBoxDisciplinas_SelectedIndexChanged);
            // 
            // comboBoxMaterias
            // 
            this.comboBoxMaterias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMaterias.FormattingEnabled = true;
            this.comboBoxMaterias.Location = new System.Drawing.Point(121, 161);
            this.comboBoxMaterias.Name = "comboBoxMaterias";
            this.comboBoxMaterias.Size = new System.Drawing.Size(247, 23);
            this.comboBoxMaterias.TabIndex = 4;
            // 
            // btnGerarTeste
            // 
            this.btnGerarTeste.Location = new System.Drawing.Point(35, 221);
            this.btnGerarTeste.Name = "btnGerarTeste";
            this.btnGerarTeste.Size = new System.Drawing.Size(75, 23);
            this.btnGerarTeste.TabIndex = 7;
            this.btnGerarTeste.Text = "Gerar teste";
            this.btnGerarTeste.UseVisualStyleBackColor = true;
            this.btnGerarTeste.Click += new System.EventHandler(this.btnGerarTeste_Click);
            // 
            // btnLimpar
            // 
            this.btnLimpar.Location = new System.Drawing.Point(121, 256);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(75, 23);
            this.btnLimpar.TabIndex = 9;
            this.btnLimpar.Text = "Limpar";
            this.btnLimpar.UseVisualStyleBackColor = true;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(209, 256);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 10;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(56, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 15);
            this.label6.TabIndex = 8;
            this.label6.Text = "Número:";
            // 
            // txtNumero
            // 
            this.txtNumero.Enabled = false;
            this.txtNumero.Location = new System.Drawing.Point(121, 9);
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Size = new System.Drawing.Size(75, 23);
            this.txtNumero.TabIndex = 9;
            this.txtNumero.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(139, 194);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(166, 15);
            this.label7.TabIndex = 10;
            this.label7.Text = "QTD de questões encontradas:";
            // 
            // labelMaxQuestoesEncontradas
            // 
            this.labelMaxQuestoesEncontradas.AutoSize = true;
            this.labelMaxQuestoesEncontradas.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelMaxQuestoesEncontradas.Location = new System.Drawing.Point(328, 189);
            this.labelMaxQuestoesEncontradas.Name = "labelMaxQuestoesEncontradas";
            this.labelMaxQuestoesEncontradas.Size = new System.Drawing.Size(19, 21);
            this.labelMaxQuestoesEncontradas.TabIndex = 11;
            this.labelMaxQuestoesEncontradas.Text = "0";
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.Location = new System.Drawing.Point(35, 189);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(75, 23);
            this.btnFiltrar.TabIndex = 5;
            this.btnFiltrar.Text = "Filtrar";
            this.btnFiltrar.UseVisualStyleBackColor = true;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // checkBoxTesteDisciplinaInteira
            // 
            this.checkBoxTesteDisciplinaInteira.AutoSize = true;
            this.checkBoxTesteDisciplinaInteira.Location = new System.Drawing.Point(127, 137);
            this.checkBoxTesteDisciplinaInteira.Name = "checkBoxTesteDisciplinaInteira";
            this.checkBoxTesteDisciplinaInteira.Size = new System.Drawing.Size(157, 19);
            this.checkBoxTesteDisciplinaInteira.TabIndex = 3;
            this.checkBoxTesteDisciplinaInteira.Text = "Teste da disciplina inteira";
            this.checkBoxTesteDisciplinaInteira.UseVisualStyleBackColor = true;
            this.checkBoxTesteDisciplinaInteira.CheckedChanged += new System.EventHandler(this.checkBoxTesteDisciplinaInteira_CheckedChanged);
            // 
            // numericUpDownQtdQuestoes
            // 
            this.numericUpDownQtdQuestoes.Location = new System.Drawing.Point(306, 219);
            this.numericUpDownQtdQuestoes.Name = "numericUpDownQtdQuestoes";
            this.numericUpDownQtdQuestoes.Size = new System.Drawing.Size(62, 23);
            this.numericUpDownQtdQuestoes.TabIndex = 6;
            this.numericUpDownQtdQuestoes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownQtdQuestoes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnGravar
            // 
            this.btnGravar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnGravar.Location = new System.Drawing.Point(35, 256);
            this.btnGravar.Name = "btnGravar";
            this.btnGravar.Size = new System.Drawing.Size(75, 23);
            this.btnGravar.TabIndex = 8;
            this.btnGravar.Text = "Gravar";
            this.btnGravar.UseVisualStyleBackColor = true;
            this.btnGravar.Click += new System.EventHandler(this.btnGravar_Click);
            // 
            // TelaCadastroTesteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 291);
            this.Controls.Add(this.btnGravar);
            this.Controls.Add(this.numericUpDownQtdQuestoes);
            this.Controls.Add(this.checkBoxTesteDisciplinaInteira);
            this.Controls.Add(this.btnFiltrar);
            this.Controls.Add(this.labelMaxQuestoesEncontradas);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtNumero);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnLimpar);
            this.Controls.Add(this.btnGerarTeste);
            this.Controls.Add(this.comboBoxMaterias);
            this.Controls.Add(this.comboBoxDisciplinas);
            this.Controls.Add(this.comboBoxSerie);
            this.Controls.Add(this.txtTituloTeste);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TelaCadastroTesteForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de Testes";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQtdQuestoes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTituloTeste;
        private System.Windows.Forms.ComboBox comboBoxSerie;
        private System.Windows.Forms.ComboBox comboBoxDisciplinas;
        private System.Windows.Forms.ComboBox comboBoxMaterias;
        private System.Windows.Forms.Button btnGerarTeste;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNumero;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelMaxQuestoesEncontradas;
        private System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.CheckBox checkBoxTesteDisciplinaInteira;
        private System.Windows.Forms.NumericUpDown numericUpDownQtdQuestoes;
        private System.Windows.Forms.Button btnGravar;
    }
}