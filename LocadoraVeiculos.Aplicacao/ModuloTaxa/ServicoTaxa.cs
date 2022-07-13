using FluentValidation.Results;
using LocadoraVeiculos.Dominio.ModuloTaxa;
using LocadoraVeiculos.Infra.BancoDados.ModuloTaxa;
using System;

namespace LocadoraVeiculos.Aplicacao.ModuloTaxa
{
    public class ServicoTaxa
    {
        private RepositorioTaxaEmBancoDados repositorioTaxa;

        public ServicoTaxa(RepositorioTaxaEmBancoDados repositorioTaxa)
        {
            this.repositorioTaxa = repositorioTaxa;
        }

        public ValidationResult Inserir(Taxa arg)
        {
            return new ValidationResult();
        }

        public ValidationResult Editar(Taxa arg)
        {
            throw new NotImplementedException();
        }
    }
}
