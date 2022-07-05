using FluentValidation.Results;
using LocadoraVeiculos.Dominio.ModuloGrupoVeiculos;
using LocadoraVeiculos.Infra.BancoDados.ModuloGrupoVeiculos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocadoraVeiculos.Aplicacao.ModuloGrupoVeiculos
{
    public class ServicoGrupoVeiculo
    {
        private RepositorioGrupoVeiculosEmBancoDados repositorioGrupoVeiculos;

        public ServicoGrupoVeiculo(RepositorioGrupoVeiculosEmBancoDados repositorioGrupoVeiculos)
        {
            this.repositorioGrupoVeiculos = repositorioGrupoVeiculos;
        }

        public ValidationResult Inserir(GrupoVeiculos arg)
        {
            var resultadoValidacao = ValidarGrupoVeiculo(arg);

            if (resultadoValidacao.IsValid)
                repositorioGrupoVeiculos.Inserir(arg);

            return resultadoValidacao;
        }

        private ValidationResult ValidarGrupoVeiculo(GrupoVeiculos arg)
        {
            ValidadorGrupoVeiculos validador = new ValidadorGrupoVeiculos();

            return validador.Validate(arg); 
        }

        public ValidationResult Editar(GrupoVeiculos arg)
        {
            var resultadoValidacao = ValidarGrupoVeiculo(arg);

            if (resultadoValidacao.IsValid)
                repositorioGrupoVeiculos.Editar(arg);

            return resultadoValidacao;
        }
    }
}
