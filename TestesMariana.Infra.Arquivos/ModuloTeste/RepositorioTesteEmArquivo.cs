using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;
using TestesMariana.Dominio.Compartilhado;
using TestesMariana.Dominio.ModuloTeste;
using TestesMariana.Infra.Arquivos.Compartilhado;

namespace TestesMariana.Infra.Arquivos.ModuloTeste
{
    public class RepositorioTesteEmArquivo : RepositorioEmArquivoBase<Teste>, IRepositorioTeste
    {
        public RepositorioTesteEmArquivo(DataContext dataContext) : base(dataContext)
        {
            if (dataContext.Testes.Count > 0)
                contador = dataContext.Testes.Max(x => x.Numero);
        }

        public ValidationResult Duplicar(Teste novoRegistro)
        {
            var resultadoValidacao = Validar(novoRegistro);

            if (resultadoValidacao.IsValid == false
                 && resultadoValidacao.Errors[0].ErrorMessage ==
                 "Não foi possível inserir, pois já existe um teste com este título cadastrado no sistema!")
            {
                novoRegistro.Titulo += $" - (Duplicado à partir de: {novoRegistro.Titulo})";
                resultadoValidacao = new ValidationResult();

            }

            if (resultadoValidacao.IsValid)
            {
                novoRegistro.Numero = ++contador;

                foreach (var questao in novoRegistro.Questoes)
                    questao.IncrementarQtdTestesRelacionados();

                var registros = ObterRegistros();

                registros.Add(novoRegistro);
            }
            return resultadoValidacao;
        }

        public ValidationResult Inserir(Teste novoRegistro)
        {
            var resultadoValidacao = Validar(novoRegistro);

            if (resultadoValidacao.IsValid == false
                 && resultadoValidacao.Errors[0].ErrorMessage ==
                 "Não foi possível inserir, pois já existe um teste com este título cadastrado no sistema!")
            {
                novoRegistro.Titulo += $" - (Duplicado à partir de: {novoRegistro.Titulo})";
                resultadoValidacao = new ValidationResult();

            }
            
            if (resultadoValidacao.IsValid)
            {
                novoRegistro.Numero = ++contador;

                foreach (var questao in novoRegistro.Questoes)
                    questao.IncrementarQtdTestesRelacionados();

                var registros = ObterRegistros();

                registros.Add(novoRegistro);
            }
            return resultadoValidacao;
        }

        public override ValidationResult Excluir(Teste registro)
        {
            var resultadoValidacao = new ValidationResult();

            var registros = ObterRegistros();

            foreach (var questao in registro.Questoes)
                questao.DecrementarQtdTestesRelacionados();

            if (registros.Remove(registro) == false)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o teste!!"));

            return resultadoValidacao;
        }

        public override List<Teste> ObterRegistros()
        {
            return dataContext.Testes;
        }

        public override AbstractValidator<Teste> ObterValidador()
        {
            return new ValidadorTeste();
        }

        private ValidationResult Validar(Teste registro)
        {
            var validator = ObterValidador();

            var resultadoValidacao = validator.Validate(registro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            resultadoValidacao = ValidarTitulo(registro);

            return resultadoValidacao;
        }

        private ValidationResult ValidarTitulo(Teste registro)
        {
            bool tituloRegistrado = VerificarSeOhTituloJaEstaRegistrado(registro);

            ValidationResult validacaoDeTitulo = new ValidationResult();

            if (tituloRegistrado && registro.Numero == 0)
                validacaoDeTitulo.Errors.Add(new ValidationFailure("", "Não foi possível inserir, pois já existe um teste com este título cadastrado no sistema!"));

            return validacaoDeTitulo;
        }

        private bool VerificarSeOhTituloJaEstaRegistrado(Teste registro)
        {
            return ObterRegistros().Exists(x => x.Titulo.SaoIguais(registro.Titulo));
        }
    }
}
