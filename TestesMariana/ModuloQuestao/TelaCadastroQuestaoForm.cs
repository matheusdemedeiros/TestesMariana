using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TestesMariana.Dominio.ModuloDisciplina;
using TestesMariana.Dominio.ModuloMateria;
using TestesMariana.Dominio.ModuloQuestao;

namespace TestesMariana.WinApp.ModuloQuestao
{
    public partial class TelaCadastroQuestaoForm : Form
    {
        private Questao questao;
        private List<Disciplina> disciplinas;
        private List<Materia> materias;

        public TelaCadastroQuestaoForm(List<Disciplina> disciplinas, List<Materia> materias)
        {
            InitializeComponent();
            this.disciplinas = disciplinas;
            this.materias = materias;

            ManipularComboboxDisciplinas();
        }

        public Questao Questao
        {
            get
            {
                return questao;
            }
            set
            {
                questao = value;
                questao.Enunciado = richTextBoxEnunciado.Text;
                questao.Materia = (Materia)comboBoxMateria.SelectedItem;

            }
        }

        public Func<Questao, ValidationResult> GravarRegistro { get; set; }

        private void btnAdicionarAlternativa_Click(object sender, EventArgs e)
        {
            AdicionarAlternativa();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void comboBoxDisiciplina_SelectedIndexChanged(object sender, EventArgs e)
        {
            ManipularComboboxMaterias();
        }

        private void btnRemoverAlternativa_Click(object sender, EventArgs e)
        {
            RemoverAlternativa();
        }

        private void LimparCampos()
        {
            richTextBoxEnunciado.Text = "";
            txtAlternativaDescricao.Text = "";
        }

        private void ManipularComboboxDisciplinas()
        {
            if (this.disciplinas.Count > 0)
                PopularComboboxDisciplinas();
        }

        private void PopularComboboxDisciplinas()
        {
            foreach (Disciplina disciplina in disciplinas)
                comboBoxDisiciplina.Items.Add(disciplina);
        }

        private void ManipularComboboxMaterias()
        {
            Disciplina disciplinaSelecionada = ObterDisciplinaSelecionada();

            List<Materia> materiasFiltradasPorDisciplina = new List<Materia>();

            if (disciplinaSelecionada != null)
            {
                materiasFiltradasPorDisciplina = FiltrarMateriasPorDisciplina(x => x.Disciplina == disciplinaSelecionada);
                PopularComboboxMaterias(materiasFiltradasPorDisciplina);
            }
        }

        private void PopularComboboxMaterias(List<Materia> materias)
        {
            comboBoxMateria.Items.Clear();

            foreach (Materia materia in materias)
                comboBoxMateria.Items.Add(materia);
        }

        private List<Materia> FiltrarMateriasPorDisciplina(Predicate<Materia> condicao)
        {
            List<Materia> materiasFiltradasPorDisciplina = new List<Materia>();

            foreach (var materia in materias)
                if (condicao(materia))
                    materiasFiltradasPorDisciplina.Add(materia);

            return materiasFiltradasPorDisciplina;
        }

        private Disciplina ObterDisciplinaSelecionada()
        {
            Disciplina disciplinaSelecionada = null;

            if (comboBoxDisiciplina.SelectedItem != null)
                disciplinaSelecionada = (Disciplina)comboBoxDisiciplina.SelectedItem;

            return disciplinaSelecionada;
        }

        private void AdicionarAlternativa()
        {
            Alternativa alternativa = new Alternativa();

            alternativa.Descricao = txtAlternativaDescricao.Text;

            MarcarCorreta(alternativa);

            var resultado = questao.AdicionarAlternativa(alternativa);

            if (resultado.IsValid == false)
                TelaPrincipalForm.Instancia.AtualizarRodape(resultado.Errors[0].ErrorMessage, Color.Red);

            AtualizarListboxAlternativas();

        }

        private void RemoverAlternativa()
        {
            var alternativaSelecionada = ObterAlternativaSelecionada();

            if (alternativaSelecionada == null)
            {
                MessageBox.Show("Selecione uma alternativa primeiro!", "Exclusão de alternativas",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var resultado = questao.ExcluirAlternativa(alternativaSelecionada);

            if (resultado.IsValid)
            {
                MessageBox.Show("Alternativa removida com sucesso!!", "Exclusão de alternativas",
                   MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                TelaPrincipalForm.Instancia.AtualizarRodape(resultado.Errors[0].ErrorMessage, Color.Red);
        }

        public void MarcarCorreta(Alternativa alternativa)
        {
            if (checkBoxAlternativaCorreta.Checked)
                alternativa.Correta = true;
        }

        private void AtualizarListboxAlternativas()
        {
            listAlternativasCadastradas.Items.Clear();

            if (questao.Alternativas.Count > 0)
                foreach (var alternativa in questao.Alternativas)
                    listAlternativasCadastradas.Items.Add(alternativa);

            AtualizarCheckboxAlternativaCorreta();
        }

        private Alternativa ObterAlternativaSelecionada()
        {
            return (Alternativa)listAlternativasCadastradas.SelectedItem;
        }

        private void AtualizarCheckboxAlternativaCorreta()
        {
            if (questao.TemAlternativaCorretaCadastrada)
                checkBoxAlternativaCorreta.Enabled = false;
            else
                checkBoxAlternativaCorreta.Enabled = true;
        }


    }
}
