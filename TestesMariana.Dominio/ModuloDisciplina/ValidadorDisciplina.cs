using FluentValidation;
using System;

namespace TestesMariana.Dominio.ModuloDisciplina
{
    public class ValidadorDisciplina : AbstractValidator<Disciplina>
    {
        public ValidadorDisciplina()
        {
            RuleFor(x => x.Nome)
                .NotNull().NotEmpty()
                .WithMessage("O campo 'Nome da disciplina' é obrigatório");
        }
    }
}
