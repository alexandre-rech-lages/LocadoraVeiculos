using LocadoraVeiculos.Aplicacao.ModuloFuncionario;
using LocadoraVeiculos.Dominio.ModuloFuncionario;
using LocadoraVeiculos.WinApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LocadoraVeiculos.WinApp.ModuloFuncionario
{
    public class ControladorFuncionario : ControladorBase
    {
        private readonly IRepositorioFuncionario repositorioFuncionario;
        private ListagemFuncionarioControl listagemFuncionarios;
        private readonly ServicoFuncionario servicoFuncionario;

        public ControladorFuncionario(IRepositorioFuncionario repositorioFuncionario,
            ServicoFuncionario servicoFuncionario)
        {
            this.repositorioFuncionario = repositorioFuncionario;
            this.servicoFuncionario = servicoFuncionario;
        }

        public override void Inserir()
        {
            var tela = new TelaCadastroFuncionarioForm();

            tela.Funcionario = new Funcionario();

            tela.GravarRegistro = servicoFuncionario.Inserir;

            if (tela.ShowDialog() == DialogResult.OK)
            {
                CarregarFuncionarios();
            }
        }

        public override void Editar()
        {
            var id = listagemFuncionarios.ObtemIdFuncionarioSelecionado();

            if (id == Guid.Empty)
            {
                MessageBox.Show("Selecione um funcionário primeiro",
                "Edição de Funcionário", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var funcionarioSelecionado = repositorioFuncionario.SelecionarPorId(id);

            var tela = new TelaCadastroFuncionarioForm();

            tela.Funcionario = funcionarioSelecionado.Clone();

            tela.GravarRegistro = servicoFuncionario.Editar;

            if (tela.ShowDialog() == DialogResult.OK)
                CarregarFuncionarios();
        }

        public override void Excluir()
        {
            var id = listagemFuncionarios.ObtemIdFuncionarioSelecionado();

            if (id == Guid.Empty)
            {
                MessageBox.Show("Selecione um funcionário primeiro",
                    "Exclusão de Funcionário", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var funcionarioSelecionado = repositorioFuncionario.SelecionarPorId(id);

            if (MessageBox.Show("Deseja realmente excluir o funcionário?", "Exclusão de Funcionário",
                 MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                repositorioFuncionario.Excluir(funcionarioSelecionado);
                CarregarFuncionarios();
            }
        }

        public override ConfiguracaoToolboxBase ObtemConfiguracaoToolbox()
        {
            return new ConfiguracaoToolboxFuncionario();
        }

        public override UserControl ObtemListagem()
        {
            if (listagemFuncionarios == null)
                listagemFuncionarios = new ListagemFuncionarioControl();

            CarregarFuncionarios();

            return listagemFuncionarios;
        }

        private void CarregarFuncionarios()
        {
            List<Funcionario> funcionarios = repositorioFuncionario.SelecionarTodos();
            listagemFuncionarios.AtualizarRegistros(funcionarios);
            TelaPrincipalForm.Instancia.AtualizarRodape($"Visualizando {funcionarios.Count} funcionário(s)");
        }
    }
}
