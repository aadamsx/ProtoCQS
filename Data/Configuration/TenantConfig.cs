using System.Data.Entity.ModelConfiguration;
using DataModel;

namespace Data.Configuration
{
    // The company, or FormsPoint's clients
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

    // The paying person or point of contact
    public class CustomerConfig : EntityTypeConfiguration<Customer>
    {
        public CustomerConfig()
        {
            HasKey(t => t.CustomerId);
        }
    }

    public class ContactTypeConfig : EntityTypeConfiguration<ContactType>
    {
        public ContactTypeConfig()
        {
            // Primary Key
            HasKey(t => t.ContactTypeId);

            // Properties
            Property(t => t.Name).HasMaxLength(30).IsRequired();
        }
    }
}
