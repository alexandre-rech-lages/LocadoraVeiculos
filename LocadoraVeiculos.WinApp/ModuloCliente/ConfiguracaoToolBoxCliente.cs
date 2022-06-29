using LocadoraVeiculos.WinApp.Compartilhado;

namespace LocadoraVeiculos.WinApp.ModuloCliente
{
    public class ConfiguracaoToolBoxCliente : ConfiguracaoToolboxBase
    {
        public override string TipoCadastro => "Cadastro de Clientes";

        public override string TooltipInserir => "Inserir um novo Cliente";

        public override string TooltipEditar => "Editar um Cliente existente";

        public override string TooltipExcluir => "Excluir um Cliente existente";
    }
}