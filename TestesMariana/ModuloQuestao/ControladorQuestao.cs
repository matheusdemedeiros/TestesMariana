using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TestesMariana.Dominio.ModuloDisciplina;
using TestesMariana.Dominio.ModuloMateria;
using TestesMariana.Dominio.ModuloQuestao;
using TestesMariana.Infra.Arquivos.ModuloQuestao;
using TestesMariana.WinApp.Compartilhado;

namespace TestesMariana.WinApp.ModuloQuestao
{
    public class ControladorQuestao : ControladorBase
    {
        private RepositorioQuestaoEmArquivo repositorioQuestao;
        private IRepositorioDisciplina repositorioDisciplina;
        private IRepositorioMateria reposirioMateria;
        private ListagemQuestoesControl listagemQuestoes;

        public ControladorQuestao(RepositorioQuestaoEmArquivo repositorioQuestao,
            IRepositorioDisciplina repositorioDisciplina, IRepositorioMateria reposirioMateria)
        {
            this.repositorioQuestao = repositorioQuestao;
            this.repositorioDisciplina = repositorioDisciplina;
            this.reposirioMateria = reposirioMateria;
        }
        public override void Inserir()
        {

            TelaCadastroQuestaoForm tela = new TelaCadastroQuestaoForm();

            tela.Questao = new Questao();

            tela.GravarRegistro = repositorioQuestao.Inserir;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
                CarregarQuestoes();

        }

       

        public override void Editar()
        {
            throw new NotImplementedException();
        }

        public override void Excluir()
        {
            throw new NotImplementedException();
        }


        public override ConfiguracaoToolboxBase ObtemConfiguracaoToolbox()
        {
            return new ConfiguracaoToolboxQuestao();
        }


        public override UserControl ObtemListagem()
        {

            if (listagemQuestoes == null)
                listagemQuestoes = new ListagemQuestoesControl();

            CarregarQuestoes();

            return listagemQuestoes;
        }

        private void CarregarQuestoes()
        {
            List<Questao> questoes = repositorioQuestao.SelecionarTodos();

            listagemQuestoes.AtualizarRegistros(questoes);

            TelaPrincipalForm.Instancia.AtualizarRodape($"Visualizando {questoes.Count} questão(ões)", Color.DarkBlue);
        }

        private Questao ObtemDisciplinaSelecionada()
        {
            var numero = listagemQuestoes.ObtemNumeroQuestaoSelecionado();

            return repositorioQuestao.SelecionarPorNumero(numero);
        }
    }
}
