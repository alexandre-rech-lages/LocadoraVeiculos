using LocadoraVeiculos.Dominio.ModuloTaxa;
using LocadoraVeiculos.Infra.BancoDados.Compartilhado;
using System;
using System.Data.SqlClient;

namespace LocadoraVeiculos.Infra.BancoDados.ModuloTaxa
{
    public class MapeadorTaxa : MapeadorBase<Taxa>
    {
        public override void ConfigurarParametros(Taxa registro, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", registro.Id);
            comando.Parameters.AddWithValue("DESCRICAO", registro.Descricao);
            comando.Parameters.AddWithValue("VALOR", registro.Valor);
            comando.Parameters.AddWithValue("TIPOCALCULO", registro.TipoCalculo);
        }

        public override Taxa ConverterRegistro(SqlDataReader leitorRegistro)
        {
            var id = Guid.Parse(leitorRegistro["TAXA_ID"].ToString());
            var descricao = Convert.ToString(leitorRegistro["TAXA_DESCRICAO"]);
            var valor = Convert.ToDecimal(leitorRegistro["TAXA_VALOR"]);
            var tipoCalculo = Convert.ToInt32(leitorRegistro["TAXA_TIPOCALCULO"]);

            var t = new Taxa();
            t.Id = id;
            t.Descricao = descricao;
            t.Valor = valor;
            t.TipoCalculo = (TipoCalculo)tipoCalculo;

            return t;
        }
    }
}