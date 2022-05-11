using System.Collections.Generic;
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

        public ControladorMateria(IRepositorioMateria repositorioMateria, IRepositorioDisciplina repositorioDisciplina)
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

        public override void Editar()
        {
            throw new System.NotImplementedException();
        }

        public override void Excluir()
        {
            throw new System.NotImplementedException();
        }

        public override void Inserir()
        {
            TelaCatastroMateriasForm tela = new TelaCatastroMateriasForm(Disiciplinas);
        }

        public override ConfiguracaoToolboxBase ObtemConfiguracaoToolbox()
        {
            throw new System.NotImplementedException();
        }

        public override UserControl ObtemListagem()
        {
            throw new System.NotImplementedException();
        }



    }
}
