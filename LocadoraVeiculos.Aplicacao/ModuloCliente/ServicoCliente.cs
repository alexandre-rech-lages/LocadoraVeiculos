using FluentResults;
using LocadoraVeiculos.Dominio.Compartilhado;
using LocadoraVeiculos.Dominio.ModuloCliente;
using LocadoraVeiculos.Infra.BancoDados.ModuloCliente;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LocadoraVeiculos.Aplicacao.ModuloCliente
{
    public interface IServicoCliente
    {
        Result<Cliente> Editar(Cliente cliente);
        Result Excluir(Cliente cliente);
        Result<Cliente> Inserir(Cliente cliente);
        Result<Cliente> SelecionarPorId(Guid id);
        Result<List<Cliente>> SelecionarTodos();
    }

    public class ServicoCliente : IServicoCliente
    {
        private IRepositorioCliente repositorioCliente;

        public ServicoCliente(IRepositorioCliente repositorioCliente)
        {
            this.repositorioCliente = repositorioCliente;
        }

        public Result<Cliente> Inserir(Cliente cliente)
        {
            Log.Logger.Debug("Tentando inserir cliente... {@c}", cliente);

            Result resultado = ValidarCliente(cliente);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            try
            {
                repositorioCliente.Inserir(cliente);

                Log.Logger.Information("Cliente {ClienteId} inserido com sucesso", cliente.Id);

                return Result.Ok(cliente);
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar inserir o Cliente";

                Log.Logger.Error(ex, msgErro + " {ClienteId}", cliente.Id);

                return Result.Fail(msgErro);
            }
        }

        public Result<Cliente> Editar(Cliente cliente)
        {
            Log.Logger.Debug("Tentando editar cliente... {@c}", cliente);

            Result resultado = ValidarCliente(cliente);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            try
            {
                repositorioCliente.Editar(cliente);

                Log.Logger.Information("Cliente {ClienteId} editado com sucesso", cliente.Id);

                return Result.Ok(cliente);
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar editar o Cliente";

                Log.Logger.Error(ex, msgErro + " {ClienteId}", cliente.Id);

                return Result.Fail(msgErro);
            }
        }

        public Result Excluir(Cliente cliente)
        {
            Log.Logger.Debug("Tentando excluir cliente... {@c}", cliente);

            try
            {
                repositorioCliente.Excluir(cliente);

                Log.Logger.Information("Cliente {ClienteId} editado com sucesso", cliente.Id);

                return Result.Ok();
            }
            catch (NaoPodeExcluirEsteRegistroException ex)
            {
                var msgErro = "O Cliente está relacionado com um condutor e não pode ser removido";

                Log.Logger.Error(ex, msgErro + " {ClienteId}", cliente.Id);

                return Result.Fail(msgErro);
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar excluir o Cliente";

                Log.Logger.Error(ex, msgErro + " {ClienteId}", cliente.Id);

                return Result.Fail(msgErro);
            }
        }

        public Result<List<Cliente>> SelecionarTodos()
        {
            try
            {
                return Result.Ok(repositorioCliente.SelecionarTodos());
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar selecionar todos os Clientes";

                Log.Logger.Error(ex, msgErro);

                return Result.Fail(new Error(msgErro));
            }
        }

        public Result<Cliente> SelecionarPorId(Guid id)
        {
            try
            {
                return Result.Ok(repositorioCliente.SelecionarPorId(id));
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar selecionar o Cliente";

                Log.Logger.Error(ex, msgErro + " {ClienteId}", id);

                return Result.Fail(msgErro);
            }
        }

        #region Métodos Privados

        private Result ValidarCliente(Cliente cliente)
        {
            ValidadorCliente validador = new ValidadorCliente();

            var resultadoValidacao = validador.Validate(cliente);

            var erros = new List<Error>();

            foreach (var validationFailure in resultadoValidacao.Errors)
            {
                Log.Logger.Warning(validationFailure.ErrorMessage);

                erros.Add(new Error(validationFailure.ErrorMessage));
            }

            if (erros.Any())
                return Result.Fail(erros);

            return Result.Ok();
        }

        #endregion

    }
}
