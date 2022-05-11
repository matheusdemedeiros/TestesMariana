using FluentValidation;


namespace TestesMariana.Dominio.ModuloQuestao
{
    public  class ValidadorQuestao : AbstractValidator<Questao>
    {
        public ValidadorQuestao()
        {
            //RuleFor(x => x.Titulo)
            //    .NotNull().NotEmpty()
            //    .WithMessage("O campo 'Título da matéria' é obrigatório.");

            //RuleFor(x => x.Serie)
            //    .NotNull().NotEmpty()
            //    .WithMessage("O campo 'Série' é obrigatório.");

            //RuleFor(x => x.Disciplina)
            //    .NotNull()
            //    .WithMessage("O campo 'Disciplina' é obrigatório.");

        }


    }
}
