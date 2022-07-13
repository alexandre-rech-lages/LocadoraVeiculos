using FluentValidation.Results;
using LocadoraVeiculos.Dominio.ModuloFuncionario;
using System;
using System.Windows.Forms;

namespace LocadoraVeiculos.WinApp.ModuloFuncionario
{
    public partial class TelaCadastroFuncionarioForm : Form
    {
        private Funcionario funcionario;

        public TelaCadastroFuncionarioForm()
        {
            InitializeComponent();
        }

        public Funcionario Funcionario
        {
            get => funcionario;
            set
            {
                funcionario = value;

                txtNome.Text = funcionario.Nome;
                txtLogin.Text = funcionario.Usuario;
                txtSenha.Text = funcionario.Senha;
                txtSalario.Text = funcionario.Salario.ToString();
                dateTimePickerDataAdmissao.Value = funcionario.DataAdmissao;

                if (funcionario.EhAdmin == true)
                    checkBoxIsAdmin.Checked = true;
                else
                    checkBoxIsAdmin.Checked = false;
            }
        }

        public Func<Funcionario, ValidationResult> GravarRegistro { get; set; }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            funcionario.Nome = txtNome.Text;
            funcionario.Usuario = txtLogin.Text;
            funcionario.Senha = txtSenha.Text;

            if (string.IsNullOrEmpty(txtSalario.Text) == false)
            {
                var valorFormatado = txtSalario.Text.Replace(".", ",");

                var conversaoRealizada = decimal.TryParse(valorFormatado, out decimal resultado);
                if (conversaoRealizada)
                    funcionario.Salario = resultado;
            }

            funcionario.DataAdmissao = dateTimePickerDataAdmissao.Value;

            if (checkBoxIsAdmin.Checked == true)
                funcionario.EhAdmin = true;
            else
                funcionario.EhAdmin = false;

            var resultadoValidacao = GravarRegistro(funcionario);

            if (resultadoValidacao.IsValid == false)
            {
                string erro = resultadoValidacao.Errors[0].ErrorMessage;

                TelaPrincipalForm.Instancia.AtualizarRodape(erro);

                DialogResult = DialogResult.None;
            }
        }



    }
}