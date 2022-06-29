using LocadoraVeiculos.WinApp.Compartilhado;

namespace LocadoraVeiculos.WinApp.ModuloTaxas
{
    public class ConfiguracaoToolBoxTaxa : ConfiguracaoToolboxBase
    {
        public override string TipoCadastro => "Cadastro de Taxas";

        public override string TooltipInserir => "Inserir uma nova Taxa";

        public override string TooltipEditar => "Editar uma Taxa existente";

        public override string TooltipExcluir => "Excluir uma Taxa existente";
    }
}
