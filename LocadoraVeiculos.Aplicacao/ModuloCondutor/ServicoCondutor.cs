using FluentResults;
using LocadoraVeiculos.Dominio.Compartilhado;
using LocadoraVeiculos.Dominio.ModuloCondutor;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LocadoraVeiculos.Aplicacao.ModuloCondutor
{
    public class ServicoCondutor
    {
        private IRepositorioCondutor repositorioCondutor;

        public ServicoCondutor(IRepositorioCondutor repositorioCondutor)
        {
            this.repositorioCondutor = repositorioCondutor;
        }

        public Result<Condutor> Inserir(Condutor condutor)
        {
            Log.Logger.Debug("Tentando inserir condutor... {@c}", condutor);

            Result resultado = ValidarCondutor(condutor);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            try
            {
                repositorioCondutor.Inserir(condutor);

                Log.Logger.Information("Condutor {CondutorId} inserido com sucesso", condutor.Id);

                return Result.Ok(condutor);
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar inserir o Condutor";

                Log.Logger.Error(ex, msgErro + " {CondutorId}", condutor.Id);

                return Result.Fail(msgErro);
            }
        }

        public Result<Condutor> Editar(Condutor condutor)
        {
            Log.Logger.Debug("Tentando editar condutor... {@c}", condutor);

            Result resultado = ValidarCondutor(condutor);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            try
            {
                repositorioCondutor.Editar(condutor);

                Log.Logger.Information("Condutor {CondutorId} editado com sucesso", condutor.Id);

                return Result.Ok(condutor);
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar editar o Condutor";

                Log.Logger.Error(ex, msgErro + " {CondutorId}", condutor.Id);

                return Result.Fail(msgErro);
            }
        }

        public Result Excluir(Condutor condutor)
        {
            Log.Logger.Debug("Tentando excluir condutor... {@c}", condutor);

            try
            {
                repositorioCondutor.Excluir(condutor);

                Log.Logger.Information("Condutor {CondutorId} editado com sucesso", condutor.Id);

                return Result.Ok();
            }
            catch (NaoPodeExcluirEsteRegistroException ex)
            {
                var msgErro = "O Condutor está relacionado com uma locação e não pode ser removido";

                Log.Logger.Error(ex, msgErro + " {CondutorId}", condutor.Id);

                return Result.Fail(msgErro);
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar excluir o Condutor";

                Log.Logger.Error(ex, msgErro + " {CondutorId}", condutor.Id);

                return Result.Fail(msgErro);
            }
        }

        public Result<List<Condutor>> SelecionarTodos()
        {
            try
            {
                return Result.Ok(repositorioCondutor.SelecionarTodos());
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar selecionar todos os Condutores";

                Log.Logger.Error(ex, msgErro);

                return Result.Fail(msgErro);
            }
        }

        public Result<Condutor> SelecionarPorId(Guid id)
        {
            try
            {
                return Result.Ok(repositorioCondutor.SelecionarPorId(id));
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar selecionar o Condutor";

                Log.Logger.Error(ex, msgErro + " {CondutorId}", id);

                return Result.Fail(msgErro);
            }
        }

        #region Métodos Privados

        private Result ValidarCondutor(Condutor condutor)
        {
            ValidadorCondutor validador = new ValidadorCondutor();

            var resultadoValidacao = validador.Validate(condutor);

            var erros = new List<Error>();

            foreach (var validationFailure in resultadoValidacao.Errors)
            {
                Log.Logger.Warning(validationFailure.ErrorMessage);

                erros.Add(new Error(validationFailure.ErrorMessage));
            }

            if (CpfDuplicado(condutor))
                erros.Add(new Error("CPF já está cadastrado como condutor!"));

            if (CnhDuplicada(condutor))
                erros.Add(new Error("CNH já está cadastrada como condutor!"));

            if (erros.Any())
                return Result.Fail(erros);

            return Result.Ok();
        }

        private bool CnhDuplicada(Condutor condutor)
        {
            var condutorEncontrado = repositorioCondutor.SelecionarCondutorPorCNH(condutor.Cnh);

            return condutorEncontrado != null &&
                   condutorEncontrado.Cnh == condutor.Cnh &&
                   condutorEncontrado.Id != condutor.Id;
        }

        private bool CpfDuplicado(Condutor condutor)
        {
            var condutorEncontrado = repositorioCondutor.SelecionarCondutorPorCPF(condutor.Cpf);

            return condutorEncontrado != null &&
                   condutorEncontrado.Cpf == condutor.Cpf &&
                   condutorEncontrado.Id != condutor.Id;
        }

        #endregion
    }
}
