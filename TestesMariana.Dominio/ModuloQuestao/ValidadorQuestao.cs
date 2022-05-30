using FluentValidation;

namespace TestesMariana.Dominio.ModuloQuestao
{
    public  class ValidadorQuestao : AbstractValidator<Questao>
    {
        public ValidadorQuestao()
        {
            RuleFor(x => x.Disciplina)
                .NotNull()
                .WithMessage("O campo 'Disciplina' é obrigatório!");
            
            RuleFor(x => x.Materia)
                .NotNull()
                .WithMessage("O campo 'Matéria' é obrigatório!");
            
            RuleFor(x => x.Enunciado)
                .NotNull().NotEmpty()
                .WithMessage("O campo 'Enunciado da questão' é obrigatório!");
            
            RuleFor(x => x.Enunciado.Length)
                .GreaterThan(4)
                .WithMessage("O campo 'Enunciado da questão' deve ter no mínimo 5 caracteres!");
            
            RuleFor(x => x.Alternativas.Count).GreaterThan(1)
                .WithMessage("A questão deve ter no mínimo duas alternativas cadastradas!");

            RuleFor(x => x.TemAlternativaCorretaCadastrada)
                .Equal(true)
                .WithMessage("A questão deve ter no mínimo uma alternativa correta cadastrada!");
        }
    }
}
