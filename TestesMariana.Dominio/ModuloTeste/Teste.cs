using FluentValidation.Results;
using System;
using System.Collections.Generic;
using TestesMariana.Dominio.Compartilhado;
using TestesMariana.Dominio.ModuloDisciplina;
using TestesMariana.Dominio.ModuloMateria;
using TestesMariana.Dominio.ModuloQuestao;

namespace TestesMariana.Dominio.ModuloTeste
{
    public class Teste : EntidadeBase<Teste>, ICloneable
    {
        private DateTime? dataCriacao;

        #region PROPS

        public string Titulo { get; set; }

        public List<Questao> Questoes { get; set; }

        public Disciplina Disciplina { get; set; }

        public Materia? Materia { get; set; }

        public string Serie { get; set; }
        
        public DateTime? DataCriacao { get => dataCriacao; set => dataCriacao = value; }

        #endregion

        public Teste()
        {
            if (dataCriacao == null)
                dataCriacao = DateTime.Now;

            Questoes = new List<Questao>();
        }

        #region MÉTODOS PÚBLICOS

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

        public object Clone()
        {
            return new Teste(this);
        }
        
        public override void Atualizar(Teste registro)
        {
            this.Titulo = registro.Titulo;
            this.DataCriacao = registro.DataCriacao;
            this.Questoes = registro.Questoes;
            this.Materia = registro.Materia;
            this.Disciplina = registro.Disciplina;
            this.Serie = registro.Serie;
        }

        public override string ToString()
        {
            return Titulo;
        }

        #endregion

        #region MÉTODOS PRIVADOS

        private Teste(Teste registro)
        {
            this.Titulo = registro.Titulo;
            this.DataCriacao = registro.DataCriacao;
            this.Questoes = registro.Questoes;
            this.Materia = registro.Materia;
            this.Disciplina = registro.Disciplina;
            this.Serie = registro.Serie;
            this.DataCriacao = DateTime.Now;
        }
        
        private ValidationResult ValidarQuestao(Questao questao)
        {
            var resultadoValidacao = new ValidationResult();

            if (VerificarSeAhQuestaoJaEstaPresenteNoTeste(questao))
                resultadoValidacao.Errors.Add(new ValidationFailure("", "A questão já está no teste!"));

            return resultadoValidacao;
        }

        private bool VerificarSeAhQuestaoJaEstaPresenteNoTeste(Questao questao)
        {
            return Questoes.Exists(x => x.Enunciado.SaoIguais(questao.Enunciado));
        }
        
        #endregion
    }
}
