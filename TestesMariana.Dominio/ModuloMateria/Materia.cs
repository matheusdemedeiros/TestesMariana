using TestesMariana.Dominio.Compartilhado;
using TestesMariana.Dominio.ModuloDisciplina;

namespace TestesMariana.Dominio.ModuloMateria
{
    public class Materia : EntidadeBase<Materia>
    {
        public string Titulo { get; set; }
        
        public Disciplina Disciplina { get; set; }
        
        public string Serie { get; set; }

        public string NomeDisciplina => Disciplina.Nome;

        [SkipProperty]
        public bool PodeExcluir => QtdQuestoesRelacionadas == 0 ? true : false;

        [SkipProperty]
        public int QtdQuestoesRelacionadas = 0;

        public void IncrementarQtdQuestoesRelacionadas()
        {
            QtdQuestoesRelacionadas++;
        }

        public void DecrementarQtdQuestoesRelacionadas()
        {
            QtdQuestoesRelacionadas--;
        }

        public Materia()
        {

        }

        public Materia(string titulo, Disciplina disciplina, string serie)
        {
            Titulo = titulo;
            Disciplina = disciplina;
            Serie = serie;
        }
        
        public override void Atualizar(Materia registro)
        {
            this.Titulo = registro.Titulo;
            this.Disciplina = registro.Disciplina;
            this.Serie = registro.Serie;
        }

        public override string ToString()
        {
            return Titulo;
        }

        public Materia Clone()
        {
            return MemberwiseClone() as Materia;
        }

    }
}
