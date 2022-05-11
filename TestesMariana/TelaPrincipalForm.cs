using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TestesMariana.Infra.Arquivos.Compartilhado;
using TestesMariana.WinApp.Compartilhado;
using TestesMariana.WinApp.ModuloDisciplina;

namespace TestesMariana
{
    public partial class TelaPrincipalForm : Form
    {

        private ControladorBase controlador;
        private Dictionary<string, ControladorBase> controladores;
        private DataContext contextoDados;


        public TelaPrincipalForm()
        {

           Instancia = this;

            labelRodape.Text = string.Empty;

            //labelTipoCadastro.Text = string.Empty;

            this.contextoDados = contextoDados;

            InitializeComponent();
        }


        public static TelaPrincipalForm Instancia
        {
            get;
            private set;
        }



        private void disciplinasMenuItem_Click(object sender, EventArgs e)
        {
            ListagemDisciplinasControl listagem = new ListagemDisciplinasControl();
            
            listagem.Dock = DockStyle.Fill;
            
            panelRegistros.Controls.Clear();

            panelRegistros.Controls.Add(listagem);
        }

        private void materiasMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void questoesMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void testesEscolaresMenuItem_Click(object sender, EventArgs e)
        {

        }

        public void AtualizarRodape(string mensagem)
        {
            labelRodape.Text = mensagem;
        }


        private void ConfigurarBotoes(ConfiguracaoToolboxBase configuracao)
        {
            btnInserir.Enabled = configuracao.InserirHabilitado;
            btnEditar.Enabled = configuracao.EditarHabilitado;
            btnExcluir.Enabled = configuracao.ExcluirHabilitado;
        }

        private void ConfigurarTooltips(ConfiguracaoToolboxBase configuracao)
        {
            btnInserir.ToolTipText = configuracao.TooltipInserir;
            btnEditar.ToolTipText = configuracao.TooltipEditar;
            btnExcluir.ToolTipText = configuracao.TooltipExcluir;
        }

        private void ConfigurarTelaPrincipal(ToolStripMenuItem opcaoSelecionada)
        {
            var tipo = opcaoSelecionada.Text;

            controlador = controladores[tipo];

            ConfigurarToolbox();

            ConfigurarListagem();
        }

        private void ConfigurarToolbox()
        {
            ConfiguracaoToolboxBase configuracao = controlador.ObtemConfiguracaoToolbox();

            if (configuracao != null)
            {
                toolbox.Enabled = true;

                labelTipoCadastro.Text = configuracao.TipoCadastro;

                ConfigurarTooltips(configuracao);

                ConfigurarBotoes(configuracao);
            }
        }

        private void ConfigurarListagem()
        {
            AtualizarRodape("");

            var listagemControl = controlador.ObtemListagem();

            panelRegistros.Controls.Clear();

            listagemControl.Dock = DockStyle.Fill;

            panelRegistros.Controls.Add(listagemControl);
        }

        //private void InicializarControladores()
        //{
        //    var repositorioTarefa = new RepositorioTarefaEmArquivo(contextoDados);
        //    var repositorioContato = new RepositorioContatoEmArquivo(contextoDados);
        //    var repositorioCompromisso = new RepositorioCompromissoEmArquivo(contextoDados);
        //    var repositorioDespesa = new RepositorioDespesaEmArquivo(contextoDados);

        //    controladores = new Dictionary<string, ControladorBase>();

        //    controladores.Add("Tarefas", new ControladorTarefa(repositorioTarefa));
        //    controladores.Add("Contatos", new ControladorContato(repositorioContato));
        //    controladores.Add("Compromissos", new ControladorCompromisso(repositorioCompromisso, repositorioContato));
        //    controladores.Add("Despesas", new ControladorDespesa(repositorioDespesa));
        //}




    }
}
