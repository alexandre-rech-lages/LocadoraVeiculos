﻿using LocadoraVeiculos.Aplicacao.ModuloGrupoVeiculos;
using LocadoraVeiculos.Dominio.ModuloGrupoVeiculos;
using LocadoraVeiculos.WinApp.Compartilhado;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LocadoraVeiculos.WinApp.ModuloGrupoVeiculos
{
    public class ControladorGrupoVeiculos : ControladorBase
    {
        private readonly IRepositorioGrupoVeiculos repositorioGrupoVeiculos;
        private ListagemGrupoVeiculosControl listagemGrupoVeiculos;
        private readonly ServicoGrupoVeiculo servicoGrupoVeiculo;

        public ControladorGrupoVeiculos(IRepositorioGrupoVeiculos repositorioGrupoVeiculos, 
            ServicoGrupoVeiculo servicoGrupoVeiculo)
        {
            this.repositorioGrupoVeiculos = repositorioGrupoVeiculos;
            this.servicoGrupoVeiculo = servicoGrupoVeiculo;
        }

        public override void Inserir()
        {
            var tela = new TelaCadastroGrupoVeiculos();
            tela.Grupo = new GrupoVeiculos();
            tela.GravarRegistro = servicoGrupoVeiculo.Inserir;

            DialogResult resultado = tela.ShowDialog();
            if (resultado == DialogResult.OK)
                CarregarGrupos();
        }

        public override void Editar()
        {
            GrupoVeiculos grupoVeiculosSelecionado = ObtemGrupoVeiculosSelecionado();

            if (grupoVeiculosSelecionado == null)
            {
                MessageBox.Show("Selecione um grupo de veículos primeiro",
                "Edição de Grupo de Veículos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var tela = new TelaCadastroGrupoVeiculos();

            tela.Grupo = grupoVeiculosSelecionado;

            tela.GravarRegistro = servicoGrupoVeiculo.Editar;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
                CarregarGrupos();
        }

        public override void Excluir()
        {
            GrupoVeiculos grupoVeiculosSelecionado = ObtemGrupoVeiculosSelecionado();

            if (grupoVeiculosSelecionado == null)
            {
                MessageBox.Show("Selecione um grupo de veículos primeiro",
                "Exclusão de Grupo de Veículos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult resultado = MessageBox.Show("Deseja realmente excluir o grupo de veículos?",
            "Exclusão de Grupo de Veículos", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (resultado == DialogResult.OK)
            {
                repositorioGrupoVeiculos.Excluir(grupoVeiculosSelecionado);
                CarregarGrupos();
            }
        }

        public override ConfiguracaoToolboxBase ObtemConfiguracaoToolbox()
        {
            return new ConfiguracaoToolBoxGrupoVeiculos();
        }

        public override UserControl ObtemListagem()
        {
            if (listagemGrupoVeiculos == null)
                listagemGrupoVeiculos = new ListagemGrupoVeiculosControl();

            CarregarGrupos();

            return listagemGrupoVeiculos;
        }

        #region MÉTODOS PRIVADOS

        private void CarregarGrupos()
        {
            List<GrupoVeiculos> grupos = repositorioGrupoVeiculos.SelecionarTodos();

            listagemGrupoVeiculos.AtualizarRegistros(grupos);

            TelaPrincipalForm.Instancia.AtualizarRodape($"Visualizando {grupos.Count} grupo(s) de veículos");
        }

        private GrupoVeiculos ObtemGrupoVeiculosSelecionado()
        {
            var id = listagemGrupoVeiculos.ObtemIdGrupoVeiculosSelecionado();

            return repositorioGrupoVeiculos.SelecionarPorId(id);
        }

        #endregion
    }
}