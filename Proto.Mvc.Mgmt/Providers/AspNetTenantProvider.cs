using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proto.Mvc.Mgmt.Providers
{
public class AspNetTenantProvider
{
    public Guid CurrentTenantId { get { return (Guid)HttpContext.Current.Session["TenantId"]; } }
}
}