using FluentValidation;

namespace TestesMariana.Dominio.ModuloTeste
{
    public class ValidadorTeste : AbstractValidator<Teste>
    {
        public ValidadorTeste()
        {
            RuleFor(x => x.Titulo)
                    .NotNull().NotEmpty()
                    .WithMessage("O campo 'Disciplina' é obrigatório!");

            RuleFor(x => x.Serie)
                    .NotNull().NotEmpty()
                    .WithMessage("O campo 'Série' é obrigatório!");
            
            RuleFor(x => x.Titulo)
                    .NotNull().NotEmpty()
                    .WithMessage("O campo 'Título do teste' é obrigatório!");

            RuleFor(x => x.Questoes.Count).GreaterThan(0)
                    .WithMessage("O teste deve ter no mínimo uma questão!");
            
            RuleFor(x => x.QtdQuestoes).GreaterThan(0)
                    .WithMessage("Defina uma quantidade de questões para o teste!");

        }
    }
}
