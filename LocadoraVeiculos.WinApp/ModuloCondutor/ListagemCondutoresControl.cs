using LocadoraVeiculos.Dominio.ModuloCondutor;
using LocadoraVeiculos.WinApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Locadora_Veiculos.WinApp.ModuloCondutor
{
    public partial class ListagemCondutoresControl : UserControl
    {
        public ListagemCondutoresControl()
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
                new DataGridViewTextBoxColumn { DataPropertyName = "Id", HeaderText = "Id", Visible=false},
                new DataGridViewTextBoxColumn { DataPropertyName = "Nome", HeaderText = "Nome"},
                new DataGridViewTextBoxColumn { DataPropertyName = "Telefone", HeaderText = "Telefone"},
                new DataGridViewTextBoxColumn { DataPropertyName = "Email", HeaderText = "Email"},
                new DataGridViewTextBoxColumn { DataPropertyName = "Cpf", HeaderText = "CPF"},
                new DataGridViewTextBoxColumn { DataPropertyName = "Cnh", HeaderText = "CNH"},
                new DataGridViewTextBoxColumn { DataPropertyName = "DataValidadeCnh", HeaderText = "Validade CNH"},
                new DataGridViewTextBoxColumn { DataPropertyName = "Cliente", HeaderText = "Cliente"},
            };

            return colunas;
        }

        public Guid ObtemIdCondutorSelecionado()
        {
            return grid.SelecionarId<Guid>();
        }

        public void AtualizarRegistros(List<Condutor> condutores)
        {
            grid.Rows.Clear();
            foreach (Condutor condutor in condutores)
            {
                grid.Rows.Add(condutor.Id, condutor.Nome, condutor.Telefone, condutor.Email,
                               condutor.Cpf, condutor.Cnh, condutor.DataValidadeCnh.ToShortDateString(),
                              condutor.Cliente.Nome);
            }
        }
    }
}