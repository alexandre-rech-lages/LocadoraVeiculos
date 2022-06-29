﻿using LocadoraVeiculos.Aplicacao.ModuloTaxa;
using LocadoraVeiculos.Dominio.ModuloTaxa;
using LocadoraVeiculos.WinApp.Compartilhado;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LocadoraVeiculos.WinApp.ModuloTaxas
{
    public class ControladorTaxa : ControladorBase
    {
        private IRepositorioTaxa repositorioTaxa;
        private ListagemTaxaControl listagemTaxa;
        private ServicoTaxa servicoTaxa;

        public ControladorTaxa(IRepositorioTaxa repositorioTaxa, ServicoTaxa servicoTaxa)
        {
            this.repositorioTaxa = repositorioTaxa;
            this.servicoTaxa = servicoTaxa;
        }

        public override void Inserir()
        {
            var tela = new TelaCadastroTaxa();
            tela.Taxa = new Taxa();
            tela.GravarRegistro = servicoTaxa.Inserir;

            DialogResult resultado = tela.ShowDialog();
            if (resultado == DialogResult.OK)
                CarregarTaxas();
        }

        public override void Editar()
        {
            Taxa taxaSelecionada = ObtemTaxaSelecionada();

            if (taxaSelecionada == null)
            {
                MessageBox.Show("Selecione uma taxa primeiro",
                "Edição de Taxa", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var tela = new TelaCadastroTaxa();

            tela.Taxa = taxaSelecionada;

            tela.GravarRegistro = servicoTaxa.Editar;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
                CarregarTaxas();
        }

        public override void Excluir()
        {
            Taxa taxaSelecionada = ObtemTaxaSelecionada();

            if (taxaSelecionada == null)
            {
                MessageBox.Show("Selecione uma taxa primeiro",
                "Exclusão de Taxa", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult resultado = MessageBox.Show("Deseja realmente excluir a taxa?",
            "Exclusão de Taxa", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (resultado == DialogResult.OK)
            {
                repositorioTaxa.Excluir(taxaSelecionada);
                CarregarTaxas();
            }
        }

        private Taxa ObtemTaxaSelecionada()
        {
            var id = listagemTaxa.ObtemIdTaxaSelecionada();

            return repositorioTaxa.SelecionarPorId(id);
        }

        public override ConfiguracaoToolboxBase ObtemConfiguracaoToolbox()
        {
            return new ConfiguracaoToolBoxTaxa();
        }

        public override UserControl ObtemListagem()
        {
            if (listagemTaxa == null)
                listagemTaxa = new ListagemTaxaControl();

            CarregarTaxas();

            return listagemTaxa;
        }

        private void CarregarTaxas()
        {
            List<Taxa> taxas = repositorioTaxa.SelecionarTodos();
            listagemTaxa.AtualizarRegistros(taxas);
            TelaPrincipalForm.Instancia.AtualizarRodape($"Visualizando {taxas.Count} taxa(s)");
        }
    }
}