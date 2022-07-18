using LocadoraVeiculos.Aplicacao.ModuloCliente;
using LocadoraVeiculos.Dominio.ModuloCliente;
using LocadoraVeiculos.WinApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LocadoraVeiculos.WinApp.ModuloCliente
{
    public class ControladorCliente : ControladorBase
    {
        private ListagemClientesControl listagemClientes;
        private readonly IServicoCliente servicoCliente;

        public ControladorCliente(IServicoCliente servicoCliente)
        {
            this.servicoCliente = servicoCliente;
        }

        public override void Inserir()
        {
            var tela = new TelaCadastroClienteForm();

            tela.Cliente = new Cliente();

            tela.GravarRegistro = servicoCliente.Inserir;

            if (tela.ShowDialog() == DialogResult.OK)
            {
                CarregarClientes();
            }
        }

        public override void Editar()
        {
            var id = listagemClientes.ObtemIdClienteSelecionado();

            if (id == Guid.Empty)
            {
                MessageBox.Show("Selecione um cliente primeiro",
                    "Edição de Clientes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var resultado = servicoCliente.SelecionarPorId(id);

            if (resultado.IsFailed)
            {
                MessageBox.Show(resultado.Errors[0].Message,
                    "Edição de Clientes", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var clienteSelecionado = resultado.Value;

            var tela = new TelaCadastroClienteForm();

            tela.Cliente = clienteSelecionado.Clone();

            tela.GravarRegistro = servicoCliente.Editar;

            if (tela.ShowDialog() == DialogResult.OK)
                CarregarClientes();
        }

        public override void Excluir()
        {
            var id = listagemClientes.ObtemIdClienteSelecionado();

            if (id == Guid.Empty)
            {
                MessageBox.Show("Selecione um cliente primeiro",
                    "Exclusão de Clientes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var resultadoSelecao = servicoCliente.SelecionarPorId(id);

            if (resultadoSelecao.IsFailed)
            {
                MessageBox.Show(resultadoSelecao.Errors[0].Message,
                    "Exclusão de Clientes", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var clienteSelecionado = resultadoSelecao.Value;

            if (MessageBox.Show("Deseja realmente excluir o cliente?", "Exclusão de Cliente",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                var resultadoExclusao = servicoCliente.Excluir(clienteSelecionado);

                if (resultadoExclusao.IsSuccess)
                    CarregarClientes();
                else
                    MessageBox.Show(resultadoExclusao.Errors[0].Message,
                        "Exclusão de Clientes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override ConfiguracaoToolboxBase ObtemConfiguracaoToolbox()
        {
            return new ConfiguracaoToolBoxCliente();
        }

        public override UserControl ObtemListagem()
        {
            if (listagemClientes == null)
                listagemClientes = new ListagemClientesControl();

            CarregarClientes();

            return listagemClientes;
        }

        private void CarregarClientes()
        {
            var resultado = servicoCliente.SelecionarTodos();

            if (resultado.IsSuccess)
            {
                List<Cliente> clientes = resultado.Value;

                listagemClientes.AtualizarRegistros(clientes);

                TelaPrincipalForm.Instancia.AtualizarRodape($"Visualizando {clientes.Count} cliente(s)");
            }
            else
            {
                MessageBox.Show(resultado.Errors[0].Message, "Visuaulização de Clientes",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}