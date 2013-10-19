using System.Data.Entity.ModelConfiguration;
using DataModel;

namespace Data.Configuration
{
    public class ConnectionConfigurationConfig : EntityTypeConfiguration<ConnectionConfiguration>
    {
        public ConnectionConfigurationConfig()
        {
            // Primary Keys
            HasKey(t => new { t.MachineName, t.Setting, t.Value });

            Property(t => t.RowVersion)
                .IsRowVersion();
        }

    }
}
