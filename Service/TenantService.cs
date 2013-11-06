using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using Repository;

namespace Service
{
    public class TenantService
    {
        private readonly IReadRepository<Tenant> _read;

        public TenantService(IReadRepository<Tenant> read)
        {
            _read = read;
        }
    }
}
