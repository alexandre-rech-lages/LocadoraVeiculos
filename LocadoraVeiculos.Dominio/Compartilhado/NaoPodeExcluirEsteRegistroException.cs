using System;

namespace LocadoraVeiculos.Dominio.Compartilhado
{
    public class NaoPodeExcluirEsteRegistroException : Exception
    {

        public NaoPodeExcluirEsteRegistroException(Exception ex) : base("", ex)
        {

        }
    }
}
