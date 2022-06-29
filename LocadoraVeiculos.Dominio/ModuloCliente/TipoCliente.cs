using System.ComponentModel;

namespace LocadoraVeiculos.Dominio.ModuloCliente
{
    public enum TipoCliente
    {
        [Description("Pessoa física")]
        PessoaFisica,

        [Description("Pessoa jurídica")]
        PessoaJuridica,
    }
}