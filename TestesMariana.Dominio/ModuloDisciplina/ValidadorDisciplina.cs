using FluentValidation;
using System;
using System.Text.RegularExpressions;

namespace TestesMariana.Dominio.ModuloDisciplina
{
    public class ValidadorDisciplina : AbstractValidator<Disciplina>
    {
        public ValidadorDisciplina()
        {
            RuleFor(x => x.Nome)
                .NotNull().NotEmpty()
                .WithMessage("O campo 'Nome da disciplina' é obrigatório!");

            RuleFor(x => x.Nome.Length)
                .GreaterThan(4)
                .WithMessage("O campo 'Nome da disciplina' deve ter no mínimo 5 caracteres!");

            RuleFor(x => Regex.IsMatch(x.Nome, "[^a-zA-Z]+", RegexOptions.IgnoreCase))
            .NotEqual(true)
            .WithMessage("O campo 'Nome da disciplina' não deve conter números ou caracteres especiais!");
        }
    }
}
