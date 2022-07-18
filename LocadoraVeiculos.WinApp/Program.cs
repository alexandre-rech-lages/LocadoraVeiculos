using LocadoraVeiculos.Infra.Logging;
using LocadoraVeiculos.WinApp.Compartilhado.ServiceLocator;
using System;
using System.Windows.Forms;

namespace LocadoraVeiculos.WinApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ConfiguracaoLogsLocadora.ConfigurarEscritaLogs();
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var serviceLocatorAutofac = new ServiceLocatorComAutofac();

            Application.Run(new TelaPrincipalForm(serviceLocatorAutofac));
        }
    }
}