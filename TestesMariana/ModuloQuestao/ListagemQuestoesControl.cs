using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TestesMariana.Dominio.ModuloQuestao;
using TestesMariana.WinApp.Compartilhado;

namespace TestesMariana.WinApp.ModuloQuestao
{
    public partial class ListagemQuestoesControl : UserControl
    {
        public ListagemQuestoesControl()
        {
            InitializeComponent();

            grid.ConfigurarGridZebrado();
            grid.ConfigurarGridSomenteLeitura();
            grid.Columns.AddRange(ObterColunas());
        }


        public DataGridViewColumn[] ObterColunas()
        {
            var colunas = new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { DataPropertyName = "Numero", HeaderText = "Número"},

                //new DataGridViewTextBoxColumn { DataPropertyName = "Nome", HeaderText = "Nome da disciplina"},

            };

            return colunas;
        }

        public int ObtemNumeroQuestaoSelecionado()
        {
            return grid.SelecionarNumero<int>();
        }

        public void AtualizarRegistros(List<Questao> questoes)
        {
            grid.Rows.Clear();

            foreach (Questao questao in questoes)
            {
                grid.Rows.Add(questao.Numero);
            }
        }
    }
}
