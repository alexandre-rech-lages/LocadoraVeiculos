using LocadoraVeiculos.Dominio.ModuloGrupoVeiculos;
using LocadoraVeiculos.Infra.BancoDados.Compartilhado;
using System;
using System.Data.SqlClient;

namespace LocadoraVeiculos.Infra.BancoDados.ModuloGrupoVeiculos
{
    public class RepositorioGrupoVeiculosEmBancoDados :
        RepositorioBase<GrupoVeiculo, MapeadorGrupoVeiculos>,
        IRepositorioGrupoVeiculos
    {
        protected override string sqlInserir =>
            @"INSERT INTO [TBGRUPOVEICULOS]
                (
                    [ID],     
                    [NOME]     
                )
            VALUES
                (
                    @ID,
                    @NOME
                )";

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

        private string sqlSelecionarPorNome=>
           @"SELECT 
                [ID] GRUPOVEICULO_ID,       
                [NOME] GRUPOVEICULO_NOME
            FROM
                [TBGRUPOVEICULOS]
            WHERE 
                [NOME] = @NOME";

        public GrupoVeiculo SelecionarGrupoVeiculoPorNome(string nome)
        {
            return SelecionarPorParametro(sqlSelecionarPorNome, new SqlParameter("NOME", nome));
        }
    }
}