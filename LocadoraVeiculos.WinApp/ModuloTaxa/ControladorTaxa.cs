using LocadoraVeiculos.Aplicacao.ModuloTaxa;
using LocadoraVeiculos.Dominio.ModuloTaxa;
using LocadoraVeiculos.WinApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LocadoraVeiculos.WinApp.ModuloTaxas
{
    public class ControladorTaxa : ControladorBase
    {
        private ListagemTaxaControl listagemTaxas;
        private readonly IServicoTaxa servicoTaxa;

        public ControladorTaxa(IServicoTaxa servicoTaxa)
        {
            this.servicoTaxa = servicoTaxa;
        }

        public override void Inserir()
        {
            var tela = new TelaCadastroTaxa();

            tela.Taxa = new Taxa();

            tela.GravarRegistro = servicoTaxa.Inserir;

            if (tela.ShowDialog() == DialogResult.OK)
            {
                CarregarTaxas();
            }
        }

        public override void Editar()
        {
            var id = listagemTaxas.ObtemIdTaxaSelecionada();

            if (id == Guid.Empty)
            {
                MessageBox.Show("Selecione uma taxa primeiro",
                    "Edição de Taxa", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var resultado = servicoTaxa.SelecionarPorId(id);

            if (resultado.IsFailed)
            {
                MessageBox.Show(resultado.Errors[0].Message,
                    "Edição de Taxa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var taxaSelecionada = resultado.Value;

            var tela = new TelaCadastroTaxa();

            tela.Taxa = taxaSelecionada;

            tela.GravarRegistro = servicoTaxa.Editar;

            if (tela.ShowDialog() == DialogResult.OK)
            {
                CarregarTaxas();
            }
        }

        public override void Excluir()
        {
            var id = listagemTaxas.ObtemIdTaxaSelecionada();

            if (id == Guid.Empty)
            {
                MessageBox.Show("Selecione uma taxa primeiro",
                    "Exclusão de Taxa", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var resultado = servicoTaxa.SelecionarPorId(id);

            if (resultado.IsFailed)
            {
                MessageBox.Show(resultado.Errors[0].Message,
                    "Exclusão de Taxa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var taxaSelecionada = resultado.Value;

            if (MessageBox.Show("Deseja realmente excluir a taxa?", "Exclusão de Taxa",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                var resultadoExclusao = servicoTaxa.Excluir(taxaSelecionada);

                if (resultadoExclusao.IsSuccess)
                    CarregarTaxas();
                else
                    MessageBox.Show(resultadoExclusao.Errors[0].Message,
                        "Exclusão de Taxa", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override ConfiguracaoToolboxBase ObtemConfiguracaoToolbox()
        {
            return new ConfiguracaoToolBoxTaxa();
        }

        public override UserControl ObtemListagem()
        {
            if (listagemTaxas == null)
                listagemTaxas = new ListagemTaxaControl();

            CarregarTaxas();

            return listagemTaxas;
        }

        private void CarregarTaxas()
        {
            var resultado = servicoTaxa.SelecionarTodos();

            if (resultado.IsSuccess)
            {
                List<Taxa> taxas = resultado.Value;

                listagemTaxas.AtualizarRegistros(taxas);

                TelaPrincipalForm.Instancia.AtualizarRodape($"Visualizando {taxas.Count} taxa(s)");
            }
            else
            {
                MessageBox.Show(resultado.Errors[0].Message, "Visualização de Taxas",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}