using LocadoraVeiculos.Dominio.ModuloGrupoVeiculos;
using LocadoraVeiculos.Infra.BancoDados.Compartilhado;

namespace LocadoraVeiculos.Infra.BancoDados.ModuloGrupoVeiculos
{
    public class RepositorioGrupoVeiculosEmBancoDados :
        RepositorioBase<GrupoVeiculos, MapeadorGrupoVeiculos>,
        IRepositorioGrupoVeiculos
    {
        protected override string sqlInserir =>
            @"INSERT INTO [TBGRUPOVEICULOS]
                (
                    [NOME]     
                )
            VALUES
                (
                    @NOME
                ); SELECT SCOPE_IDENTITY();";

        protected override string sqlEditar =>
            @" UPDATE [TBGRUPOVEICULOS]
                    SET 
                        [NOME] = @NOME
                    WHERE [ID] = @ID";

        protected override string sqlExcluir =>
            @"DELETE FROM [TBGRUPOVEICULOS]
                WHERE [ID] = @ID";

        protected override string sqlSelecionarPorId =>
            @"SELECT 
                [ID] GRUPOVEICULO_ID,       
                [NOME] GRUPOVEICULO_NOME
            FROM
                [TBGRUPOVEICULOS]
            WHERE 
             [ID] = @ID";

        protected override string sqlSelecionarTodos =>
            @"SELECT 
                [ID] GRUPOVEICULO_ID,       
                [NOME] GRUPOVEICULO_NOME
            FROM
                [TBGRUPOVEICULOS]";


    }
}