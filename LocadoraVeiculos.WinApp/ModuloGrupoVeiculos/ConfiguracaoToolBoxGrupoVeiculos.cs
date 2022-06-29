using LocadoraVeiculos.WinApp.Compartilhado;

namespace LocadoraVeiculos.WinApp.ModuloGrupoVeiculos
{
    public class ConfiguracaoToolBoxGrupoVeiculos : ConfiguracaoToolboxBase
    {
        public override string TipoCadastro => "Cadastro de Grupos de Veículos";

        public override string TooltipInserir => "Inserir um novo Grupo de Veículos";

        public override string TooltipEditar => "Editar um Grupo de Veículos existente";

        public override string TooltipExcluir => "Excluir um Grupo de Veículos existente";
    }
}