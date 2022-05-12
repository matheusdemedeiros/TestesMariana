using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TestesMariana.Dominio.ModuloDisciplina;
using TestesMariana.Dominio.ModuloMateria;

namespace TestesMariana.WinApp.ModuloMateria
{
    public partial class TelaCadastroMateriasForm : Form
    {

        private Materia materia;

        private List<Disciplina> disciplinas;

        public TelaCadastroMateriasForm(List<Disciplina> disciplinas)
        {
            InitializeComponent();

            this.disciplinas = disciplinas;

            ManipularComboboxDisciplinas();
        }

        public Materia Materia
        {
            get
            {
                return materia;
            }
            set
            {
                materia = value;
                txtNumero.Text = materia.Numero.ToString();
                txtTituloMateria.Text = materia.Titulo;
                comboBoxDisciplina.SelectedItem = materia.Disciplina;
                comboBoxSerie.SelectedItem = materia.Serie;
            }
        }

        public Func<Materia, ValidationResult> GravarRegistro { get; set; }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();

            TelaPrincipalForm.Instancia.AtualizarRodape("", Color.DarkBlue);
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            materia.Titulo = txtTituloMateria.Text;
            materia.Disciplina =  (Disciplina)comboBoxDisciplina.SelectedItem;
            materia.Serie =  (string)comboBoxSerie.SelectedItem;

            var resultadoValidacao = GravarRegistro(materia);

            if (resultadoValidacao.IsValid == false)
            {
                string erro = resultadoValidacao.Errors[0].ErrorMessage;

                TelaPrincipalForm.Instancia.AtualizarRodape(erro, Color.Red);

                DialogResult = DialogResult.None;
            }
        }

        private void TelaCatastroMateriasForm_Load(object sender, EventArgs e)
        {
            TelaPrincipalForm.Instancia.AtualizarRodape("", Color.DarkBlue);
        }

        private void TelaCatastroMateriasForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            TelaPrincipalForm.Instancia.AtualizarRodape("", Color.DarkBlue);
        }

        private void LimparCampos()
        {
            this.txtTituloMateria.Clear();
            this.comboBoxDisciplina.SelectedItem = null;
            this.comboBoxSerie.SelectedItem = null;
        }

        private void ManipularComboboxDisciplinas()
        {
            if (this.disciplinas.Count > 0)
                PopularComboboxDisciplinas();
            
        }

        private void PopularComboboxDisciplinas()
        {
            foreach (Disciplina disciplina in disciplinas)
                comboBoxDisciplina.Items.Add(disciplina);
        }
    }
}
