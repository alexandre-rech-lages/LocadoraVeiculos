using LocadoraVeiculos.Infra.BancoDados.Compartilhado;

namespace LocadoraVeiculos.Infra.BancoDados.Tests
{
    public class BaseIntegrationTest
    {
        public BaseIntegrationTest()
        {
            Db.ExecutarSql("DELETE FROM TBCONDUTOR;");
            Db.ExecutarSql("DELETE FROM TBFUNCIONARIO;");
            Db.ExecutarSql("DELETE FROM TBGRUPOVEICULOS;");
            Db.ExecutarSql("DELETE FROM TBTAXA;");
            Db.ExecutarSql("DELETE FROM TBCLIENTE;");
        }
    }
}
