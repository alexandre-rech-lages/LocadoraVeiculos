using FluentResults;
using FluentValidation.Results;
using LocadoraVeiculos.Dominio.ModuloTaxa;
using LocadoraVeiculos.Infra.BancoDados.ModuloTaxa;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LocadoraVeiculos.Aplicacao.ModuloTaxa
{
    public class ServicoTaxa
    {
        private RepositorioTaxaEmBancoDados repositorioTaxa;

        public ServicoTaxa(RepositorioTaxaEmBancoDados repositorioTaxa)
        {
            this.repositorioTaxa = repositorioTaxa;
        }

        public Result<Taxa> Inserir(Taxa taxa)
        {
            Log.Logger.Debug("Tentando inserir taxa... {@t}", taxa);

            Result resultadoValidacao = ValidarTaxa(taxa);

            if (resultadoValidacao.IsFailed)
            {
                foreach (var erro in resultadoValidacao.Errors)
                {
                    Log.Logger.Warning("Falha ao tentar inserir o Taxa {TaxaId} - {Motivo}",
                       taxa.Id, erro.Message);
                }

                return Result.Fail(resultadoValidacao.Errors);
            }

            try
            {
                repositorioTaxa.Inserir(taxa);

                Log.Logger.Information("Taxa {TaxaId} inserido com sucesso", taxa.Id);

                return Result.Ok(taxa);
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar inserir a taxa";

                Log.Logger.Error(ex, msgErro + "{TaxaId}", taxa.Id);

                return Result.Fail(msgErro);
            }
        }

        public Result<Taxa> Editar(Taxa taxa)
        {
            Log.Logger.Debug("Tentando editar taxa... {@t}", taxa);

            Result resultadoValidacao = ValidarTaxa(taxa);

            if (resultadoValidacao.IsFailed)
            {
                foreach (var erro in resultadoValidacao.Errors)
                {
                    Log.Logger.Warning("Falha ao tentar editar a Taxa {TaxaId} - {Motivo}",
                       taxa.Id, erro.Message);
                }

                return Result.Fail(resultadoValidacao.Errors);
            }

            try
            {
                repositorioTaxa.Editar(taxa);

                Log.Logger.Information("Taxa {TaxaId} editado com sucesso", taxa.Id);

                return Result.Ok(taxa);
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar editar a taxa";

                Log.Logger.Error(ex, msgErro + "{TaxaId}", taxa.Id);

                return Result.Fail(msgErro);
            }
        }

        public Result Excluir(Taxa taxa)
        {
            Log.Logger.Debug("Tentando excluir taxa... {@t}", taxa);

            try
            {
                repositorioTaxa.Excluir(taxa);

                Log.Logger.Information("Taxa {TaxaId} excluído com sucesso", taxa.Id);

                return Result.Ok();
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar excluir a taxa";

                Log.Logger.Error(ex, msgErro + "{TaxaId}", taxa.Id);

                return Result.Fail(msgErro);
            }
        }

        public Result<List<Taxa>> SelecionarTodos()
        {
            try
            {
                return Result.Ok(repositorioTaxa.SelecionarTodos());
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar selecionar todas as taxas";

                Log.Logger.Error(ex, msgErro);

                return Result.Fail(msgErro);
            }
        }

        public Result<Taxa> SelecionarPorId(Guid id)
        {
            try
            {
                return Result.Ok(repositorioTaxa.SelecionarPorId(id));
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar selecionar a taxa";

                Log.Logger.Error(ex, msgErro + "{TaxaId}", id);

                return Result.Fail(msgErro);
            }
        }

        #region Métodos privados
        private Result ValidarTaxa(Taxa taxa)
        {
            var validador = new ValidadorTaxa();

            var resultadoValidacao = validador.Validate(taxa);

            List<Error> erros = new List<Error>(); //FluentResult

            foreach (ValidationFailure item in resultadoValidacao.Errors) //FluentValidation            
                erros.Add(new Error(item.ErrorMessage));

            if (erros.Any())
                return Result.Fail(erros);

            return Result.Ok();
        }

        #endregion
    }
}
