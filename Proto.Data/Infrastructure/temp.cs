//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Proto.Model.Entities;


//namespace Proto.Data.Repositories
//{

//    public class WriteRepository<TEntity> : IWriteRepository<TEntity> where TEntity : class
//    {
//        private ClientManagementContext context;

//        public WriteRepository(ClientManagementContext context)
//        {
//            if (context == null) throw new ArgumentNullException("context");
//            this.context = context;
//        }

//        private bool Save()
//        {
//            return context.SaveChanges() > 0;
//        }

//        public void Create(TEntity entity)
//        {
//            context.Entry(entity).State = EntityState.Added;
//            Save();
//        }

//        public void Update(TEntity entity)
//        {
//            context.Entry(entity).State = EntityState.Modified;
//            Save();
//        }

//        public void Delete(TEntity entity)
//        {
//            context.Entry(entity).State = EntityState.Deleted;
//            Save();
//        }

//        public class TenantController : Controller
//        {
//            private ClientManagementContext context;

//            public TenantController(ClientManagementContext context)
//            {
//                this.context = context;
//            }

//            public ActionResult Index()
//            {
//                var tenants = context.Tenants;

//                return View(tenants);
//            }

//            public ActionResult Details(int? id)
//            {
//                var tenant = context.Tenants.Find(id);
//                return tenant == null
//                    ? (ActionResult) HttpNotFound()
//                    : View(tenant);
//            }

//            public ActionResult Create()
//            {
//                return View();
//            }

//            public ActionResult Create(Tenant tenant)
//            {
//                if (ModelState.IsValid)
//                {
//                    context.Entry(tenant).State = EntityState.Added;
//                    context.SaveChanges();

//                    return RedirectToAction("Index");
//                }

//                return View(tenant);
//            }
//        }
//    }
//}
