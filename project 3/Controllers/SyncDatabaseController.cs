using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static project_3.Models.Sync;

namespace project_3.Controllers
{
    public class SyncDatabaseController : Controller
    {
        // GET: SyncDatabase
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Upload()
        {
            _cSynchronization sync = new _cSynchronization();
            sync._MSync();
            return View("Index");
        }
        public ActionResult Download()
        {
            _cSynchronization sync = new _cSynchronization();
            sync._MSyncD();
            return View("Index");
        }
        public ActionResult BackUp()
        {
            return View("Index");
        }
    }
}