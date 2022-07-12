using System;
using Taikandi;

namespace LocadoraVeiculos.Dominio.Compartilhado
{
    public abstract class EntidadeBase<T>
    {
        public Guid Id { get; set; }

        public EntidadeBase()
        {
            Id = SequentialGuid.NewGuid();
        }
    }
}
