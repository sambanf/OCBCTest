using Asp.NETMVCCRUD.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asp.NETMVCCRUD.Controllers
{
    public class TransaksiController : Controller
    {
        // GET: Transaksi
        public ActionResult InputPage(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index", "Nasabah");
            }
            else
            {
                using (OCBCTestEntities1 db = new OCBCTestEntities1())
                {
                    tm_Nasabah emp = db.tm_Nasabah.Where(x => x.AccountID == id).FirstOrDefault<tm_Nasabah>();
                    return View(emp);
                }
            }
        }

        public ActionResult GetData(int id)
        {
            List<Transaksi> res = new List<Transaksi>();
            using (OCBCTestEntities1 db = new OCBCTestEntities1())
            {
                res = (from x in db.tt_Transaction
                       where x.AccountID == id
                       select new Transaksi
                       {
                           TransactionID = x.TransactionID,
                           AccountID = x.AccountID,
                           TransDate = x.TransDate.ToString(),
                           Description = x.Description,
                           DebitCreditStatus = x.DebitCreditStatus,
                           Amount = x.Amount
                       }).ToList();

            }
            return Json(new { data = res }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new tt_Transaction());
            else
            {
                using (OCBCTestEntities1 db = new OCBCTestEntities1())
                {
                    return View(db.tt_Transaction.Where(x => x.TransactionID == id).FirstOrDefault<tt_Transaction>());
                }
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(tt_Transaction emp)
        {
            using (OCBCTestEntities1 db = new OCBCTestEntities1())
            {
                if (emp.TransactionID == 0)
                {
                    db.tt_Transaction.Add(emp);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.Entry(emp).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }


        }

        public ActionResult Create()
        {
            return PartialView();
        }
    }
}