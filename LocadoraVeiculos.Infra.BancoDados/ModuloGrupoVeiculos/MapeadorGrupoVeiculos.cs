using LocadoraVeiculos.Dominio.ModuloGrupoVeiculos;
using LocadoraVeiculos.Infra.BancoDados.Compartilhado;
using System;
using System.Data.SqlClient;

namespace LocadoraVeiculos.Infra.BancoDados.ModuloGrupoVeiculos
{
    public class MapeadorGrupoVeiculos : MapeadorBase<GrupoVeiculos>
    {
        public override void ConfigurarParametros(GrupoVeiculos registro, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", registro.Id);
            comando.Parameters.AddWithValue("NOME", registro.Nome);
        }

        public override GrupoVeiculos ConverterRegistro(SqlDataReader leitorRegistro)
        {
            var id = Convert.ToInt32(leitorRegistro["GRUPOVEICULO_ID"]);
            var nome = Convert.ToString(leitorRegistro["GRUPOVEICULO_NOME"]);

            var g = new GrupoVeiculos();
            g.Id = id;
            g.Nome = nome;

            return g;
        }
    }
}