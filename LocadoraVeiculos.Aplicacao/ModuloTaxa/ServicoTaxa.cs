using FluentValidation.Results;
using LocadoraVeiculos.Dominio.ModuloTaxa;
using LocadoraVeiculos.Infra.BancoDados.ModuloTaxa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
        }

        public ValidationResult Editar(Taxa arg)
        {
            throw new NotImplementedException();
        }
    }
}
