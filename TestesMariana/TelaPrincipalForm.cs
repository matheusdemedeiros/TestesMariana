﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using TesteMariana.infra.DataBase.ModuloDisciplina;
using TesteMariana.infra.DataBase.ModuloMateria;
using TesteMariana.infra.DataBase.ModuloQuestao;
using TesteMariana.infra.DataBase.ModuloTeste;
using TestesMariana.Infra.Arquivos.Compartilhado;
using TestesMariana.WinApp.Compartilhado;
using TestesMariana.WinApp.ModuloDisciplina;
using TestesMariana.WinApp.ModuloMateria;
using TestesMariana.WinApp.ModuloQuestao;
using TestesMariana.WinApp.ModuloTeste;

namespace TestesMariana
{
    public partial class TelaPrincipalForm : Form
    {
        private ControladorBase controlador;
        private Dictionary<string, ControladorBase> controladores;
        private DataContext contextoDados;

        public TelaPrincipalForm(DataContext contextoDados)
        {
            InitializeComponent();

            Instancia = this;

            AtualizarRodape(string.Empty, TipoMensagemRodape.VAZIO);

            labelTipoCadastro.Text = string.Empty;

            this.contextoDados = contextoDados;

            InicializarControladores();
        }

        public static TelaPrincipalForm Instancia
        {
            get;
            private set;
        }

        private void disciplinasMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarTelaPrincipal((ToolStripMenuItem)sender);
        }

        private void materiasMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarTelaPrincipal((ToolStripMenuItem)sender);
        }

        private void questoesMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarTelaPrincipal((ToolStripMenuItem)sender);
        }

        private void testesEscolaresMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarTelaPrincipal((ToolStripMenuItem)sender);
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            controlador.Inserir();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            controlador.Editar();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            controlador.Excluir();
        }

        private void btnGerarPdf_Click(object sender, EventArgs e)
        {
            var controladorTeste = (ControladorTeste)controlador;
            controladorTeste.GerarPDF(false);
        }

        private void btnGerarPDFGabarito_Click(object sender, EventArgs e)
        {
            var controladorTeste = (ControladorTeste)controlador;
            controladorTeste.GerarPDF(true);
        }

        private void btnDuplicar_Click(object sender, EventArgs e)
        {
            var controladorTeste = (ControladorTeste)controlador;
            controladorTeste.Duplicar();
        }

        private void btnVisualizarDetalhadamente_Click(object sender, EventArgs e)
        {
            var controladorTeste = (ControladorTeste)controlador;
            controladorTeste.VisualizacaoDetalhadaTeste();
        }

        private void panelRegistros_ControlAdded(object sender, ControlEventArgs e)
        {
            panelRegistros.Focus();
        }

        private void ConfigurarBotoes(ConfiguracaoToolboxBase configuracao)
        {
            btnInserir.Enabled = configuracao.InserirHabilitado;
            btnEditar.Visible = configuracao.EditarHabilitado;
            btnExcluir.Enabled = configuracao.ExcluirHabilitado;
            btnGerarPdf.Visible = configuracao.GerarPDFHabilitado;
            btnDuplicar.Visible = configuracao.DuplicarHabilitado;
            btnGerarPDFGabarito.Visible = configuracao.PDFGabaritoHabilitado;
            btnVisualizarDetalhadamente.Visible = configuracao.VisualizarDetalhadamenteHabilitado;
        }

        private void ConfigurarTooltips(ConfiguracaoToolboxBase configuracao)
        {
            btnInserir.ToolTipText = configuracao.TooltipInserir;
            btnEditar.ToolTipText = configuracao.TooltipEditar;
            btnExcluir.ToolTipText = configuracao.TooltipExcluir;
            btnGerarPdf.ToolTipText = configuracao.TooltipGerarPDF;
            btnDuplicar.ToolTipText = configuracao.TooltipDuplicar;
            btnGerarPDFGabarito.ToolTipText = configuracao.TooltipGabarito;
            btnVisualizarDetalhadamente.ToolTipText = configuracao.TooltipVisualizarDetalhadamente;
        }

        private void ConfigurarTelaPrincipal(ToolStripMenuItem opcaoSelecionada)
        {
            var tipo = opcaoSelecionada.Text;

            controlador = controladores[tipo];

            ConfigurarToolbox();

            ConfigurarListagem();
        }

        public void AtualizarRodape(string mensagem, TipoMensagemRodape tipoMSG)
        {
            var corBack = Color.DarkBlue;

            labelRodape.ForeColor = Color.White;

            Thread segundaThread = new Thread

                (() =>
                    {
                        if (tipoMSG == TipoMensagemRodape.SUCESSO)
                        {
                            labelRodape.Text = mensagem;

                            statusStripRodape.BackColor = Color.Green;

                            //Thread.Sleep(3000);

                            //AtualizarRodape(string.Empty, TipoMensagemRodape.VAZIO);
                        }

                        if (tipoMSG == TipoMensagemRodape.ERRO)
                        {
                            labelRodape.Text = mensagem;

                            statusStripRodape.BackColor = Color.Red;

                            //Thread.Sleep(3000);

                            AtualizarRodape(string.Empty, TipoMensagemRodape.VAZIO);
                        }

                        if (tipoMSG == TipoMensagemRodape.VISUALIZANDO || tipoMSG == TipoMensagemRodape.VAZIO)
                        {
                            Thread.Sleep(1500);

                            labelRodape.Text = mensagem;

                            statusStripRodape.BackColor = corBack;
                        }
                    }
                );

            segundaThread.Start();
        }

        public void ControlaRodape(string mensagem, TipoMensagemRodape tipoMSG)
        {
            var corBack = Color.DarkBlue;

            labelRodape.ForeColor = Color.White;

            if (tipoMSG == TipoMensagemRodape.SUCESSO)
            {

                Thread segundaThread = new Thread

                    (() =>
                    {
                        labelRodape.Text = mensagem;

                        statusStripRodape.BackColor = Color.Green;

                        Thread.Sleep(3000);

                        AtualizarRodape(string.Empty, TipoMensagemRodape.VAZIO);

                    }
                    );

                segundaThread.Start();

            }

            if (tipoMSG == TipoMensagemRodape.ERRO)
            {
                labelRodape.Text = mensagem;

                statusStripRodape.BackColor = Color.Red;

            }

            if (tipoMSG == TipoMensagemRodape.VISUALIZANDO || tipoMSG == TipoMensagemRodape.VAZIO)
            {
                labelRodape.Text = mensagem;

                statusStripRodape.BackColor = corBack;
            }
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
            AtualizarRodape("", TipoMensagemRodape.VAZIO);

            var listagemControl = controlador.ObtemListagem();

            panelRegistros.Controls.Clear();

            listagemControl.Dock = DockStyle.Fill;

            panelRegistros.Controls.Add(listagemControl);
        }

        private void InicializarControladores()
        {
            var repositorioDisciplina = new RepositorioDisciplinaDB();
            var repositorioMateria = new RepositorioMateriaDB();
            var repositorioQuestao = new RepositorioQuestaoDB();
            var repositorioTeste = new RepositorioTesteDB();

            //var repositorioDisciplina = new RepositorioDisciplinaEmArquivo(contextoDados);
            //var repositorioMateria = new RepositorioMateriaEmArquivo(contextoDados);
            //var repositorioQuestao = new RepositorioQuestaoEmArquivo(contextoDados);
            //var repositorioTeste = new RepositorioTesteEmArquivo(contextoDados);

            controladores = new Dictionary<string, ControladorBase>();

            controladores.Add("Disciplinas", new ControladorDisciplina(repositorioDisciplina));
            controladores.Add("Matérias", new ControladorMateria(repositorioMateria, repositorioDisciplina));
            controladores.Add("Questões", new ControladorQuestao(repositorioQuestao, repositorioDisciplina, repositorioMateria));
            controladores.Add("Testes escolares", new ControladorTeste(repositorioQuestao, repositorioDisciplina,
               repositorioMateria, repositorioTeste));
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (toolbox.Enabled == true)
            {
                switch (keyData)
                {
                    case Keys.Control | Keys.I: controlador.Inserir(); break;
                    case Keys.Control | Keys.D: controlador.Excluir(); break;
                    case Keys.Control | Keys.E: controlador.Editar(); break;
                        //case Keys.Control | Keys.NumPad4: controlador.Inserir break;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
