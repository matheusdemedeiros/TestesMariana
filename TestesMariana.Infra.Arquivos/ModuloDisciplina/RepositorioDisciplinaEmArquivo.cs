using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;
using TestesMariana.Dominio.ModuloDisciplina;
using TestesMariana.Infra.Arquivos.Compartilhado;

namespace TestesMariana.Infra.Arquivos.ModuloDisciplina
{
    public class RepositorioDisciplinaEmArquivo : RepositorioEmArquivoBase<Disciplina>, IRepositorioDisciplina
    {


        public override ValidationResult Inserir(Disciplina novoRegistro)
        {
            var resultadoValidacao = Validar(novoRegistro);

            if (resultadoValidacao.IsValid)
            {
                novoRegistro.Numero = ++contador;

                var registros = ObterRegistros();

                registros.Add(novoRegistro);
            }

            return resultadoValidacao;
        }

        public RepositorioDisciplinaEmArquivo(DataContext context) : base(context)
        {
            if (dataContext.Disciplinas.Count > 0)
                contador = dataContext.Disciplinas.Max(x => x.Numero);
        }

        public override List<Disciplina> ObterRegistros()
        {
            return dataContext.Disciplinas;
        }

        public override AbstractValidator<Disciplina> ObterValidador()
        {
            return new ValidadorDisciplina();
        }

        private ValidationResult Validar(Disciplina registro)
        {
            var validator = ObterValidador();

            var resultadoValidacao = validator.Validate(registro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            var nomeEncontrado = ObterRegistros()
               .Select(x => x.Nome.ToUpper())
               .Contains(registro.Nome.ToUpper());

            if (nomeEncontrado && registro.Numero == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Já existe uma disciplina com este nome cadastrada no sistema!"));

            return resultadoValidacao;
        }

    }
}
