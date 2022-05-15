using TestesMariana.WinApp.Compartilhado;

namespace TestesMariana.WinApp.ModuloTeste
{
    public class ConfiguracaoToolboxTeste : ConfiguracaoToolboxBase
    {
        public override string TipoCadastro => "Cadastro de Testes escolares";

        public override string TooltipInserir => "Inserir um nova Teste";

        public override string TooltipEditar => "Editar um Teste existente";

        public override string TooltipExcluir => "Excluir um Teste existente";

        public virtual string TooltipGerarPDF => "Gerar PDF do Teste";

        public virtual string TooltipDuplicar => "Duplicar Teste";


        public override bool EditarHabilitado { get { return false; } }

        public override bool GerarPDFHabilitado { get { return true; } }

        public override bool DuplicarHabilitado { get { return true; } }


    }
}
