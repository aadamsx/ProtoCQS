using System;
using System.ComponentModel.DataAnnotations;

namespace Proto.Mvc.Mgmt.Models
{
    public class TenantViewModel
    {
        public int TenantId { get; set; }
        public string AccountNumber { get; set; }

        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Company Name")]
        public string Name { get; set; }
        public Nullable<int> Active { get; set; }

        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Primary Contact First Name")]
        public string PrimaryContactFirstName { get; set; }

        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Primary Contact Last Name")]
        public string PrimaryContactLastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Primary Contact Phone Number")]
        public string PrimaryContactPhone { get; set; }

        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "Company Description")]
        public string Description { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string OfficePhone { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        [DataType(DataType.PostalCode)]
        public string Zip { get; set; }
        public string LastModifiedBy { get; set; }
        public System.Guid RowGuid { get; set; }
        public Byte[] RowVersion { get; set; }
        public int ContactTypeId { get; set; }


    }
}