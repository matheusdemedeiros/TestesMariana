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

        public RepositorioDisciplinaEmArquivo(DataContext context) : base(context)
        {
            if (dataContext.Disciplinas.Count > 0)
                contador = dataContext.Disciplinas.Max(x => x.Numero);
        }

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

        public virtual ValidationResult Editar(Disciplina registro)
        {
            var resultadoValidacao = Validar(registro);

            if (resultadoValidacao.IsValid)
            {
                var registros = ObterRegistros();

                foreach (var item in registros)
                {
                    if (item.Numero == registro.Numero)
                    {
                        item.Atualizar(registro);
                        break;
                    }
                }
            }

            return resultadoValidacao;
        }

        public override ValidationResult Excluir(Disciplina registro)
        {
            var resultadovalidacao = new ValidationResult();

            var registros = ObterRegistros();

            if (registro.PodeExcluir)
            {
                if (registros.Remove(registro) == false)
                    resultadovalidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover a disciplina!"));
            }
            else
                resultadovalidacao.Errors.Add(new ValidationFailure("", "Não foi possível excluir a disciplina," +
                    " pois ela está associada a alguma(s) matéria(s)!"));

            return resultadovalidacao;
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

            resultadoValidacao = ValidarNome(registro);

            return resultadoValidacao;
        }

        private ValidationResult ValidarNome(Disciplina registro)
        {
            bool nomeRegistrado = VerificarSeOhNomeJaEstaRegistrado(registro);

            ValidationResult validacaoDeNome = new ValidationResult();

            if (nomeRegistrado)
            {
                if (registro.Numero == 0)
                    validacaoDeNome.Errors.Add(new ValidationFailure("", "Não foi possível inserir, pois já existe uma disciplina com este nome cadastrada no sistema!"));

                else if (ObterDisciplinaPeloNome(registro.Nome).Numero != registro.Numero)
                    validacaoDeNome.Errors.Add(new ValidationFailure("", "Não foi possível editar, pois já existe uma disciplina com este nome cadastrada no sistema!"));
            }
            return validacaoDeNome;
        }

        private bool VerificarSeOhNomeJaEstaRegistrado(Disciplina registro)
        {
            return ObterRegistros()
                           .Select(x => x.Nome.ToUpper())
                           .Contains(registro.Nome.ToUpper());
        }

        private Disciplina ObterDisciplinaPeloNome(string nome)
        {
            return ObterRegistros()
               .Find(x => x.Nome.ToUpper() == nome.ToUpper());
        }
    }
}
