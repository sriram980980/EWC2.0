using EWC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EWC.Controllers
{
    public class ReportsController : Controller
    {
        private DCDBset db = new DCDBset();
        //
        // GET: /Reports/

        public ActionResult Index(DateTime ? start , DateTime? enddate )
        {
            if (start == null) {
                start = DateTime.Now.Date;
            }
            if (enddate == null)
            {
                enddate = start.Value.AddDays(1);
            }
            var report = new Report();
            report.start = start.Value;
            report.end = enddate.Value;
            report.invoices = db.Invoices.Include("customer").Include("Items").Where(i => i.isActive == true && i.Date >= start && i.Date <= enddate).ToList();
            report.estimates = db.estimates.Include("customer").Include("Items").Where(i => i.isActive == true && i.Date >= start && i.Date <= enddate).ToList();
            report.oldInvoice = db.oInvoices.Include("customer").Include("Items").Where(i => i.isActive == true && i.Date >= start && i.Date <= enddate).ToList();
            return View(report);
        }
    }
}
