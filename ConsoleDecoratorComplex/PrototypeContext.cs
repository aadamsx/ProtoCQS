using System.Data.Entity;

namespace ProtoConsole
{
    public class PrototypeContext : DbContext
    {
        DbSet<Entity> Entities { get; set; } 
    }
}