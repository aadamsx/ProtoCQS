namespace Core
{
    public interface IDatabaseFactory
    {
        IDatabase CreateInstance();
    }
}