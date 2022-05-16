using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TestesMariana.Dominio.ModuloDisciplina;
using TestesMariana.Dominio.ModuloMateria;
using TestesMariana.Dominio.ModuloQuestao;
using TestesMariana.WinApp.Compartilhado;

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

            DesabilitarComponentes();
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
                
                if (questao.Numero != 0)
                {
                    HabilitarComponentes();

                    txtNumero.Text = questao.Numero.ToString();
                    comboBoxDisiciplina.SelectedItem = questao.Disciplina;    
                    comboBoxMateria.SelectedItem = questao.Materia;    
                    richTextBoxEnunciado.Text = questao.Enunciado;
                    AtualizarListboxAlternativas();
                }
            }
        }

        public Func<Questao, ValidationResult> GravarRegistro { get; set; }

        private void btnAdicionarAlternativa_Click(object sender, EventArgs e)
        {
            AdicionarAlternativa();

            AtualizarListboxAlternativas();
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

            AtualizarListboxAlternativas();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            questao.Enunciado = richTextBoxEnunciado.Text;
            questao.Materia = (Materia)comboBoxMateria.SelectedItem;


            var resultadoValidacao = GravarRegistro(questao);

            if (resultadoValidacao.IsValid == false)
            {
                string erro = resultadoValidacao.Errors[0].ErrorMessage;

                TelaPrincipalForm.Instancia.AtualizarRodape(erro, TipoMensagemRodape.ERRO);

                DialogResult = DialogResult.None;
            }
        }

        private void comboBoxMateria_SelectedIndexChanged(object sender, EventArgs e)
        {
            HabilitarComponentes();
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
                TelaPrincipalForm.Instancia.AtualizarRodape(resultado.Errors[0].ErrorMessage, TipoMensagemRodape.ERRO);

            txtAlternativaDescricao.Text = "";
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
                TelaPrincipalForm.Instancia.AtualizarRodape(resultado.Errors[0].ErrorMessage, TipoMensagemRodape.ERRO);
        }

        public void MarcarCorreta(Alternativa alternativa)
        {
            if (checkBoxAlternativaCorreta.Checked && checkBoxAlternativaCorreta.Enabled == true)
                alternativa.Correta = true;
        }

        private void AtualizarListboxAlternativas()
        {
            listAlternativasCadastradas.Items.Clear();

            if (questao.Alternativas != null && questao.Alternativas.Count > 0)
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
            {
                checkBoxAlternativaCorreta.Enabled = true;
                checkBoxAlternativaCorreta.Checked = false;
            }
        }

        private void DesabilitarComponentes()
        {
            richTextBoxEnunciado.Enabled = false;
            txtAlternativaDescricao.Enabled = false;
            txtAlternativaDescricao.Enabled = false;
            listAlternativasCadastradas.Enabled = false;
            checkBoxAlternativaCorreta.Enabled = false;

            btnAdicionarAlternativa.Enabled = false;
            btnGravar.Enabled = false;
            btnLimpar.Enabled = false;
            btnRemoverAlternativa.Enabled = false;

        }

        private void HabilitarComponentes()
        {
            richTextBoxEnunciado.Enabled = true;
            txtAlternativaDescricao.Enabled = true;
            txtAlternativaDescricao.Enabled = true;
            listAlternativasCadastradas.Enabled = true;
            checkBoxAlternativaCorreta.Enabled = true;

            btnAdicionarAlternativa.Enabled = true;
            btnGravar.Enabled = true;
            btnLimpar.Enabled = true;
            btnRemoverAlternativa.Enabled = true;

        }

    }
}
