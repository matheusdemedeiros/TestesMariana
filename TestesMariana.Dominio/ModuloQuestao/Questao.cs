using System;
using System.Collections.Generic;
using TestesMariana.Dominio.Compartilhado;
using TestesMariana.Dominio.ModuloDisciplina;
using TestesMariana.Dominio.ModuloMateria;

namespace TestesMariana.Dominio.ModuloQuestao
{
    public class Questao : EntidadeBase<Questao>
    {
        public string Enunciado { get; set; }

        public List<Alternativa> Alternativas { get; set; }

        public Materia Materia { get; set; }

        public Disciplina Disciplina => Materia.Disciplina;

        public Questao()
        {

        }

        public Questao(string enunciado, List<Alternativa> alternativas, Materia materia)
        {
            Enunciado = enunciado;
            Alternativas = alternativas;
            Materia = materia;
        }

        public override void Atualizar(Questao registro)
        {
            this.Enunciado = registro.Enunciado;
            this.Alternativas = registro.Alternativas;
            this.Materia = registro.Materia;
        }
        
        public override string ToString()
        {
            return $"Número: {Numero} Enunciado: {Enunciado} Matéria: {Materia.Titulo} Disciplina: {Disciplina}";
        }

        public Questao Clone()
        {
            return MemberwiseClone() as Questao;
        }

    }
}
