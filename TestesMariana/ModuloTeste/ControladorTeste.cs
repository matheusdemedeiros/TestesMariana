﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TestesMariana.Dominio.ModuloDisciplina;
using TestesMariana.Dominio.ModuloMateria;
using TestesMariana.Dominio.ModuloQuestao;
using TestesMariana.Dominio.ModuloTeste;
using TestesMariana.WinApp.Compartilhado;

namespace TestesMariana.WinApp.ModuloTeste
{
    public class ControladorTeste : ControladorBase
    {
        private IRepositorioQuestao repositorioQuestao;
        private IRepositorioDisciplina repositorioDisciplina;
        private IRepositorioMateria repositorioMateria;
        private IRepositorioTeste repositorioTeste;
        private ListagemTesteControl listagemTestes;

        public ControladorTeste(IRepositorioQuestao repositorioQuestao,
            IRepositorioDisciplina repositorioDisciplina,
            IRepositorioMateria repositorioMateria, IRepositorioTeste repositorioTeste)
        {
            this.repositorioQuestao = repositorioQuestao;
            this.repositorioDisciplina = repositorioDisciplina;
            this.repositorioMateria = repositorioMateria;
            this.repositorioTeste = repositorioTeste;
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
        public List<Questao> Questoes
        {
            get
            {
                return repositorioQuestao.SelecionarTodos();
            }
        }

        public override void Inserir()
        {
            TelaCadastroTesteForm tela = new TelaCadastroTesteForm(Disiciplinas, Materias, Questoes);

            tela.Teste = new Teste();

            tela.GravarRegistro = repositorioTeste.Inserir;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
                CarregarTestes();
        }

        public override void Editar()
        {
            throw new NotImplementedException();
        }

        public override void Excluir()
        {
            Teste testeSelecionado = ObtemTesteSelecionado();

            if (testeSelecionado == null)
            {
                MessageBox.Show("Selecione um teste primeiro!",
                "Exclusão de Matérias", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult resultado = MessageBox.Show("Deseja realmente excluir o teste?",
                "Exclusão de Testes", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (resultado == DialogResult.OK)
            {
                var resultadoExclusao = repositorioTeste.Excluir(testeSelecionado);

                if (resultadoExclusao.IsValid == false)
                {
                    string erro = resultadoExclusao.Errors[0].ErrorMessage;

                    MessageBox.Show(erro, "Exclusão de Testes - Informativo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                    MessageBox.Show("Teste excluído com sucesso!", "Exclusão de Testes - Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CarregarTestes();
            }
        }

        public override ConfiguracaoToolboxBase ObtemConfiguracaoToolbox()
        {
            return new ConfiguracaoToolboxTeste();
        }

        public override UserControl ObtemListagem()
        {
            if (listagemTestes == null)
                listagemTestes = new ListagemTesteControl();

            CarregarTestes();

            return listagemTestes;
        }

        private void CarregarTestes()
        {
            List<Teste> testes = repositorioTeste.SelecionarTodos();

            listagemTestes.AtualizarRegistros(testes);

            TelaPrincipalForm.Instancia.AtualizarRodape($"Visualizando {testes.Count} Teste(s)", Color.DarkBlue);
        }

        private Teste ObtemTesteSelecionado()
        {
            var numero = listagemTestes.ObtemNumeroTesteSelecionado();

            return repositorioTeste.SelecionarPorNumero(numero);
        }
    }
}
