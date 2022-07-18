using LocadoraVeiculos.Aplicacao.ModuloCliente;
using LocadoraVeiculos.Aplicacao.ModuloCondutor;
using LocadoraVeiculos.Dominio.ModuloCondutor;
using LocadoraVeiculos.WinApp;
using LocadoraVeiculos.WinApp.Compartilhado;
using LocadoraVeiculos.WinApp.ModuloCondutor;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Locadora_Veiculos.WinApp.ModuloCondutor
{
    public class ControladorCondutor : ControladorBase
    {
        private ListagemCondutoresControl listagemCondutores;
        private readonly IServicoCliente servicoCliente;
        private readonly IServicoCondutor servicoCondutor;

        public ControladorCondutor(IServicoCondutor servicoCondutor, IServicoCliente servicoCliente)
        {
            this.servicoCondutor = servicoCondutor;
            this.servicoCliente = servicoCliente;
        }

        public override void Inserir()
        {
            var resultadoSelecaoClientes = servicoCliente.SelecionarTodos();

            if (resultadoSelecaoClientes.IsFailed)
            {
                string erro = resultadoSelecaoClientes.Errors[0].Message;

                MessageBox.Show(erro,
                    "Inserção de Condutores", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            TelaCadastroCondutorForm tela = new TelaCadastroCondutorForm(resultadoSelecaoClientes.Value);

            tela.Condutor = new Condutor();

            tela.GravarRegistro = servicoCondutor.Inserir;

            if (tela.ShowDialog() == DialogResult.OK)
            {
                CarregarCondutores();
            }
        }

        public override void Editar()
        {
            var id = listagemCondutores.ObtemIdCondutorSelecionado();

            if (id == Guid.Empty)
            {
                MessageBox.Show("Selecione um condutor primeiro",
                    "Edição de Condutores", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var resultado = servicoCondutor.SelecionarPorId(id);

            if (resultado.IsFailed)
            {
                MessageBox.Show(resultado.Errors[0].Message,
                    "Edição de Condutores", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Condutor condutorSelecionado = resultado.Value;

            var resultadoSelecaoClientes = servicoCliente.SelecionarTodos();

            if (resultadoSelecaoClientes.IsFailed)
            {
                MessageBox.Show(resultado.Errors[0].Message,
                    "Edição de Condutores", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            TelaCadastroCondutorForm tela = new TelaCadastroCondutorForm(resultadoSelecaoClientes.Value);

            tela.Condutor = condutorSelecionado.Clone();

            tela.GravarRegistro = servicoCondutor.Editar;

            if (tela.ShowDialog() == DialogResult.OK)
            {
                CarregarCondutores();
            }
        }

        public override void Excluir()
        {
            var id = listagemCondutores.ObtemIdCondutorSelecionado();

            if (id == Guid.Empty)
            {
                MessageBox.Show("Selecione um condutor primeiro",
                    "Exclusão de Condutores", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var resultadoSelecao = servicoCondutor.SelecionarPorId(id);

            if (resultadoSelecao.IsFailed)
            {
                MessageBox.Show(resultadoSelecao.Errors[0].Message,
                    "Exclusão de Condutores", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var condutorSelecionado = resultadoSelecao.Value;

            if (MessageBox.Show("Deseja realmente excluir o condutor?", "Exclusão de Condutor",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                var resultadoExclusao = servicoCondutor.Excluir(condutorSelecionado);

                if (resultadoExclusao.IsSuccess)
                    CarregarCondutores();
                else
                    MessageBox.Show(resultadoExclusao.Errors[0].Message,
                        "Exclusão de Condutores", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override ConfiguracaoToolboxBase ObtemConfiguracaoToolbox()
        {
            return new ConfiguracaoToolboxCondutor();
        }

        public override UserControl ObtemListagem()
        {
            if (listagemCondutores == null)
                listagemCondutores = new ListagemCondutoresControl();

            CarregarCondutores();

            return listagemCondutores;
        }

        private void CarregarCondutores()
        {
            var resultado = servicoCondutor.SelecionarTodos();

            if (resultado.IsSuccess)
            {
                List<Condutor> condutors = resultado.Value;

                listagemCondutores.AtualizarRegistros(condutors);

                TelaPrincipalForm.Instancia.AtualizarRodape($"Visualizando {condutors.Count} condutor(es)");
            }
            else
            {
                MessageBox.Show(resultado.Errors[0].Message, "Visuaulização de Condutores",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}