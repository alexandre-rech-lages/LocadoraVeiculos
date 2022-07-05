using FluentValidation.Results;
using LocadoraVeiculos.Dominio.ModuloCliente;
using LocadoraVeiculos.Infra.BancoDados.ModuloCliente;
using System;
using static LocadoraVeiculos.Infra.BancoDados.ModuloCliente.RepositorioClienteEmBancoDados;

namespace LocadoraVeiculos.Aplicacao.ModuloCliente
{
    public class ServicoCliente
    {
        private RepositorioClienteEmBancoDados repositorioCliente;

        public ServicoCliente(RepositorioClienteEmBancoDados repositorioCliente)
        {
            this.repositorioCliente = repositorioCliente;
        }

        public ValidationResult Inserir(Cliente arg)
        {
            var resultadoValidacao = ValidarCliente(arg);

            if (resultadoValidacao.IsValid)
                repositorioCliente.Editar(arg);

            return resultadoValidacao;
        }

        public ValidationResult Editar(Cliente arg)
        {
            var resultadoValidacao = ValidarCliente(arg);

            if (resultadoValidacao.IsValid)
                repositorioCliente.Editar(arg);

            return resultadoValidacao;
        }

        private ValidationResult ValidarCliente(Cliente arg)
        {
            ValidadorCliente validador = new ValidadorCliente();

            var resultadoValidacao = validador.Validate(arg);

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Cliente arg)
        {
            ValidationResult resultadoValidacao = new ValidationResult();

            try
            {
                repositorioCliente.Excluir(arg);
            }
            catch (RegistroNaoPodeSerExcluidoException ex)
            {
                resultadoValidacao.Errors.Add(new ValidationFailure("", ex.Message));
            }            
            catch (Exception)
            {
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Erro desconhecido"));
            }

            return resultadoValidacao;
        }

    }
}
