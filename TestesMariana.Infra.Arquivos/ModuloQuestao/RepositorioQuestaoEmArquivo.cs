using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using TestesMariana.Dominio.Compartilhado;
using TestesMariana.Dominio.ModuloQuestao;
using TestesMariana.Infra.Arquivos.Compartilhado;

namespace TestesMariana.Infra.Arquivos.ModuloQuestao
{
    public class RepositorioQuestaoEmArquivo : RepositorioEmArquivoBase<Questao>, IRepositorioQuestao
    {
        public RepositorioQuestaoEmArquivo(DataContext dataContext) : base(dataContext)
        {
            if (dataContext.Questoes.Count > 0)
                contador = dataContext.Questoes.Max(x => x.Numero);
        }

        public override ValidationResult Inserir(Questao novoRegistro)
        {
            var resultadoValidacao = Validar(novoRegistro);

            if (resultadoValidacao.IsValid)
            {
                novoRegistro.Numero = ++contador;

                novoRegistro.Materia.IncrementarQtdQuestoesRelacionadas();

                var registros = ObterRegistros();

                registros.Add(novoRegistro);
            }

            return resultadoValidacao;
        }

        public virtual ValidationResult Editar(Questao registro)
        {
            var resultadoValidacao = Validar(registro);

            if (resultadoValidacao.IsValid)
            {
                var registros = ObterRegistros();

                foreach (var questao in registros)
                {
                    if (questao.Numero == registro.Numero)
                    {
                        AtualizarMateriasRelacionadas(registro, questao);

                        questao.Atualizar(registro);

                        break;
                    }
                }
            }

            return resultadoValidacao;
        }

        public override ValidationResult Excluir(Questao registro)
        {
            var resultadoValidacao = new ValidationResult();

            var registros = ObterRegistros();

            registro.Materia.DecrementarQtdQuestoesRelacionadas();

            if (registros.Remove(registro) == false)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover a questão!"));

            return resultadoValidacao;
        }

        public override List<Questao> ObterRegistros()
        {
            return dataContext.Questoes;
        }

        public override AbstractValidator<Questao> ObterValidador()
        {
            return new ValidadorQuestao();
        }

        private ValidationResult Validar(Questao registro)
        {
            var validator = ObterValidador();

            var resultadoValidacao = validator.Validate(registro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            resultadoValidacao = ValidarEnunciado(registro);

            return resultadoValidacao;
        }

        private ValidationResult ValidarEnunciado(Questao registro)
        {
            bool enunciadoRegistrado = VerificarSeOhEnunciadoJaEstaRegistrado(registro);

            ValidationResult validacaoDeEnunciado = new ValidationResult();

            if (enunciadoRegistrado)
            {
                if (registro.Numero == 0)
                    validacaoDeEnunciado.Errors.Add(new ValidationFailure("", "Não foi possível inserir, pois já existe uma questão com este enunciado cadastrada no sistema!"));

                else if (ObterQuestaoPeloEnunciado(registro.Enunciado).Numero != registro.Numero)
                    validacaoDeEnunciado.Errors.Add(new ValidationFailure("", "Não foi possível editar, pois já existe uma questão com este enunciado cadastrada no sistema!"));
            }
            return validacaoDeEnunciado;
        }

        private bool VerificarSeOhEnunciadoJaEstaRegistrado(Questao registro)
        {
            return ObterRegistros()
                           .Exists(x => x.Enunciado.SaoIguais(registro.Enunciado));
        }

        private Questao ObterQuestaoPeloEnunciado(string enunciado)
        {
            return ObterRegistros()
               .Find(x => x.Enunciado.SaoIguais(enunciado));
        }

        private bool VerificaSeAsMateriasSaoIguais(Questao questao1, Questao questao2)
        {
            if (questao1.Materia == questao2.Materia)
                return true;

            return false;
        }

        private void AtualizarMateriasRelacionadas(Questao questaoNova, Questao questaoAntiga)
        {
            if (VerificaSeAsMateriasSaoIguais(questaoAntiga, questaoNova) == false)
            {
                questaoNova.Materia.IncrementarQtdQuestoesRelacionadas();
                questaoAntiga.Materia.DecrementarQtdQuestoesRelacionadas();
            }
        }
    }
}
