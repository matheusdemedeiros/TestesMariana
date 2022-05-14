namespace TestesMariana.WinApp.ModuloQuestao
{
    partial class TelaCadastroQuestaoForm
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
            this.label7 = new System.Windows.Forms.Label();
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.comboBoxDisiciplina = new System.Windows.Forms.ComboBox();
            this.comboBoxMateria = new System.Windows.Forms.ComboBox();
            this.richTextBoxEnunciado = new System.Windows.Forms.RichTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtAlternativaDescricao = new System.Windows.Forms.TextBox();
            this.listAlternativasCadastradas = new System.Windows.Forms.ListBox();
            this.checkBoxAlternativaCorreta = new System.Windows.Forms.CheckBox();
            this.btnAdicionarAlternativa = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnGravar = new System.Windows.Forms.Button();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnRemoverAlternativa = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Número:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(197, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Disciplina:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(519, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Matéria:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(171, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Digite o enunciado da questão:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(33, 125);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 15);
            this.label7.TabIndex = 6;
            // 
            // txtNumero
            // 
            this.txtNumero.Location = new System.Drawing.Point(80, 18);
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Size = new System.Drawing.Size(75, 23);
            this.txtNumero.TabIndex = 7;
            this.txtNumero.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // comboBoxDisiciplina
            // 
            this.comboBoxDisiciplina.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDisiciplina.FormattingEnabled = true;
            this.comboBoxDisiciplina.Location = new System.Drawing.Point(264, 18);
            this.comboBoxDisiciplina.Name = "comboBoxDisiciplina";
            this.comboBoxDisiciplina.Size = new System.Drawing.Size(213, 23);
            this.comboBoxDisiciplina.TabIndex = 9;
            this.comboBoxDisiciplina.SelectedIndexChanged += new System.EventHandler(this.comboBoxDisiciplina_SelectedIndexChanged);
            // 
            // comboBoxMateria
            // 
            this.comboBoxMateria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMateria.FormattingEnabled = true;
            this.comboBoxMateria.Location = new System.Drawing.Point(575, 18);
            this.comboBoxMateria.Name = "comboBoxMateria";
            this.comboBoxMateria.Size = new System.Drawing.Size(213, 23);
            this.comboBoxMateria.TabIndex = 10;
            // 
            // richTextBoxEnunciado
            // 
            this.richTextBoxEnunciado.Location = new System.Drawing.Point(20, 85);
            this.richTextBoxEnunciado.Name = "richTextBoxEnunciado";
            this.richTextBoxEnunciado.Size = new System.Drawing.Size(413, 116);
            this.richTextBoxEnunciado.TabIndex = 11;
            this.richTextBoxEnunciado.Text = "";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 213);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(119, 15);
            this.label8.TabIndex = 12;
            this.label8.Text = "Informe a alternativa:";
            // 
            // txtAlternativaDescricao
            // 
            this.txtAlternativaDescricao.Location = new System.Drawing.Point(20, 231);
            this.txtAlternativaDescricao.Name = "txtAlternativaDescricao";
            this.txtAlternativaDescricao.Size = new System.Drawing.Size(413, 23);
            this.txtAlternativaDescricao.TabIndex = 13;
            this.txtAlternativaDescricao.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // listAlternativasCadastradas
            // 
            this.listAlternativasCadastradas.FormattingEnabled = true;
            this.listAlternativasCadastradas.ItemHeight = 15;
            this.listAlternativasCadastradas.Location = new System.Drawing.Point(439, 85);
            this.listAlternativasCadastradas.Name = "listAlternativasCadastradas";
            this.listAlternativasCadastradas.Size = new System.Drawing.Size(349, 169);
            this.listAlternativasCadastradas.TabIndex = 14;
            // 
            // checkBoxAlternativaCorreta
            // 
            this.checkBoxAlternativaCorreta.AutoSize = true;
            this.checkBoxAlternativaCorreta.Location = new System.Drawing.Point(123, 263);
            this.checkBoxAlternativaCorreta.Name = "checkBoxAlternativaCorreta";
            this.checkBoxAlternativaCorreta.Size = new System.Drawing.Size(123, 19);
            this.checkBoxAlternativaCorreta.TabIndex = 17;
            this.checkBoxAlternativaCorreta.Text = "Alternativa correta";
            this.checkBoxAlternativaCorreta.UseVisualStyleBackColor = true;
            // 
            // btnAdicionarAlternativa
            // 
            this.btnAdicionarAlternativa.Location = new System.Drawing.Point(20, 263);
            this.btnAdicionarAlternativa.Name = "btnAdicionarAlternativa";
            this.btnAdicionarAlternativa.Size = new System.Drawing.Size(75, 23);
            this.btnAdicionarAlternativa.TabIndex = 18;
            this.btnAdicionarAlternativa.Text = "Adicionar";
            this.btnAdicionarAlternativa.UseVisualStyleBackColor = true;
            this.btnAdicionarAlternativa.Click += new System.EventHandler(this.btnAdicionarAlternativa_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(442, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(135, 15);
            this.label6.TabIndex = 19;
            this.label6.Text = "Alternativas adicionadas";
            // 
            // btnGravar
            // 
            this.btnGravar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnGravar.Location = new System.Drawing.Point(256, 263);
            this.btnGravar.Name = "btnGravar";
            this.btnGravar.Size = new System.Drawing.Size(75, 23);
            this.btnGravar.TabIndex = 20;
            this.btnGravar.Text = "Gravar";
            this.btnGravar.UseVisualStyleBackColor = true;
            // 
            // btnLimpar
            // 
            this.btnLimpar.Location = new System.Drawing.Point(338, 263);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(75, 23);
            this.btnLimpar.TabIndex = 21;
            this.btnLimpar.Text = "Limpar";
            this.btnLimpar.UseVisualStyleBackColor = true;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(420, 263);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 22;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnRemoverAlternativa
            // 
            this.btnRemoverAlternativa.Location = new System.Drawing.Point(502, 263);
            this.btnRemoverAlternativa.Name = "btnRemoverAlternativa";
            this.btnRemoverAlternativa.Size = new System.Drawing.Size(142, 23);
            this.btnRemoverAlternativa.TabIndex = 23;
            this.btnRemoverAlternativa.Text = "Remover alternativa";
            this.btnRemoverAlternativa.UseVisualStyleBackColor = true;
            this.btnRemoverAlternativa.Click += new System.EventHandler(this.btnRemoverAlternativa_Click);
            // 
            // TelaCadastroQuestaoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 304);
            this.Controls.Add(this.btnRemoverAlternativa);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnLimpar);
            this.Controls.Add(this.btnGravar);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnAdicionarAlternativa);
            this.Controls.Add(this.checkBoxAlternativaCorreta);
            this.Controls.Add(this.listAlternativasCadastradas);
            this.Controls.Add(this.txtAlternativaDescricao);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.richTextBoxEnunciado);
            this.Controls.Add(this.comboBoxMateria);
            this.Controls.Add(this.comboBoxDisiciplina);
            this.Controls.Add(this.txtNumero);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TelaCadastroQuestaoForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de Questões";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNumero;
        private System.Windows.Forms.ComboBox comboBoxDisiciplina;
        private System.Windows.Forms.ComboBox comboBoxMateria;
        private System.Windows.Forms.RichTextBox richTextBoxEnunciado;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtAlternativaDescricao;
        private System.Windows.Forms.ListBox listAlternativasCadastradas;
        private System.Windows.Forms.CheckBox checkBoxAlternativaCorreta;
        private System.Windows.Forms.Button btnAdicionarAlternativa;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnGravar;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnRemoverAlternativa;
    }
}