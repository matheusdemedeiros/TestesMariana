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
    public class ControladorQuestao : ControladorBase
    {
        private IRepositorioQuestao repositorioQuestao;
        private IRepositorioDisciplina repositorioDisciplina;
        private IRepositorioMateria repositorioMateria;
        private ListagemQuestoesControl listagemQuestoes;

        public ControladorQuestao(IRepositorioQuestao repositorioQuestao,
            IRepositorioDisciplina repositorioDisciplina, IRepositorioMateria reposirioMateria)
        {
            this.repositorioQuestao = repositorioQuestao;
            this.repositorioDisciplina = repositorioDisciplina;
            this.repositorioMateria = reposirioMateria;
        }


        public List<Disciplina> Disiciplinas
        {
            get
            {
                return repositorioDisciplina.SelecionarTodos();
            }
        }

        public List<Materia> Materias
        {
            get
            {
                return repositorioMateria.SelecionarTodos();
            }
        }

        public override void Inserir()
        {
            TelaCadastroQuestaoForm tela = new TelaCadastroQuestaoForm(Disiciplinas, Materias);

            tela.Questao = new Questao();

            tela.GravarRegistro = repositorioQuestao.Inserir;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
                CarregarQuestoes();

        }
        

        public override void Editar()
        {
            Questao questaoSelecionada = ObtemQuestaoSelecionada();

            if (questaoSelecionada == null)
            {
                MessageBox.Show("Selecione uma questão primeiro",
                "Edição de Questões", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TelaCadastroQuestaoForm tela = new TelaCadastroQuestaoForm(Disiciplinas, Materias);

            tela.Questao = questaoSelecionada.Clone();

            tela.GravarRegistro = repositorioQuestao.Editar;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
                CarregarQuestoes();
        }

        public override void Excluir()
        {
            Questao questaoSelecionada = ObtemQuestaoSelecionada();

            if (questaoSelecionada == null)
            {
                MessageBox.Show("Selecione uma questão primeiro",
                "Exclusão de Questões", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult resultado = MessageBox.Show("Deseja realmente excluir a questão?",
                "Exclusão de Questões", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (resultado == DialogResult.OK)
            {
                var resultadoExclusao = repositorioQuestao.Excluir(questaoSelecionada);

                if (resultadoExclusao.IsValid == false)
                {
                    string erro = resultadoExclusao.Errors[0].ErrorMessage;

                    MessageBox.Show(erro, "Exclusão de Questões - Informativo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                    MessageBox.Show("Questão excluída com sucesso!", "Exclusão de Matérias - Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CarregarQuestoes();
            }
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

            TelaPrincipalForm.Instancia.AtualizarRodape(
                $"Visualizando {questoes.Count} questão(ões)", TipoMensagemRodape.VISUALIZANDO);
        }

        private Questao ObtemQuestaoSelecionada()
        {
            var numero = listagemQuestoes.ObtemNumeroQuestaoSelecionado();

            return repositorioQuestao.SelecionarPorNumero(numero);
        }
    }
}
