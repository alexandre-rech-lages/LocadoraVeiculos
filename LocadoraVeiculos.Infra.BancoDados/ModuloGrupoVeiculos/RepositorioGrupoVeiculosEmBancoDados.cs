using LocadoraVeiculos.Dominio.ModuloGrupoVeiculos;
using LocadoraVeiculos.Infra.BancoDados.Compartilhado;
using System;

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

        public GrupoVeiculo SelecionarGrupoVeiculoPorNome(string nome)
        {
            throw new NotImplementedException();
        }
    }
}