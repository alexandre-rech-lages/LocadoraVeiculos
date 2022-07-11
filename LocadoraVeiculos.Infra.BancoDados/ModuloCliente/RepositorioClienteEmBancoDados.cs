using LocadoraVeiculos.Dominio.ModuloCliente;
using LocadoraVeiculos.Infra.BancoDados.Compartilhado;
using System;
using System.Data.SqlClient;

namespace LocadoraVeiculos.Infra.BancoDados.ModuloCliente
{
    public class RepositorioClienteEmBancoDados :
        RepositorioBase<Cliente, MapeadorCliente>,
        IRepositorioCliente
    {
        protected override string sqlInserir =>
            @"INSERT INTO [TBCLIENTE]
                 (
		            [ID],
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
		            @ID,
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
			)";

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


        public override void Excluir(Cliente registro)
        {
            try
            {
                base.Excluir(registro);
            }
            catch (SqlException ex)
            {
                if (ex.Message != null && ex.Message.Contains("FK_TBCondutor_TBCliente"))
                {
                    var msgErro = $"O Cliente {registro.Nome} [id:{registro.Id}] está relacionado com um condutor e não pode ser removido";
                    throw new RegistroNaoPodeSerExcluidoException(msgErro, ex);
                }

                throw;
            }
        }

        public class RegistroNaoPodeSerExcluidoException : ApplicationException
        {
            public RegistroNaoPodeSerExcluidoException(string mensagem , Exception exception) 
                : base("O Registro está referenciado com outro registro da aplicação e não pode ser removido", exception)
            {
            }
        }

    }
}