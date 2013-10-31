using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.SqlServer;
using System.Runtime.Remoting.Messaging;

namespace Data.Infrastructure
{
    // EF6 Suspendable Execution Strategy
    // EF6 introduces the new connection resiliency feature that allows for automatic retries of failed database operations.
    // Global switch to enable/disable retry logic.  Such a switch isn’t built into EF6 as of this class introduction.
    // http://entityframework.codeplex.com/wikipage?title=Connection%20Resiliency%20Spec
    // http://romiller.com/2013/08/19/ef6-suspendable-execution-strategy/


    /*
        Code-based configuration in EF6 and above is achieved by creating a subclass of System.Data.Entity.Config.DbConfiguration. 
        The following guidelines should be followed when subclassing DbConfiguration:
        
     * Create only one DbConfiguration class for your application. This class specifies app-domain wide settings.
        
     * Place your DbConfiguration class in the same assembly as your DbContext class. (See the Moving DbConfiguration section if you want to change this.)
        
     * Give your DbConfiguration class a public parameterless constructor.
        
     * Set configuration options by calling protected DbConfiguration methods from within this constructor.
        
     Following these guidelines allows EF to discover and use your configuration automatically by both tooling that 
     needs to access your model and when your application is run.
     http://msdn.microsoft.com/en-us/data/jj680699.aspx
     */

    //public class ContextConfiguration : DbConfiguration, IContextConfiguration
    //{
    //    public ContextConfiguration()
    //    {
    //        this.SetExecutionStrategy("System.Data.SqlClient", () => SuspendExecutionStrategy
    //          ? (IDbExecutionStrategy)new DefaultExecutionStrategy()
    //          : new SqlAzureExecutionStrategy(1, TimeSpan.FromSeconds(30)));

    //        // registered with EF using the DbInterception class
    //        //Interception.AddInterceptor(new NLogCommandInterceptor());
    //        DbInterception.Add(new NLogCommandInterceptor());

    //    }

    //    public static bool SuspendExecutionStrategy
    //    {
    //        get
    //        {
    //            return (bool?)CallContext.LogicalGetData("SuspendExecutionStrategy") ?? false;
    //        }
    //        set
    //        {
    //            CallContext.LogicalSetData("SuspendExecutionStrategy", value);
    //        }
    //    }
    //}
}
