using System.Data.Entity.ModelConfiguration;
using DataModel;

namespace Data.Configuration
{
    class ContactTypeConfig : EntityTypeConfiguration<ContactType>
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
