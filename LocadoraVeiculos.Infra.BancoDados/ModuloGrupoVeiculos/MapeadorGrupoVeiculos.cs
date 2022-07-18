using LocadoraVeiculos.Dominio.ModuloGrupoVeiculos;
using LocadoraVeiculos.Infra.BancoDados.Compartilhado;
using System;
using System.Data.SqlClient;

namespace LocadoraVeiculos.Infra.BancoDados.ModuloGrupoVeiculos
{
    public class MapeadorGrupoVeiculos : MapeadorBase<GrupoVeiculo>
    {
        public override void ConfigurarParametros(GrupoVeiculo registro, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", registro.Id);
            comando.Parameters.AddWithValue("NOME", registro.Nome);
        }

        public override GrupoVeiculo ConverterRegistro(SqlDataReader leitorRegistro)
        {
            var id = Guid.Parse(leitorRegistro["GRUPOVEICULO_ID"].ToString());
            var nome = Convert.ToString(leitorRegistro["GRUPOVEICULO_NOME"]);

            var g = new GrupoVeiculo();
            g.Id = id;
            g.Nome = nome;

            return g;
        }
    }
}