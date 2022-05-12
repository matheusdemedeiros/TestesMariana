using TestesMariana.WinApp.Compartilhado;

namespace TestesMariana.WinApp.ModuloQuestao
{
    public class ConfiguracaoToolboxQuestao : ConfiguracaoToolboxBase
    {
        public override string TipoCadastro => "Cadastro de Questões";

        public override string TooltipInserir => "Inserir uma nova Questão";

        public override string TooltipEditar => "Editar uma Questão existente";

        public override string TooltipExcluir => "Excluir uma Questão existente";
    }
}
