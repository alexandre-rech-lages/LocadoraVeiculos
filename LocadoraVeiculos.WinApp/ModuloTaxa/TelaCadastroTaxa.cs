﻿using FluentValidation.Results;
using LocadoraVeiculos.Dominio.ModuloTaxa;
using System;
using System.Windows.Forms;

namespace LocadoraVeiculos.WinApp.ModuloTaxas
{
    public partial class TelaCadastroTaxa : Form
    {
        private Taxa taxa;

        public TelaCadastroTaxa()
        {
            InitializeComponent();
        }

        public Func<Taxa, ValidationResult> GravarRegistro { get; set; }

        public Taxa Taxa
        {
            get
            {
                return taxa;
            }

            set
            {
                taxa = value;
                txtDescricao.Text = taxa.Descricao;
                numericValor.Value = taxa.Valor;
                if (taxa.TipoCalculo == 0)
                    radioButtonDiario.Checked = true;
                else radioButtonFixo.Checked = true;

            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            taxa.Descricao = txtDescricao.Text;
            taxa.Valor = numericValor.Value;

            taxa.TipoCalculo = (TipoCalculo)(radioButtonDiario.Checked == true ? 0 : 1);

            var resultadoValidacao = GravarRegistro(taxa);
            if (resultadoValidacao.IsValid == false)
            {
                string erro = resultadoValidacao.Errors[0].ErrorMessage;

                TelaPrincipalForm.Instancia.AtualizarRodape(erro);

                DialogResult = DialogResult.None;
            }
        }
    }
}