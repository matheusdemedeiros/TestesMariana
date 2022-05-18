using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;
using TestesMariana.Dominio.Compartilhado;
using TestesMariana.Dominio.ModuloDisciplina;
using TestesMariana.Dominio.ModuloMateria;

namespace TestesMariana.Dominio.ModuloQuestao
{
    public class Questao : EntidadeBase<Questao>
    {

        private int contadorASCII = 'a';

        public string Enunciado { get; set; }

        public List<Alternativa> Alternativas { get; set; }

        public Materia Materia { get; set; }

        public Disciplina Disciplina => Materia.Disciplina;

        public string Serie => Materia.Serie;

        public bool TemAlternativaCorretaCadastrada
        {
            get
            {
                return Alternativas.Exists(x => x.Correta == true);
            }
        }

        public Questao()
        {
            Alternativas = new List<Alternativa>();
        }

        public Questao(string enunciado, List<Alternativa> alternativas, Materia materia)
        {
            Enunciado = enunciado;
            Alternativas = alternativas;
            Materia = materia;
        }

        public ValidationResult AdicionarAlternativa(Alternativa alternativa)
        {
            var resultadoValidacao = ValidarAlternativa(alternativa);

            if (resultadoValidacao.IsValid)
            {
                Alternativas.Add(alternativa);

                AtualizarLetraAlternativas();
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
    }
}
