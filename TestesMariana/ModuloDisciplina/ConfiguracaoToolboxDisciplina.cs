﻿using TestesMariana.WinApp.Compartilhado;

namespace TestesMariana.WinApp.ModuloDisciplina
{
    public class ConfiguracaoToolboxDisciplina : ConfiguracaoToolboxBase
    {
        public override string TipoCadastro => "Cadastro de Disciplinas";

        public override string TooltipInserir => "Inserir uma nova Disciplina";

        public override string TooltipEditar => "Editar uma Disciplina existente";

        public override string TooltipExcluir => "Excluir uma Disciplina existente";
    }
}
