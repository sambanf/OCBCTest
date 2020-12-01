using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asp.NETMVCCRUD.Models;
using System.Data.Entity;

namespace Asp.NETMVCCRUD.Controllers
{
    public class NasabahController : Controller
    {
        //
        // GET: /tm_Operator/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            using (OCBCTestEntities1 db = new OCBCTestEntities1())
            {
                List<tm_Nasabah> empList = db.tm_Nasabah.Where(x => x.Status_FK == 1).ToList<tm_Nasabah>();
                return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new tm_Nasabah());
            else
            {
                using (OCBCTestEntities1 db = new OCBCTestEntities1())
                {
                    return View(db.tm_Nasabah.Where(x => x.AccountID==id).FirstOrDefault<tm_Nasabah>());
                }
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(tm_Nasabah emp)
        {
            using (OCBCTestEntities1 db = new OCBCTestEntities1())
            {
                if (emp.Status_FK == 0)
                {
                    emp.Status_FK = 1;
                    db.tm_Nasabah.Add(emp);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else {
                    db.Entry(emp).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }


        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (OCBCTestEntities1 db = new OCBCTestEntities1())
            {
                tm_Nasabah emp = db.tm_Nasabah.Where(x => x.AccountID == id).FirstOrDefault<tm_Nasabah>();
                emp.Status_FK = 2;
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}