using FluentValidation;
using TestesMariana.Dominio.Compartilhado;

namespace TestesMariana.Dominio.ModuloQuestao
{
    public class ValidadorAlternativa : AbstractValidator<Alternativa>
    {
        public ValidadorAlternativa()
        {
            RuleFor(x => x.Descricao.RemoverEspacosEmBranco())
                .NotNull().NotEmpty()
                .WithMessage("O campo 'Descrição da alternativa' é obrigatório.");
        }
    }
}
