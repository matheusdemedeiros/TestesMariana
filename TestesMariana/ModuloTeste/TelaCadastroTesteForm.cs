using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TestesMariana.Dominio.ModuloDisciplina;
using TestesMariana.Dominio.ModuloMateria;
using TestesMariana.Dominio.ModuloQuestao;
using TestesMariana.Dominio.ModuloTeste;
using TestesMariana.WinApp.Compartilhado;

namespace TestesMariana.WinApp.ModuloTeste
{
    public partial class TelaCadastroTesteForm : Form
    {
        private List<Disciplina> disciplinas;
        private List<Materia> materias;
        private List<Questao> questoes;
        private List<Questao> questoesFiltradas;
        private Teste teste;
        private bool gerouTeste;

        public TelaCadastroTesteForm(List<Disciplina> disciplinas, List<Materia> materias,
            List<Questao> questoes)
        {
            InitializeComponent();
            this.disciplinas = disciplinas;
            this.materias = materias;
            this.questoes = questoes;

            ManipularComboboxDisciplinas();

            questoesFiltradas = new List<Questao>();
        }

        public Teste Teste
        {
            get
            {
                return teste;
            }

            set
            {
                teste = value;
                txtNumero.Text = teste.Numero.ToString();
                numericUpDownQtdQuestoes.Value = teste.QtdQuestoes;
                txtTituloTeste.Text = teste.Titulo;
                comboBoxDisciplinas.SelectedItem = teste.Disciplina;

                if (teste.Materia != null)
                    comboBoxMaterias.SelectedItem = teste.Materia;
                else
                    comboBoxMaterias.SelectedIndex = -1;

                comboBoxSerie.SelectedItem = teste.Serie;
                questoesFiltradas = teste.Questoes;
            }
        }

        public Func<Teste, ValidationResult> GravarRegistro { get; set; }

        private void btnGerarTeste_Click(object sender, EventArgs e)
        {
            GerarTeste();
            if(gerouTeste)
                MessageBox.Show("Teste gerado na memória com sucesso!!", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();

            TelaPrincipalForm.Instancia.AtualizarRodape("", TipoMensagemRodape.VAZIO);
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            if (VerificaCampos())
            {
                if (checkBoxTesteDisciplinaInteira.Checked)
                {
                    questoesFiltradas = Filtrar(x => x.Materia.Serie == comboBoxSerie.Text
                    && x.Disciplina == comboBoxDisciplinas.SelectedItem);
                }
                else
                {
                    questoesFiltradas = Filtrar(x => x.Materia.Serie == comboBoxSerie.Text
                    && x.Disciplina == comboBoxDisciplinas.SelectedItem && x.Materia == comboBoxMaterias.SelectedItem);
                }

                labelMaxQuestoesEncontradas.Text = questoesFiltradas.Count.ToString();
                numericUpDownQtdQuestoes.Maximum = questoesFiltradas.Count;
            }
            else
                MessageBox.Show("A quantidade desejada deve ser menor que o total encontrado!", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void comboBoxDisciplinas_SelectedIndexChanged(object sender, EventArgs e)
        {
            ManipularComboboxMaterias();
        }

        private bool VerificaCampos()
        {
            if (comboBoxSerie.Text == "")
            {
                TelaPrincipalForm.Instancia.AtualizarRodape("Escolha uma Série primeiro!", TipoMensagemRodape.ERRO);
                //MessageBox.Show("Escolha uma Série primeiro", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (comboBoxDisciplinas.Text == "")
            {
                TelaPrincipalForm.Instancia.AtualizarRodape("Escolha uma Disciplina primeiro!", TipoMensagemRodape.ERRO);
                MessageBox.Show("Escolha uma Disciplina primeiro", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (checkBoxTesteDisciplinaInteira.Checked == false && comboBoxMaterias.Text == "")
            {
                TelaPrincipalForm.Instancia.AtualizarRodape("Escolha uma Matéria primeiro!", TipoMensagemRodape.ERRO);
                //MessageBox.Show("Escolha uma Matéria primeiro", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            return true;
        }

        private List<Questao> Filtrar(Predicate<Questao> condicao)
        {
            List<Questao> listaFiltrada = new List<Questao>();

            foreach (var questao in questoes)
            {
                if (condicao(questao))
                    listaFiltrada.Add(questao);
            }

            return listaFiltrada;
        }

        private void ManipularComboboxDisciplinas()
        {
            if (this.disciplinas.Count > 0)
                PopularComboboxDisciplinas();
        }

        private void PopularComboboxDisciplinas()
        {
            foreach (Disciplina disciplina in disciplinas)
                comboBoxDisciplinas.Items.Add(disciplina);
        }

        private void ManipularComboboxMaterias()
        {
            Disciplina disciplinaSelecionada = ObterDisciplinaSelecionada();

            List<Materia> materiasFiltradasPorDisciplina = new List<Materia>();

            if (disciplinaSelecionada != null)
            {
                materiasFiltradasPorDisciplina = materias.FindAll(x => x.Disciplina == disciplinaSelecionada);
                PopularComboboxMaterias(materiasFiltradasPorDisciplina);
            }
        }

        private void PopularComboboxMaterias(List<Materia> materias)
        {
            comboBoxMaterias.Items.Clear();

            foreach (Materia materia in materias)
                comboBoxMaterias.Items.Add(materia);
        }

        private Disciplina ObterDisciplinaSelecionada()
        {
            Disciplina disciplinaSelecionada = null;

            if (comboBoxDisciplinas.SelectedItem != null)
                disciplinaSelecionada = (Disciplina)comboBoxDisciplinas.SelectedItem;

            return disciplinaSelecionada;
        }

        private void LimparCampos()
        {
            numericUpDownQtdQuestoes.Text = "";
            txtTituloTeste.Text = "";
            labelMaxQuestoesEncontradas.Text = "0";
            comboBoxDisciplinas.SelectedIndex = -1;
            comboBoxMaterias.SelectedIndex = -1;
            comboBoxSerie.SelectedIndex = -1;
            
            gerouTeste = false;
        }

        private void checkBoxTesteDisciplinaInteira_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxTesteDisciplinaInteira.Checked)
            {
                comboBoxMaterias.Enabled = false;
                comboBoxMaterias.SelectedIndex = -1;
            }
            else
                comboBoxMaterias.Enabled = true;
        }

        private void GerarTeste()
        {
            if (VerificarCampoDeQtd())
            {
                int valor;

                int qtdInformadaUsuario = (int)numericUpDownQtdQuestoes.Value;

                Random randNum = new Random();

                teste.Questoes.Clear();

                while (qtdInformadaUsuario > teste.Questoes.Count)
                {
                    valor = randNum.Next(0, questoesFiltradas.Count);

                    teste.AdicionarQuestao(questoesFiltradas[valor]);
                }
                if (teste.Questoes.Count > 0)
                    gerouTeste = true;
                else
                    gerouTeste = false;
            }
        }

        private bool VerificarCampoDeQtd()
        {
            if (int.Parse(numericUpDownQtdQuestoes.Text) <= questoesFiltradas.Count)
                return true;

            return false;
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            Teste novoTeste = new Teste();

            novoTeste.Titulo = txtTituloTeste.Text;
            novoTeste.QtdQuestoes = (int)numericUpDownQtdQuestoes.Value;
            novoTeste.Disciplina = (Disciplina)comboBoxDisciplinas.SelectedItem;
            novoTeste.Materia = (Materia)comboBoxMaterias.SelectedItem;
            novoTeste.Serie = comboBoxSerie.Text;
            novoTeste.Questoes.AddRange(teste.Questoes);

            var resultadoValidacao = GravarRegistro(novoTeste);

            if (resultadoValidacao.IsValid == false)
            {
                string erro = resultadoValidacao.Errors[0].ErrorMessage;

                TelaPrincipalForm.Instancia.AtualizarRodape(erro, TipoMensagemRodape.ERRO);

                DialogResult = DialogResult.None;

            }
            else
                MessageBox.Show("Teste gravdo com sucesso!!", "Informativo",MessageBoxButtons.OK,MessageBoxIcon.Information);

        }
    }
}
