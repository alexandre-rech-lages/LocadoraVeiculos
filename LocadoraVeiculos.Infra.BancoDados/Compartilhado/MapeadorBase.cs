using LocadoraVeiculos.Dominio.Compartilhado;
using System.Data.SqlClient;


namespace LocadoraVeiculos.Infra.BancoDados.Compartilhado
{
    public abstract class MapeadorBase<T> where T : EntidadeBase<T>
    {
        public abstract void ConfigurarParametros(T registro, SqlCommand comando);

        public abstract T ConverterRegistro(SqlDataReader leitorRegistro);
    }
}