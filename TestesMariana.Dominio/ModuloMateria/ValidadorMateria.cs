using FluentValidation;
using System.Text.RegularExpressions;

namespace TestesMariana.Dominio.ModuloMateria
{
    public class ValidadorMateria : AbstractValidator<Materia>
    {
        public ValidadorMateria()
        {
            RuleFor(x => x.Titulo)
                .NotNull().NotEmpty()
                .WithMessage("O campo 'Título da matéria' é obrigatório!");

            RuleFor(x => x.Titulo.Length)
                .GreaterThan(4)
                .WithMessage("O campo 'Título da matéria' deve ter no mínimo 5 caracteres!");

            RuleFor(x => Regex.IsMatch(x.Titulo, "[^a-zA-Z]+", RegexOptions.IgnoreCase))
                .NotEqual(true)
                .WithMessage("O campo 'Título da matéria' não deve conter números ou caracteres especiais!");

            RuleFor(x => x.Disciplina)
                .NotNull()
                .WithMessage("O campo 'Disciplina' é obrigatório!");

            RuleFor(x => x.Serie)
                .NotNull().NotEmpty()
                .WithMessage("O campo 'Série' é obrigatório!");
        }
    }
}
