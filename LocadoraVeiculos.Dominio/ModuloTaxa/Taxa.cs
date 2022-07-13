﻿using LocadoraVeiculos.Dominio.Compartilhado;
using System;

namespace LocadoraVeiculos.Dominio.ModuloTaxa
{
    public class Taxa : EntidadeBase<Taxa>
    {


        public string Descricao { get; set; }
        public Decimal Valor { get; set; }
        public TipoCalculo TipoCalculo { get; set; }





        public Taxa()
        {

        }

        public Taxa(string descricao, decimal valor, TipoCalculo tipoCalculo)
        {
            Descricao = descricao;
            Valor = valor;
            TipoCalculo = tipoCalculo;
        }



        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}", Descricao, Valor, TipoCalculo.GetDescription());
        }

        public override bool Equals(object obj)
        {
            Taxa t = obj as Taxa;

            if (t == null)
                return false;

            return
             t.Id.Equals(Id) &&
             t.Descricao.Equals(Descricao) &&
             t.Valor.Equals(Valor) &&
             t.TipoCalculo.Equals(TipoCalculo);

        }
    }
}