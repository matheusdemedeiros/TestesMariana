using System;
using TestesMariana.Dominio.Compartilhado;

namespace TestesMariana.Dominio.ModuloDisciplina
{
    public class Disciplina : EntidadeBase<Disciplina>
    {
        public Disciplina()
        {

        }

        #region PROPS
        
        public string Nome { get; set; }

        [SkipProperty]
        public bool PodeExcluir => QtdMateriasRelacionadas == 0 && QtdQuestoesRelacionadas == 0 ? true : false;

        [SkipProperty]
        public int QtdMateriasRelacionadas = 0;

        [SkipProperty]
        public int QtdQuestoesRelacionadas = 0;

        #endregion

        #region MÉTODOS PÚBLICOS

        public void IncrementarQtdMateriasRelacionadas()
        {
            QtdMateriasRelacionadas++;
        }

        public void DecrementarQtdMateriasRelacionadas()
        {
            QtdMateriasRelacionadas--;
        }

        public void IncrementarQtdQuestoesRelacionadas()
        {
            QtdQuestoesRelacionadas++;
        }

        public void DecrementarQtdQuestoesRelacionadas()
        {
            QtdQuestoesRelacionadas--;
        }

        public override void Atualizar(Disciplina registro)
        {
            this.Nome = registro.Nome;
        }

        public override string ToString()
        {
            return Nome;
        }

        public Disciplina Clone()
        {
            return MemberwiseClone() as Disciplina;
        }

        public override bool Equals(object obj)
        {
            return obj is Disciplina disciplina &&
                   Numero == disciplina.Numero &&
                   Nome == disciplina.Nome;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Numero, Nome);
        }

        #endregion
    }
}
