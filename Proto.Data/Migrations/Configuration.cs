using Proto.Model.Entities;

namespace Proto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ClientManagementContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ClientManagementContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            // the first column is what makes the record distinct?

            context.Set<ContactType>().AddOrUpdate(
                ct => ct.Name,
                new ContactType { Name = "consultant" },
                new ContactType { Name = "lawyer" }
            );

            context.Set<Tenant>().AddOrUpdate(
                _ => _.Name,
                new Tenant
                {
                    AccountNumber = "123",
                    Active = 1,
                    BillingAddress = new Address()
                    {
                        Street = "123 foo dr.",
                        City = "foo place",
                        State = "tx",
                        Zip = "123"
                    },
                    ContactTypeId = 1,
                    Description = "foo immigration",
                    Email = "foo@immigration.com",
                    //ModifiedDate = DateTime.Now,
                    Name = "foo",
                    OfficePhone = "5551234561",
                    PrimaryContactFirstName = "foo",
                    PrimaryContactLastName = "bar",
                    PrimaryContactPhone = "5556782341"
                }
            );

            context.Set<Tenant>().AddOrUpdate(
                _ => _.Name,
                new Tenant
                {
                    AccountNumber = "234",
                    Active = 1,
                    BillingAddress = new Address()
                    {
                        Street = "234 bar dr.",
                        City = "bar place",
                        State = "tx",
                        Zip = "234"
                    },
                    ContactTypeId = 1,
                    Description = "bar immigration",
                    Email = "bar@immigration.com",
                    //ModifiedDate = DateTime.Now,
                    Name = "bar",
                    OfficePhone = "5556789122",
                    PrimaryContactFirstName = "barbar",
                    PrimaryContactLastName = "foo",
                    PrimaryContactPhone = "5556789122"
                }
            );

            context.Set<ConnectionConfiguration>().AddOrUpdate(
                _ => _.Value,
                new ConnectionConfiguration
                {
                    MachineName = "DevWeb",
                    Setting = "ClientManagementContext",
                    Type = "ConnectionString",
                    Value = "Data Source=(localdb)\v11.0;Initial Catalog=ClientManagementContext;Integrated Security=True",
                    Provider = "System.Data.SqlClient"
                }
            );

            context.Set<ConnectionConfiguration>().AddOrUpdate(
                _ => _.Value,
                new ConnectionConfiguration
                {
                    MachineName = "TestWeb",
                    Setting = "ClientManagementContext",
                    Type = "ConnectionString",
                    Value = "Data Source=(localdb)\v11.0;Initial Catalog=ClientManagementContext;Integrated Security=True",
                    Provider = "System.Data.SqlClient"
                }
            );

            context.Set<ConnectionConfiguration>().AddOrUpdate(
                _ => _.Value,
                new ConnectionConfiguration
                {
                    MachineName = "ProdWeb",
                    Setting = "ClientManagementContext",
                    Type = "ConnectionString",
                    Value = "Data Source=(localdb)\v11.0;Initial Catalog=ClientManagementContext;Integrated Security=True",
                    Provider = "System.Data.SqlClient"
                }
            );

            context.Set<ConnectionConfiguration>().AddOrUpdate(
                _ => _.Value,
                new Model.Entities.ConnectionConfiguration
                {
                    MachineName = "DevWeb",
                    Setting = "ClientManagementContextEntities",
                    Type = "ConnectionString",
                    Value = "test", // metadata=res://*/Query.ClientManagementContextQueryEntities.csdl|res://*/Query.ClientManagementContextQueryEntities.ssdl|res://*/Query.ClientManagementContextQueryEntities.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=(localdb)\v11.0;initial catalog=ClientManagementContext;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;
                    Provider = "System.Data.EntityClient"
                }
            );

            context.Set<ConnectionConfiguration>().AddOrUpdate(
                _ => _.Value,
                new ConnectionConfiguration
                {
                    MachineName = "TestWeb",
                    Setting = "ClientManagementContextEntities",
                    Type = "ConnectionString",
                    Value = "test2", // "metadata=res://*/Query.ClientManagementContextQueryEntities.csdl|res://*/Query.ClientManagementContextQueryEntities.ssdl|res://*/Query.ClientManagementContextQueryEntities.msl", // "metadata=res://*/Query.ClientManagementContextQueryEntities.csdl|res://*/Query.ClientManagementContextQueryEntities.ssdl|res://*/Query.ClientManagementContextQueryEntities.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=(localdb)\v11.0;initial catalog=ClientManagementContext;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;",
                    Provider = "System.Data.EntityClient"
                }
            );

            context.Set<ConnectionConfiguration>().AddOrUpdate(
                _ => _.Value,
                new ConnectionConfiguration
                {
                    MachineName = "ProdWeb",
                    Setting = "ClientManagementContextEntities",
                    Type = "ConnectionString",
                    Value = "connectiontest3", //"metadata=res://*/Query.ClientManagementContextQueryEntities.csdl|res://*/Query.ClientManagementContextQueryEntities.ssdl|res://*/Query.ClientManagementContextQueryEntities.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=(localdb)\v11.0;initial catalog=ClientManagementContext;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;",
                    Provider = "System.Data.EntityClient"
                }
            );  
        }
    }
}



      //protected override void Seed(ClientManagementContext context)
      //  {
      //      //  This method will be called after migrating to the latest version.

      //      //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
      //      //  to avoid creating duplicate seed data. E.g.
      //      //
      //      //    context.People.AddOrUpdate(
      //      //      p => p.FullName,
      //      //      new Person { FullName = "Andrew Peters" },
      //      //      new Person { FullName = "Brice Lambson" },
      //      //      new Person { FullName = "Rowan Miller" }
      //      //    );
      //      //


      //      context.Set<ContactType>().AddOrUpdate(
      //          ct => ct.Name,
      //          new ContactType { Name = "consultant" },
      //          new ContactType { Name = "lawyer" }
      //      );

      //      context.Set<Tenant>().AddOrUpdate(
      //          _ => _.Name,
      //          new Tenant
      //          {
      //              AccountNumber = "123",
      //              Active = 1,
      //              BillingAddress = new Address()
      //              {
      //                  Street = "123 foo dr.",
      //                  City = "foo place",
      //                  State = "tx",
      //                  Zip = "123"
      //              },
      //              ContactTypeId = 1,
      //              Description = "foo immigration",
      //              Email = "foo@immigration.com",
      //              //ModifiedDate = DateTime.Now,
      //              Name = "foo",
      //              OfficePhone = "5551234561",
      //              PrimaryContactFirstName = "foo",
      //              PrimaryContactLastName = "bar",
      //              PrimaryContactPhone = "5556782341"
      //          }
      //      );

      //      context.Set<Tenant>().AddOrUpdate(
      //          _ => _.Name,
      //          new Tenant
      //          {
      //              AccountNumber = "234",
      //              Active = 1,
      //              BillingAddress = new Address()
      //              {
      //                  Street = "234 bar dr.",
      //                  City = "bar place",
      //                  State = "tx",
      //                  Zip = "234"
      //              },
      //              ContactTypeId = 1,
      //              Description = "bar immigration",
      //              Email = "bar@immigration.com",
      //              //ModifiedDate = DateTime.Now,
      //              Name = "bar",
      //              OfficePhone = "5556789122",
      //              PrimaryContactFirstName = "barbar",
      //              PrimaryContactLastName = "foo",
      //              PrimaryContactPhone = "5556789122"
      //          }
      //      );
      //  }