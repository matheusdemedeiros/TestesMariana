using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TestesMariana.Dominio.ModuloDisciplina;
using TestesMariana.Dominio.ModuloMateria;
using TestesMariana.Dominio.ModuloQuestao;
using TestesMariana.Dominio.ModuloTeste;

namespace TestesMariana.WinApp.ModuloTeste
{
    public partial class TelaCadastroTesteForm : Form
    {
        private List<Disciplina> disciplinas;
        private List<Materia> materias;
        private List<Questao> questoes;
        private List<Questao> questoesFiltradas;
        private Teste teste;


        public TelaCadastroTesteForm(List<Disciplina> disciplinas, List<Materia> materias, List<Questao> questoes)
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
                if (teste.Numero != 0 && string.IsNullOrEmpty(txtTituloTeste.Text))
                {
                    txtNumero.Text = teste.Numero.ToString();
                    txtQtdQuestoes.Text = teste.QtdQuestoes.ToString();
                    txtTituloTeste.Text = teste.Titulo;
                    comboBoxDisciplinas.SelectedItem = teste.Disciplina;
                    
                    if(teste.Materia != null)
                        comboBoxMaterias.SelectedItem = teste.Materia;
                    else
                        comboBoxMaterias.SelectedIndex = -1;

                    comboBoxSerie.SelectedItem = teste.Serie;
                    questoesFiltradas = teste.Questoes;
                }
            }
        }

        public Func<Teste, ValidationResult> GravarRegistro { get; set; }


        private void btnGravar_Click(object sender, EventArgs e)
        {
            teste.Titulo = txtTituloTeste.Text;
            teste.QtdQuestoes = int.Parse(txtQtdQuestoes.Text);
            teste.Disciplina = (Disciplina)comboBoxDisciplinas.SelectedItem;
            teste.Materia = (Materia)comboBoxMaterias.SelectedItem;
            teste.Serie = comboBoxSerie.SelectedItem.ToString();
            
            GerarTeste();

            var resultadoValidacao = GravarRegistro(teste);

            if (resultadoValidacao.IsValid == false)
            {
                string erro = resultadoValidacao.Errors[0].ErrorMessage;

                TelaPrincipalForm.Instancia.AtualizarRodape(erro, Color.Red);

                DialogResult = DialogResult.None;
            }

        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LiparCampos();
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
                MessageBox.Show("Escolha uma Série primeiro", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (comboBoxDisciplinas.Text == "")
            {
                MessageBox.Show("Escolha uma Disciplina primeiro", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (checkBoxTesteDisciplinaInteira.Checked == false && comboBoxMaterias.Text == "")
            {
                MessageBox.Show("Escolha uma Matéria primeiro", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        private void LiparCampos()
        {
            txtQtdQuestoes.Text = "";
            txtTituloTeste.Text = "";
            labelMaxQuestoesEncontradas.Text = "0";
            comboBoxDisciplinas.SelectedIndex = -1;
            comboBoxMaterias.SelectedIndex = -1;
            comboBoxSerie.SelectedIndex = -1;
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

                int qtdInformadaUsuario = int.Parse(txtQtdQuestoes.Text);

                Random randNum = new Random();

                teste.Questoes.Clear();

                while (qtdInformadaUsuario > teste.Questoes.Count)
                {
                    valor = randNum.Next(0, questoesFiltradas.Count);

                    teste.AdicionarQuestao(questoesFiltradas[valor]);
                }
            }
        }

        private bool VerificarCampoDeQtd()
        {
            if (int.Parse(txtQtdQuestoes.Text) <= questoesFiltradas.Count)
                return true;

            return false;
        }

    }
}
