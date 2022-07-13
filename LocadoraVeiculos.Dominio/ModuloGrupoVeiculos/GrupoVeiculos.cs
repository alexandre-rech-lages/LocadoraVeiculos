using LocadoraVeiculos.Dominio.Compartilhado;

namespace LocadoraVeiculos.Dominio.ModuloGrupoVeiculos
{
    public class GrupoVeiculos : EntidadeBase<GrupoVeiculos>
    {
        public GrupoVeiculos()
        {

        }

        public GrupoVeiculos(string nome)
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
            GrupoVeiculos g = obj as GrupoVeiculos;

            if (g == null)
                return false;

            return
                g.Id.Equals(Id) &&
                g.Nome.Equals(Nome);
        }

        public GrupoVeiculos Clone()
        {
            return MemberwiseClone() as GrupoVeiculos;
        }
    }
}