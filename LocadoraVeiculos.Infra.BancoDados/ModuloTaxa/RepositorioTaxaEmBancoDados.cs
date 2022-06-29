using LocadoraVeiculos.Dominio.ModuloTaxa;
using LocadoraVeiculos.Infra.BancoDados.Compartilhado;

namespace LocadoraVeiculos.Infra.BancoDados.ModuloTaxa
{
    public class RepositorioTaxaEmBancoDados :
        RepositorioBase<Taxa, MapeadorTaxa>,
        IRepositorioTaxa
    {
        protected override string sqlInserir =>
            @"INSERT INTO [TBTAXA]
                (
                    [DESCRICAO],
                    [VALOR],
                    [TIPOCALCULO]
                )
            VALUES
                (
                    @DESCRICAO,
                    @VALOR,
                    @TIPOCALCULO
                ); SELECT SCOPE_IDENTITY();";

        protected override string sqlEditar =>
            @" UPDATE [TBTAXA]
                    SET 
                        [DESCRICAO] = @DESCRICAO,
                        [VALOR] = @VALOR,
                        [TIPOCALCULO] = @TIPOCALCULO
                    WHERE [ID] = @ID";

        protected override string sqlExcluir =>
            @"DELETE FROM [TBTAXA]
                WHERE [ID] = @ID";

        protected override string sqlSelecionarPorId =>
            @"SELECT 
                [ID] TAXA_ID,       
                [DESCRICAO] TAXA_DESCRICAO,
                [VALOR] TAXA_VALOR,
                [TIPOCALCULO] TAXA_TIPOCALCULO
            FROM
                [TBTAXA]
            WHERE 
                [ID] = @ID";

        protected override string sqlSelecionarTodos =>
            @"SELECT 
                [ID] TAXA_ID,       
                [DESCRICAO] TAXA_DESCRICAO,
                [VALOR] TAXA_VALOR,
                [TIPOCALCULO] TAXA_TIPOCALCULO
            FROM
                [TBTAXA]";

    }
}