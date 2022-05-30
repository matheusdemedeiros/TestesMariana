using FluentValidation;

namespace TestesMariana.Dominio.ModuloTeste
{
    public class ValidadorTeste : AbstractValidator<Teste>
    {
        public ValidadorTeste()
        {
            RuleFor(x => x.Titulo)
                    .NotNull().NotEmpty()
                    .WithMessage("O campo 'Título do teste' é obrigatório!");
            
            RuleFor(x => x.Titulo.Length)
                    .GreaterThan(4)
                    .WithMessage("O campo 'Título do teste' deve ter no mínimo 5 caracteres!");

            RuleFor(x => x.Serie)
                    .NotNull().NotEmpty()
                    .WithMessage("O campo 'Série' é obrigatório!");
            
            RuleFor(x => x.Disciplina)
                    .NotNull()
                    .WithMessage("O campo 'Disciplina' é obrigatório!");
                        
            RuleFor(x => x.Questoes.Count).GreaterThan(0)
                    .WithMessage("O teste deve ter no mínimo uma questão!");
        }
    }
}
