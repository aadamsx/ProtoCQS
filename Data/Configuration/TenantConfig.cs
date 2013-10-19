using System.Data.Entity.ModelConfiguration;
using Domain.Model;

namespace Data.Configuration
{
    public class TenantConfig : EntityTypeConfiguration<Tenant>
    {
        public TenantConfig()
        {
            // Primary Key
            HasKey(t => t.TenantId);

            // Properties
            Property(t => t.AccountNumber).HasMaxLength(10);
            Property(t => t.Name).HasMaxLength(50).IsRequired();
            Property(t => t.Email).HasMaxLength(50);
            Property(t => t.PrimaryContactFirstName).HasMaxLength(50).IsRequired();
            Property(t => t.PrimaryContactLastName).HasMaxLength(50).IsRequired();
            Property(t => t.PrimaryContactPhone).HasMaxLength(25).IsRequired();
            Property(t => t.OfficePhone).HasMaxLength(25).IsRequired();

            Property(t => t.Street).HasColumnName("Street").HasMaxLength(50);
            Property(t => t.City).HasColumnName("City").HasMaxLength(50);
            Property(t => t.State).HasColumnName("State").HasMaxLength(2);
            Property(t => t.Zip).HasColumnName("Zip").HasMaxLength(10);

            Property(t => t.RowVersion)
                .IsRowVersion();
                //.IsConcurrencyToken();

            // Relations
            HasRequired(t => t.Type)
                .WithMany(t => t.Tenants)
                .HasForeignKey(d => d.ContactTypeId);

        }

    }
}
