using System.Data.SqlClient;

namespace LocadoraVeiculos.Infra.BancoDados.Compartilhado
{
    public static class Db
    {
        private static string enderecoBanco =
        @"Data Source=(LOCALDB)\MSSQLLOCALDB;
              Initial Catalog=BreakpointLocadoraDb;
              Integrated Security=True";

        public static void ExecutarSql(string sql)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comando = new SqlCommand(sql, conexaoComBanco);

            conexaoComBanco.Open();
            comando.ExecuteNonQuery();
            conexaoComBanco.Close();
        }
    }
}