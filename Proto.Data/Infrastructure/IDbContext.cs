namespace Proto.Data.Infrastructure
{
    /// <summary>
    /// to deal with multiple data stores. To minimize the number of needed interfaces I decided 
    /// to define a single generic interface
    /// for creating dbcontext classes
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public interface IDbContextFactory<TDbContext>
    {
        TDbContext CreateNew();
    }
}
