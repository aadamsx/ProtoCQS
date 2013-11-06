using System.Web.Mvc;

namespace ViewModel
{
    public class TenantEdit
    {
        public Tenant Tenant { get; set; }
        public SelectList Types { get; set; }
    }
}