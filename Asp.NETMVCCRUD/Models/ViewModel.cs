using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asp.NETMVCCRUD.Models
{
    // REPORT
    public class PointoCountList
    {
        public int AccountID { get; set; }
        public string Description { get; set; }
        public string Nama { get; set; }
        public decimal Amount { get; set; }
        public int poin { get; set; }

    }
    public class Transaksi
    {
        public int TransactionID { get; set; }
        public int AccountID { get; set; }
        public string TransDate { get; set; }
        public string Description { get; set; }
        public string DebitCreditStatus { get; set; }
        public decimal Amount { get; set; }
    }
   
    public class ReportProperty
    {
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public int Operator { get; set; }
    }


}