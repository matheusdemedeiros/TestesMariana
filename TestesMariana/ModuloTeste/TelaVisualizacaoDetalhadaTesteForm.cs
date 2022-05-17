using System;
using System.Windows.Forms;
using TestesMariana.Dominio.ModuloTeste;

namespace TestesMariana.WinApp.ModuloTeste
{
    public partial class TelaVisualizacaoDetalhadaTesteForm : Form
    {

        private Teste teste;


        public TelaVisualizacaoDetalhadaTesteForm(Teste testeSelecionado)
        {
            InitializeComponent();
        
            this.teste = testeSelecionado;

            CarregarDadosTeste();

        }

        private void CarregarDadosTeste()
        {
            listBoxTeste.Items.Add(teste.Titulo);

            foreach (var questao in teste.Questoes)
            {
                listBoxTeste.Items.Add(questao.Enunciado);

                foreach (var alternativa in questao.Alternativas)
                    listBoxTeste.Items.Add(alternativa);
            }
        }

    }
}
