using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;
using TestesMariana.Dominio.Compartilhado;
using TestesMariana.Dominio.ModuloMateria;
using TestesMariana.Infra.Arquivos.Compartilhado;

namespace TestesMariana.Infra.Arquivos.ModuloMateria
{
    public class RepositorioMateriaEmArquivo : RepositorioEmArquivoBase<Materia>, IRepositorioMateria
    {
        public RepositorioMateriaEmArquivo(DataContext dataContext) : base(dataContext)
        {
            if (dataContext.Materias.Count > 0)
                contador = dataContext.Materias.Max(x => x.Numero);

        }

        public override ValidationResult Inserir(Materia novoRegistro)
        {
            var resultadoValidacao = Validar(novoRegistro);

            if (resultadoValidacao.IsValid)
            {
                novoRegistro.Numero = ++contador;

                novoRegistro.Disciplina.QtdMateriasRelacionadas++; ;

                var registros = ObterRegistros();

                registros.Add(novoRegistro);
            }

            return resultadoValidacao;
        }

        public virtual ValidationResult Editar(Materia registro)
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

        public override ValidationResult Excluir(Materia registro)
        {
            var resultadoValidacao = new ValidationResult();

            var registros = ObterRegistros();
            
            registro.Disciplina.QtdMateriasRelacionadas--;

            if (registros.Remove(registro) == false)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            return resultadoValidacao;
        }

        public override List<Materia> ObterRegistros()
        {
            return dataContext.Materias;
        }

        public override AbstractValidator<Materia> ObterValidador()
        {
            return new ValidadorMateria();
        }

        private ValidationResult Validar(Materia registro)
        {
            var validator = ObterValidador();

            var resultadoValidacao = validator.Validate(registro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            resultadoValidacao = ValidarTitulo(registro);

            return resultadoValidacao;
        }

        private ValidationResult ValidarTitulo(Materia registro)
        {
            bool tituloRegistrado = VerificarSeOhTituloJaEstaRegistrado(registro);

            ValidationResult validacaoDeTitulo = new ValidationResult();

            if (tituloRegistrado)
            {
                if (registro.Numero == 0)
                    validacaoDeTitulo.Errors.Add(new ValidationFailure("", "Não foi possível inserir, pois já existe uma matéria com este título cadastrada no sistema!"));

                else if (ObterMateriaPeloTitulo(registro.Titulo).Numero != registro.Numero)
                    validacaoDeTitulo.Errors.Add(new ValidationFailure("", "Não foi possível editar, pois já existe uma matéria com este título cadastrada no sistema!"));
            }
            return validacaoDeTitulo;
        }

        private bool VerificarSeOhTituloJaEstaRegistrado(Materia registro)
        {
            return ObterRegistros()
                             .Exists(x => x.Titulo.SaoIguais(registro.Titulo));

        }

        private Materia ObterMateriaPeloTitulo(string titulo)
        {
            return ObterRegistros()
               .Find(x => x.Titulo.SaoIguais(titulo));
        }
    }
}
