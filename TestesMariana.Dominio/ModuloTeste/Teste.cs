using System;
using System.Collections.Generic;
using TestesMariana.Dominio.Compartilhado;
using TestesMariana.Dominio.ModuloDisciplina;
using TestesMariana.Dominio.ModuloMateria;
using TestesMariana.Dominio.ModuloQuestao;

namespace TestesMariana.Dominio.ModuloTeste
{
    public class Teste : EntidadeBase<Teste>
    {

        private DateTime? dataCriacao;

        public int QtdQuestoes { get; set; }

        public string Titulo { get; set; }

        public List<Questao> Questoes { get; set; }

        public Disciplina Disciplina { get; set; }

        public Materia Materia { get; set; }

        public string Serie { get; set; }

        public DateTime? DataCriacao
        {
            get
            {
                return dataCriacao;
            }
        }

        public Teste()
        {
            if (dataCriacao == null)
                dataCriacao = DateTime.Now;
        }


        public override void Atualizar(Teste registro)
        {
            throw new NotImplementedException();
        }
    }
}
