namespace Data.Infrastructure
{
    public interface IDatabaseFactory
    {
        IDatabase CreateInstance();
    }
}