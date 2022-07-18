using Locadora_Veiculos.WinApp.ModuloCondutor;
using LocadoraVeiculos.Aplicacao.ModuloCliente;
using LocadoraVeiculos.Aplicacao.ModuloCondutor;
using LocadoraVeiculos.Aplicacao.ModuloFuncionario;
using LocadoraVeiculos.Aplicacao.ModuloGrupoVeiculos;
using LocadoraVeiculos.Aplicacao.ModuloTaxa;
using LocadoraVeiculos.Infra.BancoDados.ModuloCliente;
using LocadoraVeiculos.Infra.BancoDados.ModuloCondutor;
using LocadoraVeiculos.Infra.BancoDados.ModuloFuncionario;
using LocadoraVeiculos.Infra.BancoDados.ModuloGrupoVeiculos;
using LocadoraVeiculos.Infra.BancoDados.ModuloTaxa;
using LocadoraVeiculos.WinApp.ModuloCliente;
using LocadoraVeiculos.WinApp.ModuloFuncionario;
using LocadoraVeiculos.WinApp.ModuloGrupoVeiculos;
using LocadoraVeiculos.WinApp.ModuloTaxas;
using System.Collections.Generic;

namespace LocadoraVeiculos.WinApp.Compartilhado
{
    public class ServiceLocatorManual : IServiceLocator
    {
        private Dictionary<string, ControladorBase> controladores;

        public ServiceLocatorManual()
        {
            InicializarControladores();
        }

        public T Get<T>() where T : ControladorBase
        {
            var tipo = typeof(T);

            var nomeControlador = tipo.Name;

            return (T)controladores[nomeControlador];
        }

        private void InicializarControladores()
        {
            controladores = new Dictionary<string, ControladorBase>();

            var repositorioCliente = new RepositorioClienteEmBancoDados();
            var servicoCliente = new ServicoCliente(repositorioCliente);
            controladores.Add("ControladorCliente", new ControladorCliente(servicoCliente));

            var repositorioFuncionario = new RepositorioFuncionarioEmBancoDados();
            var servicoFuncionario = new ServicoFuncionario(repositorioFuncionario);
            controladores.Add("ControladorFuncionario", new ControladorFuncionario(servicoFuncionario));

            var repositorioTaxa = new RepositorioTaxaEmBancoDados();
            var servicoTaxa = new ServicoTaxa(repositorioTaxa);
            controladores.Add("ControladorTaxa", new ControladorTaxa(servicoTaxa));

            var repositorioGrupoVeiculos = new RepositorioGrupoVeiculosEmBancoDados();
            var servicoGrupoVeiculo = new ServicoGrupoVeiculo(repositorioGrupoVeiculos);
            controladores.Add("ControladorGrupoVeiculos", new ControladorGrupoVeiculos(servicoGrupoVeiculo));

            var repositorioCondutor = new RepositorioCondutorEmBancoDados();
            var servicoCondutor = new ServicoCondutor(repositorioCondutor);
            controladores.Add("ControladorCondutor", new ControladorCondutor(servicoCondutor, servicoCliente));
        }
    }
}
