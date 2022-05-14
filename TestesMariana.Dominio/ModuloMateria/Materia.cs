using System;
using TestesMariana.Dominio.Compartilhado;
using TestesMariana.Dominio.ModuloDisciplina;

namespace TestesMariana.Dominio.ModuloMateria
{

    [Serializable]
    public class Materia : EntidadeBase<Materia>
    {
        public string Titulo { get; set; }
        
        public Disciplina Disciplina { get; set; }
        
        public string Serie { get; set; }

        public string NomeDisciplina => Disciplina.Nome;

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
            return $"Número: {Numero} - Título: {Titulo} - Disciplina: {Disciplina.Nome} - Série: {Serie}";
        }

        public Materia Clone()
        {
            return MemberwiseClone() as Materia;
        }




    }
}
