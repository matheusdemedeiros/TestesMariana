using FluentValidation.Results;
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

        public Materia? Materia { get; set; }

        public string Serie { get; set; }

        public DateTime? DataCriacao
        {
            get
            {
                return dataCriacao;
            }

            set { dataCriacao = value; }

        }

        public Teste()
        {
            if (dataCriacao == null)
                dataCriacao = DateTime.Now;

            Questoes = new List<Questao>();
        }

        public ValidationResult AdicionarQuestao(Questao questao)
        {
            var resultadoValidacao = ValidarQuestao(questao);

            if (resultadoValidacao.IsValid)
                Questoes.Add(questao);

            return resultadoValidacao;
        }

        public ValidationResult ExcluirQuestao(Questao questao)
        {
            var resultadoValidacao = new ValidationResult();

            if (Questoes.Remove(questao) == false)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível excluir a questão!"));

            return resultadoValidacao;
        }

        private ValidationResult ValidarQuestao(Questao questao)
        {
            var resultadoValidacao = new ValidationResult();

            if(VerificarSeAhQuestaoJaEstaPresenteNoTeste(questao))
                resultadoValidacao.Errors.Add(new ValidationFailure("", "A questão já está no teste!"));

            return resultadoValidacao;
        }

        private bool VerificarSeAhQuestaoJaEstaPresenteNoTeste(Questao questao)
        {
            return Questoes.Exists(x => x.Enunciado.SaoIguais(questao.Enunciado));
        }

        public Teste Clone()
        {
            return MemberwiseClone() as Teste;
        }

        public override void Atualizar(Teste registro)
        {
            this.Titulo = registro.Titulo;
            this.DataCriacao = registro.DataCriacao;
            this.QtdQuestoes = registro.QtdQuestoes;
            this.Questoes = registro.Questoes;
            this.Materia = registro.Materia;
            this.Disciplina = registro.Disciplina;
            this.Serie = registro.Serie;
        }

        public override string ToString()
        {
            return Titulo;
        }
    }
}
