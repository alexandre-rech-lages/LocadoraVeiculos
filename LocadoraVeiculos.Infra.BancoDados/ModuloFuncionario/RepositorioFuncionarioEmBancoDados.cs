using LocadoraVeiculos.Dominio.ModuloFuncionario;
using LocadoraVeiculos.Infra.BancoDados.Compartilhado;
using System.Data.SqlClient;

namespace LocadoraVeiculos.Infra.BancoDados.ModuloFuncionario
{
    public class RepositorioFuncionarioEmBancoDados :
        RepositorioBase<Funcionario, MapeadorFuncionario>,
        IRepositorioFuncionario
    {
        protected override string sqlInserir =>
            @"INSERT INTO [TBFUNCIONARIO]
                   (
                   [ID],
                   [NOME],
                   [USUARIO],
                   [SENHA],
                   [DATA_ENTRADA],
                   [SALARIO],
                   [IS_ADMIN],
                   [ESTA_ATIVO]
                    )
             VALUES
               (
                   @ID,
                   @NOME,
                   @USUARIO,
                   @SENHA,
                   @DATA_ENTRADA,
                   @SALARIO,
                   @IS_ADMIN,
                   @ESTA_ATIVO
                );";

        protected override string sqlEditar =>
             @" UPDATE [TBFUNCIONARIO]
                    SET 
                   [NOME] = @NOME,
                   [USUARIO] = @USUARIO,
                   [SENHA] = @SENHA,
                   [DATA_ENTRADA] = @DATA_ENTRADA,
                   [SALARIO] = @SALARIO,
                   [IS_ADMIN] = @IS_ADMIN,
                   [ESTA_ATIVO] = @ESTA_ATIVO

                    WHERE [ID] = @ID";

        protected override string sqlExcluir =>
          @"DELETE FROM [TBFUNCIONARIO]
                WHERE [ID] = @ID";

        protected override string sqlSelecionarPorId =>
          @"SELECT 
                   [ID] FUNCIONARIO_ID,       
                   [NOME] FUNCIONARIO_NOME,
                   [USUARIO] FUNCIONARIO_USUARIO,
                   [SENHA] FUNCIONARIO_SENHA,
                   [DATA_ENTRADA] FUNCIONARIO_DATA_ENTRADA,
                   [SALARIO] FUNCIONARIO_SALARIO,
                   [IS_ADMIN] FUNCIONARIO_IS_ADMIN,
                   [ESTA_ATIVO] FUNCIONARIO_ESTA_ATIVO
            FROM
                [TBFUNCIONARIO]
            WHERE 
             [ID] = @ID";

        protected override string sqlSelecionarTodos =>
            @"SELECT 
                   [ID] FUNCIONARIO_ID,       
                   [NOME] FUNCIONARIO_NOME,
                   [USUARIO] FUNCIONARIO_USUARIO,
                   [SENHA] FUNCIONARIO_SENHA,
                   [DATA_ENTRADA] FUNCIONARIO_DATA_ENTRADA,
                   [SALARIO] FUNCIONARIO_SALARIO,
                   [IS_ADMIN] FUNCIONARIO_IS_ADMIN,
                   [ESTA_ATIVO] FUNCIONARIO_ESTA_ATIVO
            FROM
                [TBFUNCIONARIO]";

        private string sqlSelecionarPorNome =>
                @"SELECT 
                   [ID] FUNCIONARIO_ID,       
                   [NOME] FUNCIONARIO_NOME,
                   [USUARIO] FUNCIONARIO_USUARIO,
                   [SENHA] FUNCIONARIO_SENHA,
                   [DATA_ENTRADA] FUNCIONARIO_DATA_ENTRADA,
                   [SALARIO] FUNCIONARIO_SALARIO,
                   [IS_ADMIN] FUNCIONARIO_IS_ADMIN,
                   [ESTA_ATIVO] FUNCIONARIO_ESTA_ATIVO
            FROM
                [TBFUNCIONARIO]
            WHERE 
                [NOME] = @NOME";

        private string sqlSelecionarPorUsuario =>
            @"SELECT 
                   [ID] FUNCIONARIO_ID,       
                   [NOME] FUNCIONARIO_NOME,
                   [USUARIO] FUNCIONARIO_USUARIO,
                   [SENHA] FUNCIONARIO_SENHA,
                   [DATA_ENTRADA] FUNCIONARIO_DATA_ENTRADA,
                   [SALARIO] FUNCIONARIO_SALARIO,
                   [IS_ADMIN] FUNCIONARIO_IS_ADMIN,
                   [ESTA_ATIVO] FUNCIONARIO_ESTA_ATIVO
            FROM
                [TBFUNCIONARIO]
            WHERE 
                [USUARIO] = @USUARIO";

        public Funcionario SelecionarFuncionarioPorNome(string nome)
        {
            return SelecionarPorParametro(sqlSelecionarPorNome, new SqlParameter("NOME", nome));
        }

        public Funcionario SelecionarFuncionarioPorUsuario(string usuario)
        {
            return SelecionarPorParametro(sqlSelecionarPorUsuario, new SqlParameter("USUARIO", usuario));
        }

    }
}
