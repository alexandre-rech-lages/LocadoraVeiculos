using LocadoraVeiculos.Infra.BancoDados.Compartilhado;

namespace LocadoraVeiculos.Infra.BancoDados.Tests
{
    public class BaseIntegrationTest
    {
        public BaseIntegrationTest()
        {
            //Db.ExecutarSql("DELETE FROM TBCONDUTOR; DBCC CHECKIDENT (TBCONDUTOR, RESEED, 0)");
            Db.ExecutarSql("DELETE FROM TBFUNCIONARIO; DBCC CHECKIDENT (TBFUNCIONARIO, RESEED, 0)");
            Db.ExecutarSql("DELETE FROM TBGRUPOVEICULOS; DBCC CHECKIDENT (TBGRUPOVEICULOS, RESEED, 0)");
            Db.ExecutarSql("DELETE FROM TBTAXA; DBCC CHECKIDENT (TBTAXA, RESEED, 0)");
            Db.ExecutarSql("DELETE FROM TBCLIENTE; DBCC CHECKIDENT (TBCLIENTE, RESEED, 0)");
        }
    }
}
