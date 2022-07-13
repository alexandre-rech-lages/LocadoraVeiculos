using FluentValidation.Results;
using LocadoraVeiculos.Dominio.ModuloCliente;
using LocadoraVeiculos.Dominio.ModuloCondutor;
using LocadoraVeiculos.WinApp;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Locadora_Veiculos.WinApp.ModuloCondutor
{
    public partial class TelaCadastroCondutorForm : Form
    {
        private Condutor condutor;
        private List<Cliente> clientes;

        public TelaCadastroCondutorForm(List<Cliente> clientes)
        {
            InitializeComponent();

            this.clientes = clientes;

            CarregarClientes();

            dateTimePickerDataValidadeCnh.MinDate = DateTime.Today;
        }

        public Condutor Condutor
        {
            get => condutor;
            set
            {
                condutor = value;

                if (condutor.Cliente != null)
                    comboBoxClientes.SelectedItem = condutor.Cliente;

                if (condutor.ClienteCondutor)
                {
                    PreencherClienteCondutor();
                    checkBoxClienteCondutor.Checked = true;
                }
                else
                {
                    txtNome.Text = condutor.Nome;
                    txtEmail.Text = condutor.Email;
                    txtTelefone.Text = condutor.Telefone;
                    txtCpf.Text = condutor.Cpf;
                }

                txtCnh.Text = condutor.Cnh;

                if (condutor.DataValidadeCnh.Date != new DateTime(1, 1, 1).Date)
                    dateTimePickerDataValidadeCnh.Value = condutor.DataValidadeCnh;
            }
        }

        public Func<Condutor, ValidationResult> GravarRegistro { get; set; }



        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparTodosOsCampos();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            condutor.Nome = txtNome.Text;
            condutor.Email = txtEmail.Text;
            condutor.Telefone = txtTelefone.Text;
            condutor.Cpf = txtCpf.Text;
            condutor.Cnh = txtCnh.Text;
            condutor.DataValidadeCnh = dateTimePickerDataValidadeCnh.Value;

            if (comboBoxClientes.SelectedIndex != -1)
                condutor.Cliente = (Cliente)comboBoxClientes.SelectedItem;

            var resultadoValidacao = GravarRegistro(condutor);

            if (resultadoValidacao.IsValid == false)
            {
                string erro = resultadoValidacao.Errors[0].ErrorMessage;

                TelaPrincipalForm.Instancia.AtualizarRodape(erro);

                DialogResult = DialogResult.None;
            }
        }

        private void comboBoxClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimparCamposDeIdentificacao();
            txtCnh.Clear();
            dateTimePickerDataValidadeCnh.Value = DateTime.Today;

            var clienteSelecionado = ObterClienteSelecionado();

            if (clienteSelecionado == null || comboBoxClientes.SelectedIndex == -1)
                DesabilitarClienteCondutor();
            else if (clienteSelecionado.TipoCliente == TipoCliente.PessoaFisica)
                HabilitarClienteCondutor();
            else
                DesabilitarClienteCondutor();
        }

        private void checkBoxClienteCondutor_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxClienteCondutor.Checked && ObterClienteSelecionado() != null)
            {
                PreencherClienteCondutor();
                DesabilitarCamposDeIdentificacao();
            }
            else
            {
                LimparCamposDeIdentificacao();
                HabilitarCamposDeIdentificacao();
            }
        }

        private void TelaCadastroCondutorForm_Load(object sender, EventArgs e)
        {
            TelaPrincipalForm.Instancia.AtualizarRodape("");
        }



        private void CarregarClientes()
        {
            if (clientes.Count > 0)
                foreach (var cliente in clientes)
                    comboBoxClientes.Items.Add(cliente);
        }

        private void PreencherClienteCondutor()
        {
            var cliente = ObterClienteSelecionado();

            txtNome.Text = cliente.Nome;
            txtEmail.Text = cliente.Email;
            txtTelefone.Text = cliente.Telefone;
            txtCpf.Text = cliente.Documento;
        }

        private Cliente ObterClienteSelecionado()
        {
            return (Cliente)comboBoxClientes.SelectedItem;
        }

        private void DesabilitarClienteCondutor()
        {
            checkBoxClienteCondutor.Checked = false;
            checkBoxClienteCondutor.Enabled = false;
        }

        private void HabilitarClienteCondutor()
        {
            checkBoxClienteCondutor.Checked = false;
            checkBoxClienteCondutor.Enabled = true;
        }

        private void LimparCamposDeIdentificacao()
        {
            txtNome.Clear();
            txtEmail.Clear();
            txtTelefone.Clear();
            txtCpf.Clear();
        }

        private void DesabilitarCamposDeIdentificacao()
        {
            txtNome.Enabled = false;
            txtEmail.Enabled = false;
            txtTelefone.Enabled = false;
            txtCpf.Enabled = false;
        }

        private void HabilitarCamposDeIdentificacao()
        {
            txtNome.Enabled = true;
            txtEmail.Enabled = true;
            txtTelefone.Enabled = true;
            txtCpf.Enabled = true;
        }

        private void LimparTodosOsCampos()
        {
            comboBoxClientes.SelectedIndex = -1;
            checkBoxClienteCondutor.Checked = false;
            LimparCamposDeIdentificacao();

            txtCnh.Clear();
            dateTimePickerDataValidadeCnh.Value = DateTime.Today;
        }


    }
}