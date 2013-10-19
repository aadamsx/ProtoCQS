using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Data.Configuration;
using DataModel;

namespace Data
{
    public class ClientManagementContext : DbContext, IDbContext
    {
        private readonly Guid _instanceId;

        public ClientManagementContext()
            : base("Name=ClientManagementContext")
        {
            //Configuration.LazyLoadingEnabled = true;
            _instanceId = Guid.NewGuid();
        }

        //public ClientManagementContext(string connectionstring)
        //    : base("Name=ClientManagementContext")
        //{
        //    //Configuration.LazyLoadingEnabled = true;
        //    _instanceId = Guid.NewGuid();
        //}

        public Guid InstanceId
        {
            get { return _instanceId; }
        }

        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<ContactType> ContactTypes { get; set; }
        public DbSet<ConnectionConfiguration> ConnectionConfigurations { get; set; } 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            // Added these options, might remove them after load testing ...
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Add any configuration or mapping stuff here
            modelBuilder.Configurations.Add(new TenantConfig());
            modelBuilder.Configurations.Add(new AddressConfig());
            modelBuilder.Configurations.Add(new ContactTypeConfig());
            modelBuilder.Configurations.Add(new ConnectionConfigurationConfig());

            this.Configuration.LazyLoadingEnabled = false;
        }
    }

    public interface IDbContext
    {
    }
}