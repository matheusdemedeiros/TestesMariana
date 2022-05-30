using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using TestesMariana.Dominio.Compartilhado;
using TestesMariana.Dominio.ModuloDisciplina;
using TestesMariana.Dominio.ModuloMateria;

namespace TestesMariana.Dominio.ModuloQuestao
{
    public class Questao : EntidadeBase<Questao>
    {
        private int contadorASCII = 'a';
        private int maxQtdAlternativas = 4;

        #region PROPS

        public string Enunciado { get; set; }

        public List<Alternativa> Alternativas { get; set; }

        public Materia Materia { get; set; }

        public Disciplina Disciplina => Materia.Disciplina;

        public string Serie => Materia.Serie;

        [SkipProperty]
        public bool TemAlternativaCorretaCadastrada => Alternativas.Exists(x => x.Correta == true);
        
        [SkipProperty]
        public bool PòdeExcluir => QtdTestesRelacionados == 0 ? true : false;

        [SkipProperty]
        public int QtdTestesRelacionados = 0;
        
        #endregion
        
        public Questao()
        {
            Alternativas = new List<Alternativa>();
        }

        #region MÉTODOS PÚBLICOS

        public void IncrementarQtdTestesRelacionados()
        {
            QtdTestesRelacionados++;
        }

        public void DecrementarQtdTestesRelacionados()
        {
            QtdTestesRelacionados--;
        }

        public ValidationResult AdicionarAlternativa(Alternativa alternativa)
        {
            var resultadoValidacao = ValidarAlternativa(alternativa);

            if (resultadoValidacao.IsValid)
            {
                if (Alternativas.Count < maxQtdAlternativas)
                {
                    Alternativas.Add(alternativa);

                    AtualizarLetraAlternativas();
                }
                else
                    resultadoValidacao.Errors.Add(new ValidationFailure("", $"É possível adicionar somente {maxQtdAlternativas} alternativas por questão!"));
            }

            return resultadoValidacao;
        }

        public ValidationResult ExcluirAlternativa(Alternativa alternativa)
        {
            var resultadoValidacao = new ValidationResult();

            if (Alternativas.Remove(alternativa) == false)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível excluir a alternativa!"));
            else
                AtualizarLetraAlternativas();

            return resultadoValidacao;
        }

        private ValidationResult ValidarAlternativa(Alternativa alternativa)
        {
            var validador = ObterValidadorAlternativa();

            var resultadoValidacao = validador.Validate(alternativa);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            resultadoValidacao = ValidarDescricaoAlternativa(alternativa);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            resultadoValidacao = ValidarAlternativaCorreta(alternativa);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            return resultadoValidacao;
        }

        private ValidationResult ValidarDescricaoAlternativa(Alternativa alternativa)
        {
            bool descricaoAlternativaRegistrada = VerificarSeAhDescricaoAlternativaJaEstaRegistrada(alternativa);

            ValidationResult validacaoDeDescricao = new ValidationResult();

            if (descricaoAlternativaRegistrada)
                validacaoDeDescricao.Errors.Add(new ValidationFailure("", "Não foi possível inserir, pois já existe uma alternativa com esta descrição cadastrada no sistema!"));

            return validacaoDeDescricao;
        }

        private ValidationResult ValidarAlternativaCorreta(Alternativa alternativa)
        {
            bool alternativaCorretaJaRegistrada = TemAlternativaCorretaCadastrada;

            ValidationResult validacaoDeDescricao = new ValidationResult();

            if (alternativaCorretaJaRegistrada && alternativa.Correta)
                validacaoDeDescricao.Errors.Add(new ValidationFailure("", "Não foi possível inserir, pois já existe uma alternativa correta cadastrada na questão atual!"));

            return validacaoDeDescricao;
        }

        private bool VerificarSeAhDescricaoAlternativaJaEstaRegistrada(Alternativa alternativa)
        {
            return Alternativas.Exists(x => x.Descricao.SaoIguais(alternativa.Descricao));
        }

        private AbstractValidator<Alternativa> ObterValidadorAlternativa()
        {
            return new ValidadorAlternativa();
        }

        public override void Atualizar(Questao registro)
        {
            this.Enunciado = registro.Enunciado;
            this.Alternativas = registro.Alternativas;
            this.Materia = registro.Materia;
        }

        public override string ToString()
        {
            return Enunciado;
        }

        public Questao Clone()
        {
            return MemberwiseClone() as Questao;
        }

        private void AtualizarLetraAlternativas()
        {
            char resultado = (char)contadorASCII;

            foreach (var alternativa in Alternativas)
            {
                alternativa.Letra = resultado;
                resultado++;
            }

        }

        public override bool Equals(object obj)
        {
            return obj is Questao questao &&
                   Numero == questao.Numero &&
                   contadorASCII == questao.contadorASCII &&
                   Enunciado == questao.Enunciado &&
                   EqualityComparer<List<Alternativa>>.Default.Equals(Alternativas, questao.Alternativas) &&
                   EqualityComparer<Materia>.Default.Equals(Materia, questao.Materia) &&
                   EqualityComparer<Disciplina>.Default.Equals(Disciplina, questao.Disciplina) &&
                   Serie == questao.Serie &&
                   TemAlternativaCorretaCadastrada == questao.TemAlternativaCorretaCadastrada &&
                   PòdeExcluir == questao.PòdeExcluir &&
                   QtdTestesRelacionados == questao.QtdTestesRelacionados;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Numero);
            hash.Add(contadorASCII);
            hash.Add(Enunciado);
            hash.Add(Alternativas);
            hash.Add(Materia);
            hash.Add(Disciplina);
            hash.Add(Serie);
            hash.Add(TemAlternativaCorretaCadastrada);
            hash.Add(PòdeExcluir);
            hash.Add(QtdTestesRelacionados);
            return hash.ToHashCode();
        }


        #endregion
    }
}
