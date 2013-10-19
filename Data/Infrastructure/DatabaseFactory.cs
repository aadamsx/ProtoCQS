namespace Data.Infrastructure
{
    //public class ClinetDatabaseFactory
    //    : IDbContextFactory<ClientManagementContext>
    //{
    //    private static MappingSource Source =
    //        new AttributeMappingSource();

    //    private readonly string conStr;

    //    public ClinetDatabaseFactory(string conStr)
    //    {
    //        this.conStr = conStr;
    //    }

    //    public ClientManagementContext CreateNew()
    //    {
    //        var db = new DbContext("Name=ClientManagementContext");
    //        //db.DefaultContainerName = "NorthwindEntities";
    //        var mapper = new EntityFrameworkDataMapper(db);
    //        return new NorthwindUnitOfWork(mapper);
    //    }
    //}
}
