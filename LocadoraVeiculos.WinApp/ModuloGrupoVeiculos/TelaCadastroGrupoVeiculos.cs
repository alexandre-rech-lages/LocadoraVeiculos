using FluentResults;
using LocadoraVeiculos.Dominio.ModuloGrupoVeiculos;
using System;
using System.Windows.Forms;

namespace LocadoraVeiculos.WinApp.ModuloGrupoVeiculos
{
    public partial class TelaCadastroGrupoVeiculos : Form
    {
        private GrupoVeiculo grupoVeiculos;

        public TelaCadastroGrupoVeiculos()
        {
            InitializeComponent();
        }

        public Func<GrupoVeiculo, Result<GrupoVeiculo>> GravarRegistro { get; set; }

        public GrupoVeiculo Grupo
        {
            get
            {
                return grupoVeiculos;
            }
            set
            {
                grupoVeiculos = value;
                txtNome.Text = grupoVeiculos.Nome;
            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            grupoVeiculos.Nome = txtNome.Text;

            var resultadoValidacao = GravarRegistro(grupoVeiculos);

            if (resultadoValidacao.IsFailed)
            {
                string erro = resultadoValidacao.Errors[0].Message;

                if (erro.StartsWith("Falha no sistema"))
                {
                    MessageBox.Show(erro,
                    "Inserção de Grupo de Veículos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    TelaPrincipalForm.Instancia.AtualizarRodape(erro);

                    DialogResult = DialogResult.None;
                }
            }
        }
    }
}