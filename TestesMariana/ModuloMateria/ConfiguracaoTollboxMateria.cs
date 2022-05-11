using TestesMariana.WinApp.Compartilhado;

namespace TestesMariana.WinApp.ModuloMateria
{
    public class ConfiguracaoTollboxMateria : ConfiguracaoToolboxBase
    {
        public override string TipoCadastro => "Cadastro de Matérias";

        public override string TooltipInserir => "Inserir uma nova Matéria";

        public override string TooltipEditar => "Editar uma Matéria existente";

        public override string TooltipExcluir => "Excluir uma Matéria existente";
    }
}
