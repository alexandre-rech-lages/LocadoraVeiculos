using Locadora_Veiculos.WinApp.ModuloCondutor;
using LocadoraVeiculos.WinApp.Compartilhado;
using LocadoraVeiculos.WinApp.ModuloCliente;
using LocadoraVeiculos.WinApp.ModuloFuncionario;
using LocadoraVeiculos.WinApp.ModuloGrupoVeiculos;
using LocadoraVeiculos.WinApp.ModuloTaxas;
using System;
using System.Windows.Forms;

namespace LocadoraVeiculos.WinApp
{
    public partial class TelaPrincipalForm : Form
    {
        private ControladorBase controlador;
        private IServiceLocator serviceLocator;

        public TelaPrincipalForm(IServiceLocator serviceLocator)
        {
            InitializeComponent();

            this.serviceLocator = serviceLocator;

            Instancia = this;

            AtualizarRodape(string.Empty);

            labelTipoCadastro.Text = string.Empty;
        }

        public static TelaPrincipalForm Instancia
        {
            get;
            private set;
        }

        private void clientesMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarTelaPrincipal(serviceLocator.Get<ControladorCliente>());
        }

        private void funcionariosMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarTelaPrincipal(serviceLocator.Get<ControladorFuncionario>());
        }

        private void grupoVeiculosMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarTelaPrincipal(serviceLocator.Get<ControladorGrupoVeiculos>());
        }

        private void taxasMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarTelaPrincipal(serviceLocator.Get<ControladorTaxa>());
        }

        private void condutoresMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarTelaPrincipal(serviceLocator.Get<ControladorCondutor>());
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            controlador.Inserir();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            controlador.Editar();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            controlador.Excluir();
        }

        private void ConfigurarBotoes(ConfiguracaoToolboxBase configuracao)
        {
            btnInserir.Visible = configuracao.InserirHabilitado;
            btnEditar.Visible = configuracao.EditarHabilitado;
            btnExcluir.Visible = configuracao.ExcluirHabilitado;
        }

        private void ConfigurarTooltips(ConfiguracaoToolboxBase configuracao)
        {
            btnInserir.ToolTipText = configuracao.TooltipInserir;
            btnEditar.ToolTipText = configuracao.TooltipEditar;
            btnExcluir.ToolTipText = configuracao.TooltipExcluir;
        }

        private void ConfigurarTelaPrincipal(ControladorBase controlador)
        {
            this.controlador = controlador;

            ConfigurarToolbox();

            ConfigurarListagem();
        }

        public void AtualizarRodape(string mensagem)
        {
            labelRodape.Text = mensagem;
        }

        private void ConfigurarToolbox()
        {
            ConfiguracaoToolboxBase configuracao = controlador.ObtemConfiguracaoToolbox();

            if (configuracao != null)
            {
                toolbox.Enabled = true;

                labelTipoCadastro.Text = configuracao.TipoCadastro;

                ConfigurarTooltips(configuracao);

                ConfigurarBotoes(configuracao);
            }
        }

        private void ConfigurarListagem()
        {
            AtualizarRodape("");

            var listagemControl = controlador.ObtemListagem();

            panelRegistros.Controls.Clear();

            listagemControl.Dock = DockStyle.Fill;

            panelRegistros.Controls.Add(listagemControl);
        }
    }
}