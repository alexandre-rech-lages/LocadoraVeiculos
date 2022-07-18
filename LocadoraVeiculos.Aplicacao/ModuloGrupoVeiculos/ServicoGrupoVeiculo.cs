using FluentResults;
using FluentValidation.Results;
using LocadoraVeiculos.Dominio.ModuloGrupoVeiculos;
using LocadoraVeiculos.Infra.BancoDados.ModuloGrupoVeiculos;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LocadoraVeiculos.Aplicacao.ModuloGrupoVeiculos
{
    public interface IServicoGrupoVeiculo
    {
        Result<GrupoVeiculo> Editar(GrupoVeiculo grupoVeiculo);
        Result Excluir(GrupoVeiculo grupoVeiculo);
        Result<GrupoVeiculo> Inserir(GrupoVeiculo grupoVeiculo);
        Result<GrupoVeiculo> SelecionarPorId(Guid id);
        Result<List<GrupoVeiculo>> SelecionarTodos();
    }

    public class ServicoGrupoVeiculo : IServicoGrupoVeiculo
    {
        private IRepositorioGrupoVeiculos repositorioGrupoVeiculo;

        public ServicoGrupoVeiculo(IRepositorioGrupoVeiculos repositorioGrupoVeiculos)
        {
            this.repositorioGrupoVeiculo = repositorioGrupoVeiculos;
        }

        public Result<GrupoVeiculo> Inserir(GrupoVeiculo grupoVeiculo)
        {
            Log.Logger.Debug("Tentando inserir Grupo de Veículo... {@gv}", grupoVeiculo);

            Result resultadoValidacao = ValidarGrupoVeiculo(grupoVeiculo);

            if (resultadoValidacao.IsFailed)
            {
                foreach (var erro in resultadoValidacao.Errors)
                {
                    Log.Logger.Warning("Falha ao tentar inserir o Grupo de Veículo {GrupoVeiculoId} - {Motivo}",
                       grupoVeiculo.Id, erro.Message);
                }

                return Result.Fail(resultadoValidacao.Errors);
            }

            try
            {
                repositorioGrupoVeiculo.Inserir(grupoVeiculo);

                Log.Logger.Information("Grupo de Veículo {GrupoVeiculoId} inserido com sucesso", grupoVeiculo.Id);

                return Result.Ok(grupoVeiculo);
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar inserir o Grupo de Veículo";

                Log.Logger.Error(ex, msgErro + "{GrupoVeiculoId}", grupoVeiculo.Id);

                return Result.Fail(msgErro);
            }
        }

        public Result<GrupoVeiculo> Editar(GrupoVeiculo grupoVeiculo)
        {
            Log.Logger.Debug("Tentando editar Grupo de Veículo... {@gv}", grupoVeiculo);

            Result resultadoValidacao = ValidarGrupoVeiculo(grupoVeiculo);

            if (resultadoValidacao.IsFailed)
            {
                foreach (var erro in resultadoValidacao.Errors)
                {
                    Log.Logger.Warning("Falha ao tentar editar o Grupo de Veículo {GrupoVeiculoId} - {Motivo}",
                       grupoVeiculo.Id, erro.Message);
                }

                return Result.Fail(resultadoValidacao.Errors);
            }

            try
            {
                repositorioGrupoVeiculo.Editar(grupoVeiculo);

                Log.Logger.Information("Grupo de Veículo {GrupoVeiculoId} editado com sucesso", grupoVeiculo.Id);

                return Result.Ok(grupoVeiculo);
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar editar o Grupo de Veículo";

                Log.Logger.Error(ex, msgErro + "{GrupoVeiculoId}", grupoVeiculo.Id);

                return Result.Fail(msgErro);
            }
        }

        public Result Excluir(GrupoVeiculo grupoVeiculo)
        {
            Log.Logger.Debug("Tentando excluir grupo de veículos... {@gv}", grupoVeiculo);

            try
            {
                repositorioGrupoVeiculo.Excluir(grupoVeiculo);

                Log.Logger.Information("Grupo de Veículos {GrupoVeiculoId} excluído com sucesso", grupoVeiculo.Id);

                return Result.Ok();
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar excluir o grupo de veículos";

                Log.Logger.Error(ex, msgErro + "{GrupoVeiculoId}", grupoVeiculo.Id);

                return Result.Fail(msgErro);
            }
        }

        public Result<GrupoVeiculo> SelecionarPorId(Guid id)
        {
            try
            {
                return Result.Ok(repositorioGrupoVeiculo.SelecionarPorId(id));
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar selecionar o Grupo de Veículos";

                Log.Logger.Error(ex, msgErro + "{GrupoVeiculoId}", id);

                return Result.Fail(msgErro);
            }
        }

        public Result<List<GrupoVeiculo>> SelecionarTodos()
        {
            try
            {
                return Result.Ok(repositorioGrupoVeiculo.SelecionarTodos());
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar selecionar todos os Grupos de Veículos";

                Log.Logger.Error(ex, msgErro);

                return Result.Fail(msgErro);
            }
        }


        #region Métodos privados
        private Result ValidarGrupoVeiculo(GrupoVeiculo grupoVeiculo)
        {
            var validador = new ValidadorGrupoVeiculo();

            var resultadoValidacao = validador.Validate(grupoVeiculo);

            List<Error> erros = new List<Error>(); //FluentResult

            foreach (ValidationFailure item in resultadoValidacao.Errors) //FluentValidation            
                erros.Add(new Error(item.ErrorMessage));

            if (NomeDuplicado(grupoVeiculo))
                erros.Add(new Error("Nome duplicado"));

            if (erros.Any())
                return Result.Fail(erros);

            return Result.Ok();
        }

        private bool NomeDuplicado(GrupoVeiculo grupoVeiculo)
        {
            var grupoVeiculoEncontrado = repositorioGrupoVeiculo.SelecionarGrupoVeiculoPorNome(grupoVeiculo.Nome);

            return grupoVeiculoEncontrado != null &&
                   grupoVeiculoEncontrado.Nome == grupoVeiculo.Nome &&
                   grupoVeiculoEncontrado.Id != grupoVeiculo.Id;
        }

        #endregion
    }
}
