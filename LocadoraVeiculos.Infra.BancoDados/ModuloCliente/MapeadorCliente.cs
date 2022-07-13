using LocadoraVeiculos.Dominio.ModuloCliente;
using LocadoraVeiculos.Infra.BancoDados.Compartilhado;
using System;
using System.Data.SqlClient;

namespace LocadoraVeiculos.Infra.BancoDados.ModuloCliente
{
    public class MapeadorCliente : MapeadorBase<Cliente>
    {
        public override void ConfigurarParametros(Cliente registro, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", registro.Id);
            comando.Parameters.AddWithValue("NOME", registro.Nome);
            comando.Parameters.AddWithValue("EMAIL", registro.Email);
            comando.Parameters.AddWithValue("TELEFONE", registro.Telefone);
            comando.Parameters.AddWithValue("ESTADO", registro.Estado);
            comando.Parameters.AddWithValue("CIDADE", registro.Cidade);
            comando.Parameters.AddWithValue("BAIRRO", registro.Bairro);
            comando.Parameters.AddWithValue("RUA", registro.Rua);
            comando.Parameters.AddWithValue("NUMERO", registro.Numero);
            comando.Parameters.AddWithValue("TIPO_CLIENTE", registro.TipoCliente);

            if (registro.TipoCliente == TipoCliente.PessoaJuridica)
                comando.Parameters.AddWithValue("DOCUMENTO", registro.Cnpj);
            else
                comando.Parameters.AddWithValue("DOCUMENTO", registro.Cpf);
        }

        public override Cliente ConverterRegistro(SqlDataReader leitorRegistro)
        {
            var id = Guid.Parse(leitorRegistro["CLIENTE_ID"].ToString());
            var nome = Convert.ToString(leitorRegistro["CLIENTE_NOME"]);
            var email = Convert.ToString(leitorRegistro["CLIENTE_EMAIL"]);
            var telefone = Convert.ToString(leitorRegistro["CLIENTE_TELEFONE"]);
            var estado = Convert.ToString(leitorRegistro["CLIENTE_ESTADO"]);
            var cidade = Convert.ToString(leitorRegistro["CLIENTE_CIDADE"]);
            var bairro = Convert.ToString(leitorRegistro["CLIENTE_BAIRRO"]);
            var rua = Convert.ToString(leitorRegistro["CLIENTE_RUA"]);
            var numero = Convert.ToInt32(leitorRegistro["CLIENTE_NUMERO"]);
            var tipo = Convert.ToInt32(leitorRegistro["CLIENTE_TIPO_CLIENTE"]);
            var documento = Convert.ToString(leitorRegistro["CLIENTE_DOCUMENTO"]);

            Cliente cliente = new Cliente()
            {
                Id = id,
                Nome = nome,
                Email = email,
                Telefone = telefone,
                Estado = estado,
                Cidade = cidade,
                Rua = rua,
                Bairro = bairro,
                Numero = numero
            };

            ConfigurarTipoCliente(tipo, documento, cliente);

            return cliente;
        }

        private void ConfigurarTipoCliente(int tipo, string documento, Cliente cliente)
        {
            if (tipo == 0)
            {
                cliente.TipoCliente = TipoCliente.PessoaFisica;
                cliente.Cpf = documento;
            }
            else if (tipo == 1)
            {
                cliente.TipoCliente = TipoCliente.PessoaJuridica;
                cliente.Cnpj = documento;
            }
        }
    }
}