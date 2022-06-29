using LocadoraVeiculos.Dominio.ModuloCliente;
using LocadoraVeiculos.Infra.BancoDados.Compartilhado;

namespace LocadoraVeiculos.Infra.BancoDados.ModuloCliente
{
    public class RepositorioClienteEmBancoDados :
        RepositorioBase<Cliente, MapeadorCliente>,
        IRepositorioCliente
    {
        protected override string sqlInserir =>
            @"INSERT INTO [TBCLIENTE]
                 (
		            [NOME],
                    [DOCUMENTO],
                    [EMAIL],
                    [TELEFONE],
                    [TIPO_CLIENTE],
                    [CNH],
                    [ESTADO],
                    [CIDADE],
                    [BAIRRO],
                    [RUA],
                    [NUMERO]
		         )
            VALUES
                (
		            @NOME, 
                    @DOCUMENTO, 
                    @EMAIL,
                    @TELEFONE,
                    @TIPO_CLIENTE,
                    @CNH,
                    @ESTADO,
                    @CIDADE,
                    @BAIRRO,
                    @RUA,
                    @NUMERO
			);SELECT SCOPE_IDENTITY();";

        protected override string sqlEditar =>
            @"UPDATE [TBCLIENTE]
                SET
		            [NOME] = @NOME,
                    [DOCUMENTO] = @DOCUMENTO,
                    [EMAIL] = @EMAIL,
                    [TELEFONE] = @TELEFONE,
                    [TIPO_CLIENTE] = @TIPO_CLIENTE,
                    [CNH] = @CNH,
                    [ESTADO] = @ESTADO,
                    [CIDADE] = @CIDADE,
                    [BAIRRO] = @BAIRRO,
                    [RUA] = @RUA,
                    [NUMERO] = @NUMERO
                WHERE
                    [ID] = @ID";

        protected override string sqlExcluir =>
            @"DELETE FROM [TBCLIENTE]
                WHERE [ID] = @ID";

        protected override string sqlSelecionarPorId =>
            @"SELECT 
	            [ID] CLIENTE_ID,
                [NOME] CLIENTE_NOME,
                [DOCUMENTO] CLIENTE_DOCUMENTO,
                [EMAIL] CLIENTE_EMAIL,
                [TELEFONE] CLIENTE_TELEFONE,
                [TIPO_CLIENTE] CLIENTE_TIPO_CLIENTE,
                [CNH] CLIENTE_CNH,
                [ESTADO] CLIENTE_ESTADO,
                [CIDADE] CLIENTE_CIDADE,
                [BAIRRO] CLIENTE_BAIRRO,
                [RUA] CLIENTE_RUA,
                [NUMERO] CLIENTE_NUMERO
            FROM
                [TBCLIENTE]
            WHERE
                [ID] = @ID";

        protected override string sqlSelecionarTodos =>
            @"SELECT 
	            [ID] CLIENTE_ID,
                [NOME] CLIENTE_NOME,
                [DOCUMENTO] CLIENTE_DOCUMENTO,
                [EMAIL] CLIENTE_EMAIL,
                [TELEFONE] CLIENTE_TELEFONE,
                [TIPO_CLIENTE] CLIENTE_TIPO_CLIENTE,
                [CNH] CLIENTE_CNH,
                [ESTADO] CLIENTE_ESTADO,
                [CIDADE] CLIENTE_CIDADE,
                [BAIRRO] CLIENTE_BAIRRO,
                [RUA] CLIENTE_RUA,
                [NUMERO] CLIENTE_NUMERO
            FROM
                [TBCLIENTE]";


    }
}