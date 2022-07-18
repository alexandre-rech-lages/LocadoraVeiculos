using LocadoraVeiculos.Aplicacao.ModuloGrupoVeiculos;
using LocadoraVeiculos.Dominio.ModuloGrupoVeiculos;
using LocadoraVeiculos.WinApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LocadoraVeiculos.WinApp.ModuloGrupoVeiculos
{
    public class ControladorGrupoVeiculos : ControladorBase
    {
        private ListagemGrupoVeiculosControl listagemGrupoVeiculos;
        private readonly ServicoGrupoVeiculo servicoGrupoVeiculo;

        public ControladorGrupoVeiculos(ServicoGrupoVeiculo servicoGrupoVeiculo)
        {
            this.servicoGrupoVeiculo = servicoGrupoVeiculo;
        }

        public override void Inserir()
        {
            var tela = new TelaCadastroGrupoVeiculos();

            tela.Grupo = new GrupoVeiculo();

            tela.GravarRegistro = servicoGrupoVeiculo.Inserir;

            if (tela.ShowDialog() == DialogResult.OK)
            {
                CarregarGrupos();
            }
        }

        public override void Editar()
        {
            var id = listagemGrupoVeiculos.ObtemIdGrupoVeiculosSelecionado();

            if (id == Guid.Empty)
            {
                MessageBox.Show("Selecione um Grupo de Veículos primeiro",
                    "Edição de Grupo de Veículos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var resultado = servicoGrupoVeiculo.SelecionarPorId(id);

            if (resultado.IsFailed)
            {
                MessageBox.Show(resultado.Errors[0].Message,
                    "Edição de Grupo de Veículos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var grupoVeiculosSelecionado = resultado.Value;

            var tela = new TelaCadastroGrupoVeiculos();

            tela.Grupo = grupoVeiculosSelecionado.Clone();

            tela.GravarRegistro = servicoGrupoVeiculo.Editar;

            if (tela.ShowDialog() == DialogResult.OK)
            {
                CarregarGrupos();
            }
        }

        public override void Excluir()
        {
            var id = listagemGrupoVeiculos.ObtemIdGrupoVeiculosSelecionado();

            if (id == Guid.Empty)
            {
                MessageBox.Show("Selecione um Grupo de Veículos primeiro",
                    "Exclusão de Grupo de Veículos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var resultado = servicoGrupoVeiculo.SelecionarPorId(id);

            if (resultado.IsFailed)
            {
                MessageBox.Show(resultado.Errors[0].Message,
                    "Exclusão de Grupo de Veículos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var grupoVeiculosSelecionado = resultado.Value;

            if (MessageBox.Show("Deseja realmente excluir o grupo de veículos?", "Exclusão de Grupo de Veículos",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                var resultadoExclusao = servicoGrupoVeiculo.Excluir(grupoVeiculosSelecionado);

                if (resultadoExclusao.IsSuccess)
                    CarregarGrupos();
                else
                    MessageBox.Show(resultadoExclusao.Errors[0].Message,
                        "Exclusão de Funcionário", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void CarregarGrupos()
        {
            var resultado = servicoGrupoVeiculo.SelecionarTodos();

            if (resultado.IsSuccess)
            {
                List<GrupoVeiculo> gruposVeiculos = resultado.Value;

                listagemGrupoVeiculos.AtualizarRegistros(gruposVeiculos);

                TelaPrincipalForm.Instancia.AtualizarRodape($"Visualizando {gruposVeiculos.Count} grupo(s) de veículos");
            }
            else
            {
                MessageBox.Show(resultado.Errors[0].Message, "Visualização de Grupos de Veículos",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}