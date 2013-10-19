using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using Repository;

namespace Proto.Mvc.Mgmt.Controllers
{
    /// <summary>
    /// There are no complete LINQ to SQL implementations. 
    /// They all are either missing features or implement things like 
    /// eager/lazy loading in their own way. That means that they all are 
    /// leaky abstractions. So if you expose LINQ outside your repository 
    /// you get a leaky abstraction. You could really stop using the 
    /// repository pattern then and use the OR/M directly.
    /// http://blog.gauffin.org/2013/01/repository-pattern-done-right/#.UmBpPFCfgSQ
    /// </summary>
    public class TenantController : Controller
    {
        public IReadRepository<DataModel.Tenant> ReadRepository { get; set; }
        public IWriteRepository<DataModel.Tenant> WriteRepository { get; set; }

        public TenantController(
            IReadRepository<DataModel.Tenant> readRepository,
            IWriteRepository<DataModel.Tenant> writeRepository)
        {
            ReadRepository = readRepository;
            WriteRepository = writeRepository;
        }

        // GET: /TenantManagement/
        public ActionResult Index()
        {
            //var query = new GetCurrentTenantsQuery { PageIndex = 1, PageSize = 10 };
            var tenants = ReadRepository.GetAll();
            return View(Mapper.Map<IEnumerable<DataModel.Tenant>, IEnumerable<ViewModel.Tenant>>(tenants));
        }

        // GET: /TenantManagement/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var tenant = ReadRepository.GetById(id);
            //var tenant = getTenantByIdHandler.Handle(new GetTenantByIdQuery { TenantId = (int)id });
            return tenant == null
                ? (ActionResult)HttpNotFound()
                : View(Mapper.Map<DataModel.Tenant, ViewModel.Tenant>(tenant));
        }

        // GET: /TenantManagement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /TenantManagement/Create
        // To protect from over posting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // 
        // Example: public ActionResult Update([Bind(Include="ExampleProperty1,ExampleProperty2")] Model model)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewModel.Tenant tenantVm)
        {
            if (ModelState.IsValid)
            {
                WriteRepository.Create(Mapper.Map<ViewModel.Tenant, DataModel.Tenant>(tenantVm));
                return RedirectToAction("Index");
            }

            // NOTE: see if the ID on the new Entity is in the TenantViewModel here.  
            // It is set after at the end of db.SaveChanges by assigning the value back to the command
            return View(tenantVm);
        }

        // GET: /TenantManagement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var tenant = ReadRepository.GetById(id);
            return tenant == null
                ? (ActionResult)HttpNotFound()
                : View(Mapper.Map<DataModel.Tenant, ViewModel.Tenant>(tenant));
        }

        // POST: /TenantManagement/Edit/5
        // To protect from over posting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // 
        // Example: public ActionResult Update([Bind(Include="ExampleProperty1,ExampleProperty2")] Model model)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            //[Bind(Include = "TenantId, Name, PrimaryContactFirstName, PrimaryContactLastName, " + 
            //    "PrimaryContactPhone, Description, Email, OfficePhone, RowVersion")]
            ViewModel.Tenant tenantVm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var tenant = Mapper.Map<ViewModel.Tenant, DataModel.Tenant>(tenantVm);
                    tenant.LastModifiedBy = User.Identity.Name;

                    WriteRepository.Update(tenant);
                    return RedirectToAction("Index");
                }
            }
            catch (NullReferenceException)
            {
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Get the current entity values and the values in the database
                // as instances of the entity type
                var entry = ex.Entries.Single();
                var clientValues = (DataModel.Tenant)entry.Entity;
                var databaseValues = (DataModel.Tenant)entry.GetDatabaseValues().ToObject();

                if (databaseValues.Name != clientValues.Name)
                    ModelState.AddModelError("Name", "Current value: "
                        + databaseValues.Name);

                if (databaseValues.Active != clientValues.Active)
                    ModelState.AddModelError("Active", "Current value: "
                        + databaseValues.Active);

                if (databaseValues.PrimaryContactFirstName != clientValues.PrimaryContactFirstName)
                    ModelState.AddModelError("PrimaryContactFirstName", "Current value: "
                        + databaseValues.PrimaryContactFirstName);

                if (databaseValues.PrimaryContactLastName != clientValues.PrimaryContactLastName)
                    ModelState.AddModelError("PrimaryContactLastName", "Current value: "
                        + databaseValues.PrimaryContactLastName);

                if (databaseValues.PrimaryContactPhone != clientValues.PrimaryContactPhone)
                    ModelState.AddModelError("PrimaryContactPhone", "Current value: "
                        + databaseValues.PrimaryContactPhone);

                if (databaseValues.Description != clientValues.Description)
                    ModelState.AddModelError("Description", "Current value: "
                        + databaseValues.Description);

                if (databaseValues.Email != clientValues.Email)
                    ModelState.AddModelError("Email", "Current value: "
                        + databaseValues.Email);

                if (databaseValues.OfficePhone != clientValues.OfficePhone)
                    ModelState.AddModelError("OfficePhone", "Current value: "
                        + databaseValues.OfficePhone);

                if (databaseValues.City != clientValues.City)
                    ModelState.AddModelError("City", "Current value: "
                        + databaseValues.City);

                if (databaseValues.State != clientValues.State)
                    ModelState.AddModelError("State", "Current value: "
                        + databaseValues.State);

                if (databaseValues.Street != clientValues.Street)
                    ModelState.AddModelError("Street", "Current value: "
                        + databaseValues.Street);

                if (databaseValues.Zip != clientValues.Zip)
                    ModelState.AddModelError("Zip", "Current value: "
                        + databaseValues.Zip);

                ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                    + "was modified by another user after you got the original value. The "
                    + "edit operation was canceled and the current values in the database "
                    + "have been displayed. If you still want to edit this record, click "
                    + "the Save button again. Otherwise click the Back to List hyperlink.");

                // see if this works now...
                tenantVm.RowVersion = databaseValues.RowVersion;
            }
            catch (DbUpdateException)
            {
            }
            catch (DataException)
            {
                // Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Oops! Something went wrong, we'll try out best to fix it. Try again, and if the problem persists contact your system administrator.");
            }

            return View(tenantVm);
        }

        // GET: /TenantManagement/Delete/5
        public ActionResult Delete(int? id, bool? concurrencyError)
        {
             //Entity Framework detects concurrency conflicts caused by someone else editing the 
             //tenant in a similar manner. When the HttpGet Delete method displays the confirmation 
             //view, the view includes the original RowVersion value in a hidden field. 
             //That value is then available to the HttpPost Delete method that's 
             //called when the user confirms the deletion.
            
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var tenant = ReadRepository.GetById(id);
            
            if (concurrencyError.GetValueOrDefault())
            {
                if (tenant == null)
                {
                    ViewBag.ConcurrencyErrorMessage = "The record you attempted to delete "
                        + "was deleted by another user after you got the original values. "
                        + "Click the Back to List hyperlink.";
                }
                else
                {
                    ViewBag.ConcurrencyErrorMessage = "The record you attempted to delete "
                        + "was modified by another user after you got the original values. "
                        + "The delete operation was canceled and the current values in the "
                        + "database have been displayed. If you still want to delete this "
                        + "record, click the Delete button again. Otherwise "
                        + "click the Back to List hyperlink.";
                }
            }

            return tenant == null
                ? (ActionResult)HttpNotFound()
                : View(Mapper.Map<DataModel.Tenant, ViewModel.Tenant>(tenant));
        }


        // http://www.asp.net/mvc/tutorials/getting-started-with-ef-using-mvc/handling-concurrency-with-the-entity-framework-in-an-asp-net-mvc-application
        // POST: /TenantManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ViewModel.Tenant tenantVm)
        {
            try
            {
                WriteRepository.Delete(Mapper.Map<ViewModel.Tenant, DataModel.Tenant>(tenantVm));
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                return RedirectToAction("Delete", new { concurrencyError = true });
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError(string.Empty, "Unable to delete. Try again, and if the problem persists contact your system administrator.");
                return View(tenantVm);
            }
        }

        //// POST: /TenantManagement/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    // shouldn't get it twice.
        //    var tenant = ReadRepository.GetById(id);
        //    WriteRepository.Delete(tenant);
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    //db.Dispose();
        //    base.Dispose(disposing);
        //}
    }
}


namespace Proto.Mvc.Mgmt.ControllersV1
{
    //public class TenantController : Controller
    //{
    //    //private ClientManagementContextEntities db = new ClientManagementContextEntities();
    //    private readonly IQueryHandler<GetCurrentTenantsQuery, IQueryable<Tenant>> getCurrentTenantsHandler;
    //    private readonly IQueryHandler<GetTenantByIdQuery, Tenant> getTenantByIdHandler;
    //    private readonly ICommandHandler<CreateOrUpdateTenantCommand> createOrUpdateTenantHandler;
    //    private readonly ICommandHandler<DeleteTenantCommand> deleteTenantHandler;

    //    public TenantController(
    //        IQueryHandler<GetCurrentTenantsQuery, IQueryable<Tenant>> getcurrenttenants,
    //        IQueryHandler<GetTenantByIdQuery, Tenant> gettenantbyidquery,
    //        ICommandHandler<CreateOrUpdateTenantCommand> createorupdatetenanthandler,
    //        ICommandHandler<DeleteTenantCommand> deletetenanthandler)
    //    {
    //        getCurrentTenantsHandler = getcurrenttenants;
    //        getTenantByIdHandler = gettenantbyidquery;
    //        createOrUpdateTenantHandler = createorupdatetenanthandler;
    //        deleteTenantHandler = deletetenanthandler;
    //    }

    //    // GET: /TenantManagement/
    //    public ActionResult Index()
    //    {
    //        var query = new GetCurrentTenantsQuery { PageIndex = 1, PageSize = 10 };
    //        var tenants = getCurrentTenantsHandler.Handle(query);
    //        return View(Mapper.Map<IQueryable<Tenant>, IEnumerable<TenantViewModel>>(tenants));
    //    }

    //    // GET: /TenantManagement/Details/5
    //    public ActionResult Details(int? id)
    //    {
    //        if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //        var tenant = getTenantByIdHandler.Handle(new GetTenantByIdQuery { TenantId = (int)id });
    //        return tenant == null
    //            ? (ActionResult) HttpNotFound()
    //            : View(Mapper.Map<Tenant, TenantViewModel>(tenant));
    //    }

    //    // GET: /TenantManagement/Create
    //    public ActionResult Create()
    //    {
    //        return View();
    //    }

    //    // POST: /TenantManagement/Create
    //    // To protect from over posting attacks, please enable the specific properties you want to bind to, for 
    //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    //    // 
    //    // Example: public ActionResult Update([Bind(Include="ExampleProperty1,ExampleProperty2")] Model model)
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult Create(TenantViewModel tenant)
    //    {
    //        if (ModelState.IsValid)
    //         {
    //            var command = Mapper.Map<TenantViewModel, CreateOrUpdateTenantCommand>(tenant);
    //            createOrUpdateTenantHandler.Handle(command);

    //            return RedirectToAction("Index");
    //        }

    //        // NOTE: see if the ID on the new Entity is in the TenantViewModel here.  
    //        // It is set after at the end of db.SaveChanges by assigning the value back to the command
    //        return View(tenant);
    //    }

    //    // GET: /TenantManagement/Edit/5
    //    public ActionResult Edit(int? id)
    //    {
    //        if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //        var tenant = getTenantByIdHandler.Handle(new GetTenantByIdQuery { TenantId = (int)id });
    //        return tenant == null
    //            ? (ActionResult)HttpNotFound()
    //            : View(Mapper.Map<Tenant, TenantViewModel>(tenant));
    //    }

    //    // POST: /TenantManagement/Edit/5
    //    // To protect from over posting attacks, please enable the specific properties you want to bind to, for 
    //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    //    // 
    //    // Example: public ActionResult Update([Bind(Include="ExampleProperty1,ExampleProperty2")] Model model)
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult Edit(
    //        //[Bind(Include = "TenantId, Name, PrimaryContactFirstName, PrimaryContactLastName, " + 
    //        //    "PrimaryContactPhone, Description, Email, OfficePhone, RowVersion")]
    //        TenantViewModel tenant)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            var command = Mapper.Map<TenantViewModel, CreateOrUpdateTenantCommand>(tenant);
    //            command.LastModifiedBy = User.Identity.Name;
    //            createOrUpdateTenantHandler.Handle(command);

    //            if (command.SaveFailed && command.ConcurrencyException)
    //            {
    //                if (command.DatabaseValues.Name != command.ClientValues.Name)
    //                    ModelState.AddModelError("Name", "Current value: "
    //                        + command.DatabaseValues.Name);

    //                if (command.DatabaseValues.Active != command.ClientValues.Active)
    //                    ModelState.AddModelError("Active", "Current value: "
    //                        + command.DatabaseValues.Active);

    //                if (command.DatabaseValues.PrimaryContactFirstName != command.ClientValues.PrimaryContactFirstName)
    //                    ModelState.AddModelError("PrimaryContactFirstName", "Current value: "
    //                        + command.DatabaseValues.PrimaryContactFirstName);

    //                if (command.DatabaseValues.PrimaryContactLastName != command.ClientValues.PrimaryContactLastName)
    //                    ModelState.AddModelError("PrimaryContactLastName", "Current value: "
    //                        + command.DatabaseValues.PrimaryContactLastName);

    //                if (command.DatabaseValues.PrimaryContactPhone != command.ClientValues.PrimaryContactPhone)
    //                    ModelState.AddModelError("PrimaryContactPhone", "Current value: "
    //                        + command.DatabaseValues.PrimaryContactPhone);

    //                if (command.DatabaseValues.Description != command.ClientValues.Description)
    //                    ModelState.AddModelError("Description", "Current value: "
    //                        + command.DatabaseValues.Description);

    //                if (command.DatabaseValues.Email != command.ClientValues.Email)
    //                    ModelState.AddModelError("Email", "Current value: "
    //                        + command.DatabaseValues.Email);

    //                if (command.DatabaseValues.OfficePhone != command.ClientValues.OfficePhone)
    //                    ModelState.AddModelError("OfficePhone", "Current value: "
    //                        + command.DatabaseValues.OfficePhone);

    //                if (command.DatabaseValues.BillingAddress.City != command.ClientValues.BillingAddress.City)
    //                    ModelState.AddModelError("City", "Current value: "
    //                        + command.DatabaseValues.BillingAddress.City);

    //                if (command.DatabaseValues.BillingAddress.State != command.ClientValues.BillingAddress.State)
    //                    ModelState.AddModelError("State", "Current value: "
    //                        + command.DatabaseValues.BillingAddress.State);

    //                if (command.DatabaseValues.BillingAddress.Street != command.ClientValues.BillingAddress.Street)
    //                    ModelState.AddModelError("Street", "Current value: "
    //                        + command.DatabaseValues.BillingAddress.Street);

    //                if (command.DatabaseValues.BillingAddress.Zip != command.ClientValues.BillingAddress.Zip)
    //                    ModelState.AddModelError("Zip", "Current value: "
    //                        + command.DatabaseValues.BillingAddress.Zip);

    //                ModelState.AddModelError(string.Empty, "The record you attempted to edit "
    //                    + "was modified by another user after you got the original value. The "
    //                    + "edit operation was canceled and the current values in the database "
    //                    + "have been displayed. If you still want to edit this record, click "
    //                    + "the Save button again. Otherwise click the Back to List hyperlink.");

    //                tenant.RowVersion = command.RowVersion;

    //                return View(tenant);
    //            }
    //            else if (command.SaveFailed)
    //            {
    //                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
    //                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
                    
    //                return View(tenant);
    //            }

    //            return RedirectToAction("Index");
    //        }
    //        return View(tenant);
    //    }

    //    // GET: /TenantManagement/Delete/5
    //    public ActionResult Delete(int? id)
    //    {
    //        if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //        var tenant = getTenantByIdHandler.Handle(new GetTenantByIdQuery { TenantId = (int)id });
    //        return tenant == null
    //            ? (ActionResult)HttpNotFound()
    //            : View(Mapper.Map<Tenant, TenantViewModel>(tenant));
    //    }

    //    // POST: /TenantManagement/Delete/5
    //    [HttpPost, ActionName("Delete")]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult DeleteConfirmed(int id)
    //    {
    //        var command = new DeleteTenantCommand {TenantId = id};
    //        deleteTenantHandler.Handle(command);
    //        return RedirectToAction("Index");
    //    }

    //    //protected override void Dispose(bool disposing)
    //    //{
    //    //    //db.Dispose();
    //    //    base.Dispose(disposing);
    //    //}
    //}
}