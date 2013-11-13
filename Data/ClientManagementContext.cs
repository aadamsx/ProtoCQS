using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Data.Configuration;

namespace Data
{
    public class ClientManagementContext : DbContext, IDbContext
    {
        //private readonly Guid _instanceId;

        public ClientManagementContext()
            : base("Name=ClientManagement")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            //_instanceId = Guid.NewGuid();

            //DbInterception.Add(new NLogCommandInterceptor());
        }

        public ClientManagementContext GetContext()
        {
            return this;
        }

        //public ClientManagementContext(string connectionstring)
        //    : base("Name=ClientManagementContext")
        //{
        //    //Configuration.LazyLoadingEnabled = true;
        //    _instanceId = Guid.NewGuid();
        //}

        //public Guid InstanceId
        //{
        //    get { return _instanceId; }
        //}

        //public DbSet<Tenant> Tenants { get; set; }
        //public DbSet<ContactType> ContactTypes { get; set; }
        //public DbSet<ConnectionConfiguration> ConnectionConfigurations { get; set; } 
        //public DbSet<TraceLog> TraceLogs { get; set; }


        //public DbSet<Tenant> Tenants { get; set; }
        //public DbSet<ContactType> ContactTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            // Added these options, might remove them after load testing ...
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            
            // Tenant
            modelBuilder.Configurations.Add(new TenantConfig());
            modelBuilder.Configurations.Add(new CustomerConfig());
            modelBuilder.Configurations.Add(new ContactTypeConfig());

            // Address
            modelBuilder.Configurations.Add(new AddressConfig());

            // Order
            modelBuilder.Configurations.Add(new OrderConfig());
            modelBuilder.Configurations.Add(new OrderLineConfig());
            modelBuilder.Configurations.Add(new PaymentConfig());

            // Product
            modelBuilder.Configurations.Add(new ProductConfig());
            modelBuilder.Configurations.Add(new ProductPackageConfig());
            modelBuilder.Configurations.Add(new ProductSubCategoryConfig());
            modelBuilder.Configurations.Add(new ProductCategoryConfig());
            modelBuilder.Configurations.Add(new ProductListPriceHistoryConfig());
            modelBuilder.Configurations.Add(new TransactionHistoryConfig());

            // Tooling
            modelBuilder.Configurations.Add(new ConnectionConfigurationConfig());
            modelBuilder.Configurations.Add(new TraceLogConfig());
            //Configuration.LazyLoadingEnabled = false;
        }

    }
}