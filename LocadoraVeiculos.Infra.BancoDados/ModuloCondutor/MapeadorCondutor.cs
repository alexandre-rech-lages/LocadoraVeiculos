using LocadoraVeiculos.Dominio.ModuloCondutor;
using LocadoraVeiculos.Infra.BancoDados.Compartilhado;
using LocadoraVeiculos.Infra.BancoDados.ModuloCliente;
using System;
using System.Data.SqlClient;

namespace LocadoraVeiculos.Infra.BancoDados.ModuloCondutor
{
    public class MapeadorCondutor : MapeadorBase<Condutor>
    {
        public override void ConfigurarParametros(Condutor registro, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", registro.Id);
            comando.Parameters.AddWithValue("NOME", registro.Nome);
            comando.Parameters.AddWithValue("EMAIL", registro.Email);
            comando.Parameters.AddWithValue("TELEFONE", registro.Telefone);
            comando.Parameters.AddWithValue("CPF", registro.Cpf);
            comando.Parameters.AddWithValue("CNH", registro.Cnh);
            comando.Parameters.AddWithValue("DATA_VALIDADE_CNH", registro.DataValidadeCnh);
            comando.Parameters.AddWithValue("ID_CLIENTE", registro.Cliente.Id);
        }

        public override Condutor ConverterRegistro(SqlDataReader leitorRegistro)
        {
            var id = Guid.Parse(leitorRegistro["CONDUTOR_ID"].ToString());
            var nome = Convert.ToString(leitorRegistro["CONDUTOR_NOME"]);
            var email = Convert.ToString(leitorRegistro["CONDUTOR_EMAIL"]);
            var telefone = Convert.ToString(leitorRegistro["CONDUTOR_TELEFONE"]);
            var cpf = Convert.ToString(leitorRegistro["CONDUTOR_CPF"]);
            var cnh = Convert.ToString(leitorRegistro["CONDUTOR_CNH"]);
            var dataValidadeCnh = Convert.ToDateTime(leitorRegistro["CONDUTOR_DATA_VALIDADE_CNH"]);

            Condutor condutor = new Condutor()
            {
                Id = id,
                Nome = nome,
                Email = email,
                Telefone = telefone,
                Cpf = cpf,
                Cnh = cnh,
                DataValidadeCnh = dataValidadeCnh
            };

            var cliente = new MapeadorCliente().ConverterRegistro(leitorRegistro);
            condutor.Cliente = cliente;

            return condutor;
        }
    }
}