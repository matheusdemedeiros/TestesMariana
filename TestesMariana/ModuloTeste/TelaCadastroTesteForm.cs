using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;
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

        #region PROPS

        public Teste Teste
        {
            get => teste;
            set
            {
                teste = value;
                txtTituloTeste.Text = teste.Titulo;

                txtNumero.Text = teste.Numero.ToString();

                if (teste.Numero != 0)
                {
                    comboBoxSerie.SelectedItem = teste.Serie;

                    ManipularComboboxDisciplinas();

                    comboBoxDisciplinas.SelectedItem = teste.Disciplina;

                    if (teste.Materia == null)
                    {
                        checkBoxTesteDisciplinaInteira.Checked = true;
                        comboBoxMaterias.Enabled = false;
                        comboBoxMaterias.SelectedIndex = -1;
                    }
                    else
                    {
                        ManipularComboboxMaterias();

                        comboBoxMaterias.SelectedItem = teste.Materia;
                    }
                }
                else
                    numericUpDownQtdQuestoes.Value = 0;
            }
        }

        public Func<Teste, ValidationResult> GravarRegistro { get; set; }

        #endregion

        #region MÉTODOS DE EVENTO

        private void btnGerarTeste_Click(object sender, EventArgs e)
        {
            if (VerificaCampos())
            {
                AplicarFiltro();
                if (questoesFiltradas.Count > 0)
                {
                    var qtdInformadaQuestoes = (int)numericUpDownQtdQuestoes.Value;

                    Teste testeAhSerPassado;

                    Teste novoTeste = new Teste();

                    if (teste.Numero == 0)
                        testeAhSerPassado = teste;
                    else
                        testeAhSerPassado = novoTeste;


                    if (qtdInformadaQuestoes > questoesFiltradas.Count)
                        GerarTeste(questoesFiltradas.Count, testeAhSerPassado);
                    else
                        GerarTeste(qtdInformadaQuestoes, testeAhSerPassado);

                    novoTeste.Titulo = txtTituloTeste.Text;
                    novoTeste.Disciplina = (Disciplina)comboBoxDisciplinas.SelectedItem;
                    novoTeste.Materia = (Materia)comboBoxMaterias.SelectedItem;
                    novoTeste.Serie = comboBoxSerie.Text;
                    novoTeste.Questoes = testeAhSerPassado.Questoes;

                    var resultadoValidacao = GravarRegistro(novoTeste);

                    if (resultadoValidacao.IsValid == false)
                    {
                        string erro = resultadoValidacao.Errors[0].ErrorMessage;

                        TelaPrincipalForm.Instancia.AtualizarRodape(erro, TipoMensagemRodape.ERRO);

                        DialogResult = DialogResult.None;
                    }
                    else
                    {
                        if (gerouTeste && qtdInformadaQuestoes > questoesFiltradas.Count)
                        {
                            MessageBox.Show($"Teste gerado somente com - {questoesFiltradas.Count} - questão(ões)," +
                                $" pois a quantidade informada é maior que os resultados encontrados!!", "Informativo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show($"Teste gerado com {qtdInformadaQuestoes} questões com sucesso!!", "Informativo",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    MessageBox.Show($"O sistema não encontrou nenhuma questão com os filtros aplicados!!", "Informativo",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
                DialogResult = DialogResult.None;
        }

        private void comboBoxDisciplinas_SelectedIndexChanged(object sender, EventArgs e)
        {
            ManipularComboboxMaterias();
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

        private void comboBoxSerie_SelectedIndexChanged(object sender, EventArgs e)
        {
            ManipularComboboxDisciplinas();

            ManipularComboboxMaterias();
        }

        #endregion

        #region MÉTODOS FUNCIONAIS

        private void AplicarFiltro()
        {
            if (checkBoxTesteDisciplinaInteira.Checked)
            {
                questoesFiltradas = Filtrar(x => x.Materia.Serie == comboBoxSerie.Text
                && x.Disciplina.Numero == ((Disciplina)comboBoxDisciplinas.SelectedItem).Numero);
            }
            else
            {
                questoesFiltradas = Filtrar(x => x.Materia.Serie == comboBoxSerie.Text
                && x.Disciplina.Numero == ((Disciplina)comboBoxDisciplinas.SelectedItem).Numero
                && x.Materia.Numero == ((Materia)comboBoxMaterias.SelectedItem).Numero);
            }
        }

        private bool VerificaCampos()
        {
            StringBuilder sb = new StringBuilder();

            bool retorno = true;

            if (comboBoxSerie.Text == "")
                sb.Append("* Selecione uma Série primeiro!\n\n");

            if (comboBoxDisciplinas.Text == "")
                sb.Append("* Selecione uma Disciplina primeiro!\n\n");

            if (checkBoxTesteDisciplinaInteira.Checked == false && comboBoxMaterias.Text == "")
                sb.Append("* Selecione uma Matéria primeiro!\n\n");

            if ((int)numericUpDownQtdQuestoes.Value == 0)
                sb.Append("* Informe a quantidade desejada de questões primeiro!\n\n");

            if (sb.Length > 0)
            {
                MessageBox.Show(sb.ToString(), "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                retorno = false;
            }

            return retorno;
        }

        private void ManipularComboboxDisciplinas()
        {
            if (comboBoxSerie.SelectedIndex != -1)
            {
                List<Disciplina> disciplinasFiltradasPorSerie = new List<Disciplina>();
                foreach (var item in materias)
                    if (item.Serie == comboBoxSerie.Text && disciplinasFiltradasPorSerie.Contains(item.Disciplina) == false)
                        disciplinasFiltradasPorSerie.Add(item.Disciplina);
                PopularComboboxDisciplinas(disciplinasFiltradasPorSerie);
            }
        }

        private void PopularComboboxDisciplinas(List<Disciplina> disciplinas)
        {
            comboBoxDisciplinas.Items.Clear();
            foreach (Disciplina disciplina in disciplinas)
                comboBoxDisciplinas.Items.Add(disciplina);
        }

        private void ManipularComboboxMaterias()
        {
            Disciplina disciplinaSelecionada = ObterDisciplinaSelecionada();

            List<Materia> materiasFiltradasPorDisciplina = new List<Materia>();

            comboBoxMaterias.Items.Clear();

            if (disciplinaSelecionada != null)
            {
                materiasFiltradasPorDisciplina = materias.FindAll(x => x.Disciplina.Numero == disciplinaSelecionada.Numero
                && x.Serie == comboBoxSerie.Text);
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

        private void GerarTeste(int qtdInformada, Teste teste)
        {
            int valor;

            int qtdInformadaUsuario = qtdInformada;

            Random randNum = new Random();

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

        #endregion
    }
}
