using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Proto.Domain.CommandHandlers;
using Proto.Domain.Commands;
using Proto.Domain.Commands.Tenants;
using Proto.Domain.Queries.Tenants;
using Proto.Domain.QueryHandlers;
using Proto.Model.Entities;
using Proto.Mvc.Mgmt.Models;

namespace Proto.Mvc.Mgmt.Controllers
{
    public class TenantController : Controller
    {
        //private ClientManagementContextEntities db = new ClientManagementContextEntities();
        private readonly IQueryHandler<GetCurrentTenantsQuery, IQueryable<Tenant>> getCurrentTenantsHandler;
        private readonly IQueryHandler<GetTenantByIdQuery, Tenant> getTenantByIdHandler;
        private readonly ICommandHandler<CreateOrUpdateTenantCommand> createOrUpdateTenantHandler;
        private readonly ICommandHandler<DeleteTenantCommand> deleteTenantHandler;

        public TenantController(
            IQueryHandler<GetCurrentTenantsQuery, IQueryable<Tenant>> getcurrenttenants,
            IQueryHandler<GetTenantByIdQuery, Tenant> gettenantbyidquery,
            ICommandHandler<CreateOrUpdateTenantCommand> createorupdatetenanthandler,
            ICommandHandler<DeleteTenantCommand> deletetenanthandler)
        {
            getCurrentTenantsHandler = getcurrenttenants;
            getTenantByIdHandler = gettenantbyidquery;
            createOrUpdateTenantHandler = createorupdatetenanthandler;
            deleteTenantHandler = deletetenanthandler;
        }

        // GET: /TenantManagement/
        public ActionResult Index()
        {
            var query = new GetCurrentTenantsQuery { PageIndex = 1, PageSize = 10 };
            var tenants = getCurrentTenantsHandler.Handle(query);
            return View(Mapper.Map<IQueryable<Tenant>, IEnumerable<TenantViewModel>>(tenants));
        }

        // GET: /TenantManagement/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var tenant = getTenantByIdHandler.Handle(new GetTenantByIdQuery { TenantId = (int)id });
            return tenant == null
                ? (ActionResult) HttpNotFound()
                : View(Mapper.Map<Tenant, TenantViewModel>(tenant));
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
        public ActionResult Create(TenantViewModel tenant)
        {
            if (ModelState.IsValid)
             {
                var command = Mapper.Map<TenantViewModel, CreateOrUpdateTenantCommand>(tenant);
                createOrUpdateTenantHandler.Handle(command);

                return RedirectToAction("Index");
            }

            // NOTE: see if the ID on the new Entity is in the TenantViewModel here.  
            // It is set after at the end of db.SaveChanges by assigning the value back to the command
            return View(tenant);
        }

        // GET: /TenantManagement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var tenant = getTenantByIdHandler.Handle(new GetTenantByIdQuery { TenantId = (int)id });
            return tenant == null
                ? (ActionResult)HttpNotFound()
                : View(Mapper.Map<Tenant, TenantViewModel>(tenant));
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
            TenantViewModel tenant)
        {
            if (ModelState.IsValid)
            {
                var command = Mapper.Map<TenantViewModel, CreateOrUpdateTenantCommand>(tenant);
                command.LastModifiedBy = User.Identity.Name;
                createOrUpdateTenantHandler.Handle(command);

                if (command.SaveFailed && command.ConcurrencyException)
                {
                    if (command.DatabaseValues.Name != command.ClientValues.Name)
                        ModelState.AddModelError("Name", "Current value: "
                            + command.DatabaseValues.Name);

                    if (command.DatabaseValues.Active != command.ClientValues.Active)
                        ModelState.AddModelError("Active", "Current value: "
                            + command.DatabaseValues.Active);

                    if (command.DatabaseValues.PrimaryContactFirstName != command.ClientValues.PrimaryContactFirstName)
                        ModelState.AddModelError("PrimaryContactFirstName", "Current value: "
                            + command.DatabaseValues.PrimaryContactFirstName);

                    if (command.DatabaseValues.PrimaryContactLastName != command.ClientValues.PrimaryContactLastName)
                        ModelState.AddModelError("PrimaryContactLastName", "Current value: "
                            + command.DatabaseValues.PrimaryContactLastName);

                    if (command.DatabaseValues.PrimaryContactPhone != command.ClientValues.PrimaryContactPhone)
                        ModelState.AddModelError("PrimaryContactPhone", "Current value: "
                            + command.DatabaseValues.PrimaryContactPhone);

                    if (command.DatabaseValues.Description != command.ClientValues.Description)
                        ModelState.AddModelError("Description", "Current value: "
                            + command.DatabaseValues.Description);

                    if (command.DatabaseValues.Email != command.ClientValues.Email)
                        ModelState.AddModelError("Email", "Current value: "
                            + command.DatabaseValues.Email);

                    if (command.DatabaseValues.OfficePhone != command.ClientValues.OfficePhone)
                        ModelState.AddModelError("OfficePhone", "Current value: "
                            + command.DatabaseValues.OfficePhone);

                    if (command.DatabaseValues.BillingAddress.City != command.ClientValues.BillingAddress.City)
                        ModelState.AddModelError("City", "Current value: "
                            + command.DatabaseValues.BillingAddress.City);

                    if (command.DatabaseValues.BillingAddress.State != command.ClientValues.BillingAddress.State)
                        ModelState.AddModelError("State", "Current value: "
                            + command.DatabaseValues.BillingAddress.State);

                    if (command.DatabaseValues.BillingAddress.Street != command.ClientValues.BillingAddress.Street)
                        ModelState.AddModelError("Street", "Current value: "
                            + command.DatabaseValues.BillingAddress.Street);

                    if (command.DatabaseValues.BillingAddress.Zip != command.ClientValues.BillingAddress.Zip)
                        ModelState.AddModelError("Zip", "Current value: "
                            + command.DatabaseValues.BillingAddress.Zip);

                    ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                        + "was modified by another user after you got the original value. The "
                        + "edit operation was canceled and the current values in the database "
                        + "have been displayed. If you still want to edit this record, click "
                        + "the Save button again. Otherwise click the Back to List hyperlink.");

                    tenant.RowVersion = command.RowVersion;

                    return View(tenant);
                }
                else if (command.SaveFailed)
                {
                    //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                    ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
                    
                    return View(tenant);
                }

                return RedirectToAction("Index");
            }
            return View(tenant);
        }

        // GET: /TenantManagement/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var tenant = getTenantByIdHandler.Handle(new GetTenantByIdQuery { TenantId = (int)id });
            return tenant == null
                ? (ActionResult)HttpNotFound()
                : View(Mapper.Map<Tenant, TenantViewModel>(tenant));
        }

        // POST: /TenantManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var command = new DeleteTenantCommand {TenantId = id};
            deleteTenantHandler.Handle(command);
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    //db.Dispose();
        //    base.Dispose(disposing);
        //}
    }
}