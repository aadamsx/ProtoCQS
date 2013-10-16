
using Proto.Model.Entities;

namespace Proto.Domain.Commands.Tenants
{
    public class CreateOrUpdateTenantCommand
    {
        public int TenantId { get; set; }
        public string AccountNumber { get; set; }
        public string Name { get; set; }
        public System.Nullable<int> Active { get; set; }
        public string PrimaryContactFirstName { get; set; }
        public string PrimaryContactLastName { get; set; }
        public string PrimaryContactPhone { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string OfficePhone { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public System.Guid RowGuid { get; set; }
        public string LastModifiedBy { get; set; }
        public int ContactTypeId { get; set; }
 
        // In surrport of Concurrency Exception and other failures
        public byte[] RowVersion { get; set; }
        public Tenant DatabaseValues { get; set; } 
        public Tenant ClientValues { get; set; }
        public bool SaveFailed { get; set; }
        public bool ConcurrencyException { get; set; }
        public bool DataException { get; set; }
        public bool UpdateException { get; set; }
        public bool NullRefException { get; set; }
        public bool OtherException { get; set; }
    }
}
