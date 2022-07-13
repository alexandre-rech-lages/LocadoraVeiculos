﻿using LocadoraVeiculos.Dominio.Compartilhado;
using LocadoraVeiculos.Dominio.ModuloCliente;
using LocadoraVeiculos.WinApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LocadoraVeiculos.WinApp.ModuloCliente
{
    public partial class ListagemClientesControl : UserControl
    {
        public ListagemClientesControl()
        {
            InitializeComponent();
            grid.ConfigurarGridZebrado();
            grid.ConfigurarGridSomenteLeitura();
            grid.Columns.AddRange(ObterColunas());
        }

        public DataGridViewColumn[] ObterColunas()
        {
            var colunas = new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { DataPropertyName = "Id", HeaderText = "Id"},
                new DataGridViewTextBoxColumn { DataPropertyName = "Nome", HeaderText = "Nome"},
                new DataGridViewTextBoxColumn { DataPropertyName = "Telefone", HeaderText = "Telefone"},
                new DataGridViewTextBoxColumn { DataPropertyName = "Email", HeaderText = "Email"},
                new DataGridViewTextBoxColumn { DataPropertyName = "tipo", HeaderText = "Tipo"},
                new DataGridViewTextBoxColumn { DataPropertyName = "Cpf", HeaderText = "CPF"},
                new DataGridViewTextBoxColumn { DataPropertyName = "Cnpj", HeaderText = "CNPJ"},
                new DataGridViewTextBoxColumn { DataPropertyName = "Cnh", HeaderText = "CNH"},
                new DataGridViewTextBoxColumn { DataPropertyName = "Estado", HeaderText = "Estado"},
                new DataGridViewTextBoxColumn { DataPropertyName = "Cidade", HeaderText = "Cidade"},
                new DataGridViewTextBoxColumn { DataPropertyName = "Bairro", HeaderText = "Bairro"},
                new DataGridViewTextBoxColumn { DataPropertyName = "Rua", HeaderText = "Rua"},
                new DataGridViewTextBoxColumn { DataPropertyName = "Numero", HeaderText = "Número"},
            };

            return colunas;
        }

        public Guid ObtemIdClienteSelecionado()
        {
            return grid.SelecionarId<Guid>();
        }

        public void AtualizarRegistros(List<Cliente> Clientes)
        {
            grid.Rows.Clear();
            foreach (Cliente cliente in Clientes)
            {
                var tipo = cliente.TipoCliente.GetDescription();

                grid.Rows.Add(cliente.Id, cliente.Nome, cliente.Telefone, cliente.Email,
                              tipo, cliente.Cpf, cliente.Cnpj,
                              cliente.Estado, cliente.Cidade, cliente.Bairro, cliente.Rua, cliente.Numero);
            }
        }
    }
}