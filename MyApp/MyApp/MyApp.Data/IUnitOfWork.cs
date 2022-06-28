namespace MyApp.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
    }
}
