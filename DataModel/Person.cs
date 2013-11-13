using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Person
    {
        public int PersonId { get; set; }
        public string PersonType { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public Guid RowGuid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
