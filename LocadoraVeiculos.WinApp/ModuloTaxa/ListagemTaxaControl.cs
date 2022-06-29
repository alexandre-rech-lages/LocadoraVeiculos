﻿using LocadoraVeiculos.Dominio.Compartilhado;
using LocadoraVeiculos.Dominio.ModuloTaxa;
using LocadoraVeiculos.WinApp.Compartilhado;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LocadoraVeiculos.WinApp.ModuloTaxas
{
    public partial class ListagemTaxaControl : UserControl
    {
        public ListagemTaxaControl()
        {
            InitializeComponent();
            grid.ConfigurarGridZebrado();
            grid.ConfigurarGridSomenteLeitura();
            grid.Columns.AddRange(ObterColunas());
        }

        private DataGridViewColumn[] ObterColunas()
        {
            var colunas = new DataGridViewColumn[] {

                new DataGridViewTextBoxColumn { DataPropertyName = "Id", HeaderText = "ID"},

                new DataGridViewTextBoxColumn { DataPropertyName = "Descricao", HeaderText = "Descrição"},

                new DataGridViewTextBoxColumn { DataPropertyName = "Valor", HeaderText = "Valor"},

                new DataGridViewTextBoxColumn { DataPropertyName = "TipoCalculo", HeaderText = "Tipo de Cálculo"}

            };

            return colunas;
        }

        public void AtualizarRegistros(List<Taxa> taxas)
        {
            grid.Rows.Clear();
            foreach (var t in taxas)
            {
                grid.Rows.Add(t.Id, t.Descricao, t.Valor, t.TipoCalculo.GetDescription());
            }
        }

        public int ObtemIdTaxaSelecionada()
        {
            return grid.SelecionarId<int>();
        }
    }
}