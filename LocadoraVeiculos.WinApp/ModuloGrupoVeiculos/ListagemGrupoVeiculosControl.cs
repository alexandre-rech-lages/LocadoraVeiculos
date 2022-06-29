﻿using LocadoraVeiculos.Dominio.ModuloGrupoVeiculos;
using LocadoraVeiculos.WinApp.Compartilhado;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LocadoraVeiculos.WinApp.ModuloGrupoVeiculos
{
    public partial class ListagemGrupoVeiculosControl : UserControl
    {
        public ListagemGrupoVeiculosControl()
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

                new DataGridViewTextBoxColumn { DataPropertyName = "Nome", HeaderText = "Nome"}
            };

            return colunas;
        }

        public void AtualizarRegistros(List<GrupoVeiculos> grupos)
        {
            grid.Rows.Clear();
            foreach (var g in grupos)
            {
                grid.Rows.Add(g.Id, g.Nome);
            }
        }

        public int ObtemIdGrupoVeiculosSelecionado()
        {
            return grid.SelecionarId<int>();
        }
    }
}