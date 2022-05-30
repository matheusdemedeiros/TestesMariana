using TestesMariana.WinApp.Compartilhado;

namespace TestesMariana.WinApp.ModuloTeste
{
    public class ConfiguracaoToolboxTeste : ConfiguracaoToolboxBase
    {
        public override string TipoCadastro => "Cadastro de Testes escolares";

        public override string TooltipInserir => "Inserir um novo Teste";

        public override string TooltipEditar => "Editar um Teste existente";

        public override string TooltipExcluir => "Excluir um Teste existente";

        public override string TooltipGerarPDF => "Gerar PDF do Teste";

        public override string TooltipDuplicar => "Duplicar Teste";
        
        public override string TooltipGabarito => "Gerar PDF do Gabarito";

        public override string TooltipVisualizarDetalhadamente => "Visualizar Teste detalhadamente";


        public override bool EditarHabilitado { get { return false; } }
        
        public override bool PDFGabaritoHabilitado { get { return true; } }

        public override bool GerarPDFHabilitado { get { return true; } }

        public override bool DuplicarHabilitado { get { return true; } }

        public override bool VisualizarDetalhadamenteHabilitado { get { return true; } }
    }
}
