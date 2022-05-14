using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TestesMariana.Dominio.ModuloDisciplina;
using TestesMariana.Dominio.ModuloMateria;
using TestesMariana.WinApp.Compartilhado;

namespace TestesMariana.WinApp.ModuloMateria
{
    public class ControladorMateria : ControladorBase
    {
        private readonly IRepositorioMateria repositorioMateria;

        private readonly IRepositorioDisciplina repositorioDisciplina;

        private ListagemMateriasControl listagemMaterias;

        public ControladorMateria(IRepositorioMateria repositorioMateria,
            IRepositorioDisciplina repositorioDisciplina)
        {
            this.repositorioMateria = repositorioMateria;
            this.repositorioDisciplina = repositorioDisciplina;
        }

        public List<Disciplina> Disiciplinas
        {
            get
            {
                return repositorioDisciplina.SelecionarTodos();
            }
        }

        public override void Inserir()
        {
            TelaCadastroMateriasForm tela = new TelaCadastroMateriasForm(Disiciplinas);

            tela.Materia = new Materia();

            tela.GravarRegistro = repositorioMateria.Inserir;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
                CarregarMaterias();
        }

        public override void Editar()
        {
            Materia materiaSelecionada = ObtemMateriaSelecionada();

            if (materiaSelecionada == null)
            {
                MessageBox.Show("Selecione uma materia primeiro",
                "Edição de Matérias", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TelaCadastroMateriasForm tela = new TelaCadastroMateriasForm(Disiciplinas);

            tela.Materia = materiaSelecionada.Clone();

            tela.GravarRegistro = repositorioMateria.Editar;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
                CarregarMaterias();
        }

        public override void Excluir()
        {
            Materia materiaSelecionada = ObtemMateriaSelecionada();

            if (materiaSelecionada == null)
            {
                MessageBox.Show("Selecione uma matéria primeiro",
                "Exclusão de Matérias", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult resultado = MessageBox.Show("Deseja realmente excluir a matéria?",
                "Exclusão de Matérias", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (resultado == DialogResult.OK)
            {
                var resultadoExclusao = repositorioMateria.Excluir(materiaSelecionada);

                if (resultadoExclusao.IsValid == false)
                {
                    string erro = resultadoExclusao.Errors[0].ErrorMessage;

                    MessageBox.Show(erro, "Exclusão de Matérias - Informativo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                    MessageBox.Show("Matéria excluída com sucesso!", "Exclusão de Matérias - Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CarregarMaterias();
            }
        }

        public override ConfiguracaoToolboxBase ObtemConfiguracaoToolbox()
        {
            return new ConfiguracaoTollboxMateria();
        }

        public override UserControl ObtemListagem()
        {
            if (listagemMaterias == null)
                listagemMaterias = new ListagemMateriasControl();

            CarregarMaterias();

            return listagemMaterias;
        }

        private void CarregarMaterias()
        {
            List<Materia> materias = repositorioMateria.SelecionarTodos();

            listagemMaterias.AtualizarRegistros(materias);

            TelaPrincipalForm.Instancia.AtualizarRodape($"Visualizando {materias.Count} materia(s)", Color.DarkBlue);
        }

        private Materia ObtemMateriaSelecionada()
        {
            var numero = listagemMaterias.ObtemNumeroMateriaSelecionado();

            return repositorioMateria.SelecionarPorNumero(numero);
        }
    }
}
