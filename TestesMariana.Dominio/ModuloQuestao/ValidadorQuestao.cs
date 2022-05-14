using FluentValidation;


namespace TestesMariana.Dominio.ModuloQuestao
{
    public  class ValidadorQuestao : AbstractValidator<Questao>
    {
        public ValidadorQuestao()
        {
            RuleFor(x => x.Enunciado)
                .NotNull().NotEmpty()
                .WithMessage("O campo 'Enunciado da questão' é obrigatório!");

            RuleFor(x => x.TemAlternativaCorretaCadastrada)
                .Equal(true)
                .WithMessage("A questão deve ter no mínimo uma alternativa correta cadastrada!");

            RuleFor(x => x.Alternativas.Count).GreaterThan(1)
                .WithMessage("A questão deve ter no mínimo duas alternativa cadastradas!");

        }


    }
}
