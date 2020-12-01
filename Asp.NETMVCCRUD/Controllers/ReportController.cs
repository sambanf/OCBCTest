using Asp.NETMVCCRUD.Class;
using Asp.NETMVCCRUD.Models;
using ClosedXML.Excel;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Web;
using System.Web.Mvc;

namespace Asp.NETMVCCRUD.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report

        //Detail Waving
        public ActionResult BukuNasabah()
        {
            //List<DDLOperator> result = new List<DDLOperator>();
            //using (OCBCTestEntities1 db = new OCBCTestEntities1())
            //{
            //    result = (from oper in db.tm_Operator
            //              where oper.Status_FK == 1
            //              select new DDLOperator
            //              {
            //                  operatorpk = oper.Operator_PK,
            //                  Text = oper.NoOperator + " - " + oper.Nama
            //              }).ToList();
            //    ViewBag.OperatorList = new SelectList(result, "operatorpk", "Text");

            return View();
        }
        public ActionResult GetData(ReportProperty rp)
        {
            List<Transaksi> result = new List<Transaksi>();
            using (OCBCTestEntities1 db = new OCBCTestEntities1())
            {
                result = (from x in db.tt_Transaction
                          where x.AccountID == rp.Operator && (x.TransDate >= rp.startdate && x.TransDate <= rp.enddate)
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
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Poin()
        {
            //List<DDLOperator> result = new List<DDLOperator>();
            //using (OCBCTestEntities1 db = new OCBCTestEntities1())
            //{
            //    result = (from oper in db.tm_Operator
            //              where oper.Status_FK == 1
            //              select new DDLOperator
            //              {
            //                  operatorpk = oper.Operator_PK,
            //                  Text = oper.NoOperator + " - " + oper.Nama
            //              }).ToList();
            //    ViewBag.OperatorList = new SelectList(result, "operatorpk", "Text");

            return View();
        }
        public ActionResult GetPoin(ReportProperty rp)
        {
            List<PointoCountList> result = new List<PointoCountList>();
            using (OCBCTestEntities1 db = new OCBCTestEntities1())
            {
                result = (from td in db.tt_Transaction
                          join n in db.tm_Nasabah on td.AccountID equals n.AccountID
                          select new PointoCountList
                          {
                              AccountID = td.AccountID,
                              Nama = n.Nama,
                              Amount = td.Amount,
                              Description = td.Description
                          }).ToList();
                foreach (var item in result)
                {
                    if (item.Description == "Beli Pulsa")
                    {
                        if (item.Amount > 30000)
                        {
                            item.poin += Convert.ToInt32((item.Amount / 1000) * 2);
                        }
                        else if (item.Amount > 10000)
                        {
                            item.poin += Convert.ToInt32((item.Amount / 1000));
                        }
                    }
                    else if(item.Description == "Bayar Listrik")
                    {
                        if (item.Amount > 100000)
                        {
                            item.poin += Convert.ToInt32((item.Amount / 2000) * 2);
                        }
                        else if (item.Amount > 50000)
                        {
                            item.poin += Convert.ToInt32((item.Amount / 2000));
                        }
                    }
                    
                }
                result = result.GroupBy(l => l.AccountID)
                          .Select(cl => new PointoCountList
                          {
                              AccountID = cl.FirstOrDefault().AccountID,
                              Nama = cl.FirstOrDefault().Nama,
                              Description = "",
                              Amount = 0,
                              poin = cl.Sum(x => x.poin),
                          }).ToList(); ;
            }
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }
    }
}