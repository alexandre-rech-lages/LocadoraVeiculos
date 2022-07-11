using LocadoraVeiculos.Dominio.ModuloFuncionario;
using LocadoraVeiculos.Infra.BancoDados.Compartilhado;
using System;
using System.Data.SqlClient;

namespace LocadoraVeiculos.Infra.BancoDados.ModuloFuncionario
{
    public class MapeadorFuncionario : MapeadorBase<Funcionario>
    {
        public override void ConfigurarParametros(Funcionario registro, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", registro.Id);
            comando.Parameters.AddWithValue("NOME", registro.Nome);
            comando.Parameters.AddWithValue("USUARIO", registro.Usuario);
            comando.Parameters.AddWithValue("SENHA", registro.Senha);
            comando.Parameters.AddWithValue("DATA_ENTRADA", registro.DataAdmissao);
            comando.Parameters.AddWithValue("SALARIO", registro.Salario);
            comando.Parameters.AddWithValue("IS_ADMIN", registro.EhAdmin);
            comando.Parameters.AddWithValue("ESTA_ATIVO", registro.EstaAtivo);

        }

        public override Funcionario ConverterRegistro(SqlDataReader leitorRegistro)
        {
            var id = Guid.Parse(leitorRegistro["FUNCIONARIO_ID"].ToString());            
            var nome = Convert.ToString(leitorRegistro["FUNCIONARIO_NOME"]);
            var login = Convert.ToString(leitorRegistro["FUNCIONARIO_USUARIO"]);
            var senha = Convert.ToString(leitorRegistro["FUNCIONARIO_SENHA"]);
            var dataAdmissao = Convert.ToDateTime(leitorRegistro["FUNCIONARIO_DATA_ENTRADA"]);
            var salario = Convert.ToDecimal(leitorRegistro["FUNCIONARIO_SALARIO"]);
            var ehAdmin = Convert.ToBoolean(leitorRegistro["FUNCIONARIO_IS_ADMIN"]);
            var estaAtivo = Convert.ToBoolean(leitorRegistro["FUNCIONARIO_ESTA_ATIVO"]);

            var f = new Funcionario();
            f.Id = id;
            f.Nome = nome;
            f.Usuario = login;
            f.Senha = senha;
            f.DataAdmissao = dataAdmissao;
            f.Salario = salario;
            f.EhAdmin = ehAdmin;
            f.EstaAtivo = estaAtivo;

            return f;
        }
    }
}
