using System.Data.Entity.ModelConfiguration;
using DataModel;

namespace Data.Configuration
{
    public class TraceLogConfig : EntityTypeConfiguration<TraceLog>
    {
        public TraceLogConfig()
        {
            // Primary Key
            HasKey(t => t.LogId);

        }

    }
}
