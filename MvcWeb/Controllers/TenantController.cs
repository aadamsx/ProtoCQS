using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ModelMap.Tenant;
using Repository;
using dm = DataModel;
using vm = ViewModel;

namespace MvcWeb.Controllers
{
    // most of the code I commented out below is auto-generated from the scafolding process
    public class TenantController : Controller
    {
        private readonly IReadRepository<dm.ContactType> _readTypes;
        private readonly IReadRepository<dm.Tenant> _readTenant;
        private readonly ICreateRepository<dm.Tenant> _createTenat;
        private readonly IUpdateRepository<dm.Tenant> _updateTenant;
        private readonly IDeleteRepository<dm.Tenant> _deleteTenant;

        // private ClientManagementContext db = new ClientManagementContext();
        // protected ITenantReadRepository Read { get; private set; }

        public TenantController(
            ICreateRepository<dm.Tenant> createTenant,
            IReadRepository<dm.Tenant> readTenant,
            IReadRepository<dm.ContactType> readTypes,
            IUpdateRepository<dm.Tenant> updateTenant,
            IDeleteRepository<dm.Tenant> deleteTenant)
        {
            _readTypes = readTypes;
            _readTenant = readTenant;
            _createTenat = createTenant;
            _updateTenant = updateTenant;
            _deleteTenant = deleteTenant;
        }

        // GET: /Tenant/
        public ActionResult Index()
        {
            // Auto-generated code:
            //var tenants1 = db.Tenants.Include(t => t.Type);
            //return View(tenants.ToList());

            var tenants = _readTenant.GetAll();
            return View(tenants.ToModels());
        }

        // GET: /Tenant/Details/5
        public ActionResult Details(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Tenant tenant = db.Tenants.Find(id);
            //if (tenant == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(tenant);
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var tenant = _readTenant.GetById(id);
            //var tuple = new Tuple<vm.Tenant, IEnumerable<vm.ContactType>>(tenant.ToModel(), items.ToModels());
            return View(tenant.ToModel());
        }

        // GET: /Tenant/Create
        public ActionResult Create()
        {
            //ViewBag.ContactTypeId = new SelectList(db.ContactTypes, "ContactTypeId", "Name");
            return View();
        }

        // POST: /Tenant/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            //[Bind(Include="TenantId,AccountNumber,Name,Active,PrimaryContactFirstName,PrimaryContactLastName,PrimaryContactPhone,Description,Email,OfficePhone,Street,City,State,Zip,RowGuid,LastModifiedBy,RowVersion,ContactTypeId")] 
            vm.Tenant tenant)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Tenants.Add(tenant);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //ViewBag.ContactTypeId = new SelectList(db.ContactTypes, "ContactTypeId", "Name", tenant.ContactTypeId);
            //return View(tenant);

            if (ModelState.IsValid)
            {
                _createTenat.Submit(tenant.ToModel());
                return RedirectToAction("Index");
            }

            // NOTE: see if the ID on the new Entity is in the TenantViewModel here.  
            // It is set after at the end of db.SaveChanges by assigning the value back to the command
            return View(tenant);
        }

        // GET: /Tenant/Edit/5
        public ActionResult Edit(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Tenant tenant = db.Tenants.Find(id);
            //if (tenant == null)
            //{
            //    return HttpNotFound();
            //}
            //ViewBag.ContactTypeId = new SelectList(db.ContactTypes, "ContactTypeId", "Name", tenant.ContactTypeId);
            //return View(tenant);

            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //var tenant = _readTenant.GetById(id);
            //var contactTypes = _readTypes.GetAll().ToList();
            //ViewBag.contactTypeModel = new SelectList(contactTypes, "Value", "Text");

            var vm = new vm.TenantEdit
            {
                Tenant = _readTenant.GetById(id).ToModel(),
                Types = new SelectList(_readTypes.GetAll(), "ContactTypeId", "Name")
            };

            return View(vm);
        }

        // POST: /Tenant/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            //[Bind(Include="TenantId,AccountNumber,Name,Active,PrimaryContactFirstName,PrimaryContactLastName,PrimaryContactPhone,Description,Email,OfficePhone,Street,City,State,Zip,RowGuid,LastModifiedBy,RowVersion,ContactTypeId")] 
            vm.Tenant tenant)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(tenant).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //ViewBag.ContactTypeId = new SelectList(db.ContactTypes, "ContactTypeId", "Name", tenant.ContactTypeId);
            //return View(tenant);

            try
            {
                if (ModelState.IsValid)
                {
                    //DataModel.Tenant tenant = tenant.ToModel();
                    //tenant.LastModifiedBy = User.Identity.Name;

                    _updateTenant.Submit(tenant.ToModel());
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
                tenant.RowVersion = databaseValues.RowVersion;
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

            var vm = new vm.TenantEdit
            {
                Tenant = tenant,
                Types = new SelectList(_readTypes.GetAll(), "ContactTypeId", "Name")
            };

            return View(vm);

        }

        // GET: /Tenant/Delete/5
        public ActionResult Delete(int? id, bool? concurrencyError)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //var tenant = db.Tenants.Find(id);
            //if (tenant == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(tenant.ToModel());

            //Entity Framework detects concurrency conflicts caused by someone else editing the 
            //tenant in a similar manner. When the HttpGet Delete method displays the confirmation 
            //view, the view includes the original RowVersion value in a hidden field. 
            //That value is then available to the HttpPost Delete method that's 
            //called when the user confirms the deletion.

            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var tenant = _readTenant.GetById(id);

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

            return tenant == null ? (ActionResult)HttpNotFound() : View(tenant.ToModel());
        }

        // POST: /Tenant/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    var tenant = db.Tenants.Find(id);
        //    db.Tenants.Remove(tenant);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(vm.Tenant tenant)
        {
            try
            {
                _deleteTenant.Submit(tenant.ToModel());
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
                return View(tenant);
            }
        }

        // Originally, the DbContext was newed up in this controller.  And the DbContext does not clean up after itself, so I'm guessing
        // this Disposed helped with that...  S.I. is handling the new of the DbContext, so I'm going to comment this out until I see
        // that this is an issue somehow.
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
