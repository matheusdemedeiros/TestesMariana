using System;
using System.Collections.Generic;
using TestesMariana.Dominio.Compartilhado;
using TestesMariana.Dominio.ModuloDisciplina;

namespace TestesMariana.Dominio.ModuloMateria
{
    public class Materia : EntidadeBase<Materia>
    {
        #region PROPS

        public string Titulo { get; set; }
        
        public Disciplina Disciplina { get; set; }
        
        public string Serie { get; set; }

        public string NomeDisciplina => Disciplina.Nome;

        [SkipProperty]
        public bool PodeExcluir => QtdQuestoesRelacionadas == 0 ? true : false;

        [SkipProperty]
        public int QtdQuestoesRelacionadas = 0;
        
        #endregion

        public Materia()
        {

        }

        #region MÉTODOS PÚBLICOS

        public void IncrementarQtdQuestoesRelacionadas()
        {
            QtdQuestoesRelacionadas++;
        }

        public void DecrementarQtdQuestoesRelacionadas()
        {
            QtdQuestoesRelacionadas--;
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

        public override bool Equals(object obj)
        {
            return obj is Materia materia &&
                   Numero == materia.Numero &&
                   Titulo == materia.Titulo &&
                   EqualityComparer<Disciplina>.Default.Equals(Disciplina, materia.Disciplina) &&
                   Serie == materia.Serie &&
                   NomeDisciplina == materia.NomeDisciplina &&
                   PodeExcluir == materia.PodeExcluir;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Numero, Titulo, Disciplina, Serie, NomeDisciplina, PodeExcluir);
        }
        
        #endregion
    }
}
