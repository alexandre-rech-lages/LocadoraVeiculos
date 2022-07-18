namespace LocadoraVeiculos.WinApp.Compartilhado
{
    public interface IServiceLocator
    {
        T Get<T>() where T : ControladorBase;
    }
}