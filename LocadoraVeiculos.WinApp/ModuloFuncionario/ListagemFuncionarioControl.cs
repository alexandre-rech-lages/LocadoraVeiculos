using LocadoraVeiculos.Dominio.ModuloFuncionario;
using LocadoraVeiculos.WinApp.Compartilhado;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LocadoraVeiculos.WinApp.ModuloFuncionario
{
    public partial class ListagemFuncionarioControl : UserControl
    {
        public ListagemFuncionarioControl()
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
                new DataGridViewTextBoxColumn { DataPropertyName = "Login", HeaderText = "Login"},
                new DataGridViewTextBoxColumn { DataPropertyName = "DataAdmissao", HeaderText = "Data de admissão"},
                new DataGridViewTextBoxColumn { DataPropertyName = "Salario", HeaderText = "Salário"},
                new DataGridViewTextBoxColumn { DataPropertyName = "ehAdmin", HeaderText = "Admin"},
                new DataGridViewTextBoxColumn { DataPropertyName = "estaAtivo", HeaderText = "Está ativo"}
            };

            return colunas;
        }

        public int ObtemIdFuncionarioSelecionado()
        {
            return grid.SelecionarId<int>();
        }

        public void AtualizarRegistros(List<Funcionario> funcionarios)
        {
            grid.Rows.Clear();
            foreach (Funcionario funcionario in funcionarios)
            {
                var ehAdmin = funcionario.EhAdmin == true ? "Sim" : "Não";
                var estaAtivo = funcionario.EstaAtivo == true ? "Sim" : "Não";

                grid.Rows.Add(funcionario.Id, funcionario.Nome, funcionario.Usuario,
                    funcionario.DataAdmissao.ToShortDateString(), funcionario.Salario, ehAdmin, estaAtivo);
            }
        }
    }
}