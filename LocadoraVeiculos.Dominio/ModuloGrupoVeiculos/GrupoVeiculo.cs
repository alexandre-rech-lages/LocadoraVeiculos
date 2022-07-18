using LocadoraVeiculos.Dominio.Compartilhado;

namespace LocadoraVeiculos.Dominio.ModuloGrupoVeiculos
{
    public class GrupoVeiculo : EntidadeBase<GrupoVeiculo>
    {
        public GrupoVeiculo()
        {

        }

        public GrupoVeiculo(string nome)
        {
            Nome = nome;
        }

        public string Nome { get; set; }

        public override string ToString()
        {
            return Nome;
        }

        public override bool Equals(object obj)
        {
            GrupoVeiculo g = obj as GrupoVeiculo;

            if (g == null)
                return false;

            return
                g.Id.Equals(Id) &&
                g.Nome.Equals(Nome);
        }

        public GrupoVeiculo Clone()
        {
            return MemberwiseClone() as GrupoVeiculo;
        }
    }
}