using LocadoraVeiculos.Dominio.Compartilhado;

namespace LocadoraVeiculos.Dominio.ModuloGrupoVeiculos
{
    public interface IRepositorioGrupoVeiculos : IRepositorio<GrupoVeiculo>
    {
        GrupoVeiculo SelecionarGrupoVeiculoPorNome(string nome);
    }
}