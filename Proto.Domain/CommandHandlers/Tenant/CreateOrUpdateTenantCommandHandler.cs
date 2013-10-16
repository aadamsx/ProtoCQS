using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using Proto.Data;
using Proto.Data.Infrastructure;
using Proto.Domain.Commands.Tenants;
using Proto.Model.Entities;


namespace Proto.Domain.CommandHandlers.Tenant
{
    public class CreateOrUpdateTenantCommandHandler : ICommandHandler<CreateOrUpdateTenantCommand>
    {
        private ClientManagementContext db;
        public CreateOrUpdateTenantCommandHandler(ClientManagementContext context)
        {
            db = context;
        }

        void ICommandHandler<CreateOrUpdateTenantCommand>.Handle(CreateOrUpdateTenantCommand command)
        {
            var tenant = new Model.Entities.Tenant
            {
                TenantId = command.TenantId,
                Name = command.Name,
                //public int? Type { get; set; }
                //public TenantType Type { get; set; }
                Active = command.Active,
                PrimaryContactFirstName = command.PrimaryContactFirstName,
                PrimaryContactLastName = command.PrimaryContactLastName,
                PrimaryContactPhone = command.PrimaryContactPhone,
                Description = command.Description,
                Email = command.Email,
                OfficePhone = command.OfficePhone,
                BillingAddress = new Address
                {
                    City = command.City,
                    State = command.State,
                    Street = command.Street,
                    Zip = command.Zip
                },

                RowVersion = command.RowVersion,

                //RowGuid
                // Relations and Navigation
                // Note: this should be a drop down
                ContactTypeId = command.ContactTypeId
                //ContactType Type { get; set; }
            };

            // A) Optimistic.

            // Cheap writes:
            // you can make these kinds of don't-care writes cheaper by using a different Context variation
            // just disabling some extra calls EF makes on your behalf that you're ignoring the results of anyway
            db.Configuration.AutoDetectChangesEnabled = false;
            db.Configuration.ValidateOnSaveEnabled = false;

            // Enable retry logic for certain operations
            //ContextConfiguration.SuspendExecutionStrategy = false;

            // unsure whether an object has been added or modified
            // You need to take these actions on all of the objects being added/modified, 
            // so if this object is complex and has other objects that need to be updated 
            // in the DB via FK relationships, you need to set their EntityState as well.
            //db.Entry(tenant).State = tenant.TenantId == 0
            //    ? EntityState.Added
            //    : EntityState.Modified;

            if (tenant.TenantId == 0)
            {
                // should allow the database to create this
                tenant.RowGuid = Guid.NewGuid();

                // also need the account number to be generated

                // should i set the state or ...
                //db.Entry(tenant).State = EntityState.Added;

                // ... should I add like this?  what's the difference?
                db.Tenants.Add(tenant);
            }
            else
            {
                //db.Entry(tenant).Property(x => x.RowVersion).OriginalValue = tenant.RowVersion;

                db.Entry(tenant).State = EntityState.Modified;

                // Concurrency rowversion
                //tenant.TimeStamp = DateTime.Now,

            }

            // resolving optimistic concurrency exceptions with reload (custom resolution)
            command.SaveFailed = false;

            try
            {
                db.SaveChanges();
            }
            catch (NullReferenceException)
            {
                command.SaveFailed = true;
                command.NullRefException = true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                command.SaveFailed = true;
                command.ConcurrencyException = true;
                // Get the current entity values and the values in the database
                // as instances of the entity type
                var entry = ex.Entries.Single();
                command.ClientValues = (Model.Entities.Tenant) entry.Entity;
                command.DatabaseValues = (Model.Entities.Tenant) entry.GetDatabaseValues().ToObject();
                command.RowVersion = command.DatabaseValues.RowVersion;
            }
            catch (DbUpdateException)
            {
                command.SaveFailed = true;
                command.UpdateException = true;
            }
            catch (DataException)
            {
                command.SaveFailed = true;
                command.DataException = true;
            }
            catch (Exception)
            {
                command.SaveFailed = true;
                command.OtherException = true;
            }
            //catch (RetryLimitExceeded)
            //{
                
            //}
            finally
            {
                //ContextConfiguration.SuspendExecutionStrategy = true;
            }





        // B) Pessimistic. You can actually query the DB to verify the data 
            // hasn't changed/been added since you last picked it up, 
            // then update it if it's safe.

            // var existing = db.Customers.Find(customer.Id);
            // Some logic here to decide whether updating is a good idea, like
            // verifying selected values haven't changed, then

            // db.Entry(existing).CurrentValues.SetValues(customer);

            // Set output properties
            command.TenantId = tenant.TenantId;
            //command.RowGuid = tenant.RowGuid;
            //command.RowVersion = tenant.RowVersion;
            command.AccountNumber = tenant.AccountNumber;
        }
    }
}


//namespace Proto.Domain.CommandHandlers.TenantV1
//{
//    public class CreateOrUpdateTenantCommandHandler : ICommandHandler<CreateOrUpdateTenantCommand>
//    {
//        private ClientManagementContext db;
//        public CreateOrUpdateTenantCommandHandler(ClientManagementContext context)
//        {
//            db = context;
//        }

//        void ICommandHandler<CreateOrUpdateTenantCommand>.Handle(CreateOrUpdateTenantCommand command)
//        {
//            var tenant = new Model.Entities.Tenant
//            {
//                TenantId = command.TenantId,
//                Name = command.Name,
//                //public int? Type { get; set; }
//                //public TenantType Type { get; set; }
//                Active = command.Active,
//                PrimaryContactFirstName = command.PrimaryContactFirstName,
//                PrimaryContactLastName = command.PrimaryContactLastName,
//                PrimaryContactPhone = command.PrimaryContactPhone,
//                Description = command.Description,
//                Email = command.Email,
//                OfficePhone = command.OfficePhone,
//                BillingAddress = new Address
//                {
//                    City = command.City, 
//                    State = command.State, 
//                    Street = command.Street, 
//                    Zip = command.Zip
//                },

//                RowVersion =  command.RowVersion,

//                //RowGuid
//                // Relations and Navigation
//                // Note: this should be a drop down
//                ContactTypeId = command.ContactTypeId
//                //ContactType Type { get; set; }
//            };

//            // A) Optimistic.

//            // Cheap writes:
//            // you can make these kinds of don't-care writes cheaper by using a different Context variation
//            // just disabling some extra calls EF makes on your behalf that you're ignoring the results of anyway
//            db.Configuration.AutoDetectChangesEnabled = false;
//            db.Configuration.ValidateOnSaveEnabled = false;
            
//            // unsure whether an object has been added or modified
//            // You need to take these actions on all of the objects being added/modified, 
//            // so if this object is complex and has other objects that need to be updated 
//            // in the DB via FK relationships, you need to set their EntityState as well.
//            //db.Entry(tenant).State = tenant.TenantId == 0
//            //    ? EntityState.Added
//            //    : EntityState.Modified;

//            if (tenant.TenantId == 0)
//            {
//                // should allow the database to create this
//                tenant.RowGuid = Guid.NewGuid();
                
//                // also need the account number to be generated

//                // should i set the state or ...
//                //db.Entry(tenant).State = EntityState.Added;

//                // ... should I add like this?  what's the difference?
//                db.Tenants.Add(tenant);
//            }
//            else
//            {
//                //db.Entry(tenant).Property(x => x.RowVersion).OriginalValue = tenant.RowVersion;

//                db.Entry(tenant).State = EntityState.Modified;

//                // Concurrency rowversion
//                //tenant.TimeStamp = DateTime.Now,

//            }

//            // resolving optimistic concurrency exceptions with reload (custom resolution)
//            bool saveFailed;
//            do
//            {
//                saveFailed = false;

//                try
//                {
//                    db.SaveChanges();
//                }
//                catch (DbUpdateConcurrencyException ex)
//                {
//                    saveFailed = true;

//                    // Get the current entity values and the values in the database
//                    // as instances of the entity type
//                    var entry = ex.Entries.Single();
//                    var databaseValues = entry.GetDatabaseValues();
//                    var databaseValuesAsTenant = (Model.Entities.Tenant)databaseValues.ToObject();

//                    // Choose an initial set of resolved values. In this case we
//                    // make the default be the values currently in the database.
//                    var resolvedValuesAsTenant = (Model.Entities.Tenant)databaseValues.ToObject();

//                    // Have the user choose what the resolved values should be
//                    HaveUserResolveConcurrency((Model.Entities.Tenant)entry.Entity,
//                                               databaseValuesAsTenant,
//                                               resolvedValuesAsTenant);

//                    // Update the original values with the database values and
//                    // the current values with whatever the user choose.
//                    entry.OriginalValues.SetValues(databaseValues);
//                    entry.CurrentValues.SetValues(resolvedValuesAsTenant);
//                }
//            } while (saveFailed);





//            // B) Pessimistic. You can actually query the DB to verify the data 
//            // hasn't changed/been added since you last picked it up, 
//            // then update it if it's safe.

//            // var existing = db.Customers.Find(customer.Id);
//            // Some logic here to decide whether updating is a good idea, like
//            // verifying selected values haven't changed, then

//            // db.Entry(existing).CurrentValues.SetValues(customer);

//            // Set output properties
//            command.TenantId = tenant.TenantId;
//            command.RowGuid = tenant.RowGuid;
//            command.RowVersion = tenant.RowVersion;
//            command.AccountNumber = tenant.AccountNumber;
//        }

//        private void HaveUserResolveConcurrency(
//            Model.Entities.Tenant entity, 
//            Model.Entities.Tenant databaseValues, 
//            Model.Entities.Tenant resolvedValues)
//        {
//            // Show the current, database, and resolved values to the user and have
//            // them update the resolved values to get the correct resolution.

//            //if (databaseValues.Name != resolvedValues.Name)
//            //    ModelState.AddModelError("Name", "Current value: "
//            //        + databaseValues.Name);

//            throw new NotImplementedException();
//        }
//    }
//}


// A good way to simulate a concurrency exception is to set a breakpoint on the SaveChanges 
// call and then modify an entity that is being saved in the database using another tool 
// such as SQL Management Studio. You could also insert a line before SaveChanges to update 
// the database directly using SqlCommand. For example:

//context.Database.SqlCommand(
//    "UPDATE dbo.Blogs SET Name = 'Another Name' WHERE BlogId = 1");