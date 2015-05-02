using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EWC.Models;

namespace EWC.Controllers
{
    public class OIController : Controller
    {
        private DCDBset db = new DCDBset();

        //
        // GET: /OI/

        public ActionResult Index(int year = 0)
        {
            return View(db.oInvoices.Include(i => i.customer).Include(i => i.Items).Where(i => i.isActive == true).ToList());
        }

        //
        // GET: /Invoice/Details/5

        public ActionResult Details(int id = 0)
        {
            Oinvoice invoice = db.oInvoices.Include(i => i.Items).Include(i => i.customer).SingleOrDefault(i => i.DeliveryChalanID == id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }
        public ActionResult Print(int id = 0)
        {
            Oinvoice invoice = db.oInvoices.Include(i => i.Items).Include(i => i.customer).SingleOrDefault(i => (i.DeliveryChalanID == id && i.isActive));
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }
        //
        // GET: /Invoice/Create

        public ActionResult Create(int id = 0)
        {
            var dc = db.odeliveryChalans.Include("Items").Include("customer").FirstOrDefault(d => d.OdeliveryChalanID == id);
            var Invoicee = new Oinvoice(dc);

            if (id == 0 || dc == null)
            {
                return HttpNotFound();
            }
            var existinvoice = db.oInvoices.Include("Items").Include("customer").FirstOrDefault(d => d.DCNo == dc.DCNo && d.isActive);
            if (existinvoice != null)
                return RedirectToAction("Print", "Invoice", new { id = existinvoice.DeliveryChalanID });
            Customer c = dc.customer;
            c.CustomerID = 0;
            Invoicee.customer = c;
            foreach (DcItems itm in dc.Items)
            {
                Invoicee.Items.Add(new InvoiceItem(itm));
            }
            return View(Invoicee);
        }

        //
        // POST: /Invoice/Create

        [HttpPost]
        public ActionResult Create(Oinvoice invoice)
        {
            if (ModelState.IsValid)
            {
                invoice.addedby = User.Identity.Name;
                invoice.isActive = true;
                var a = db.odeliveryChalans.Where(i => i.DCNo == invoice.DCNo && i.IsActive).FirstOrDefault();
                a.invoiceCreated = true;
                db.Entry(a).State = EntityState.Modified;
                db.oInvoices.Add(invoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(invoice);
        }

        //
        // GET: /Invoice/Edit/5

        public ActionResult Edit(int id = 0)
        {
            var invoice = db.oInvoices.Include(i => i.Items).Include(i => i.customer).FirstOrDefault(i => i.DeliveryChalanID == id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        //
        // POST: /Invoice/Edit/5

        [HttpPost]
        public ActionResult Edit(Oinvoice invoice)
        {
            if (ModelState.IsValid)
            {
                var invindb = db.oInvoices.Include(dc => dc.customer).Include(dc => dc.Items).SingleOrDefault(dc => dc.DeliveryChalanID == invoice.DeliveryChalanID);
                db.Entry(invindb).CurrentValues.SetValues(invoice);
                var itemsindb = invindb.Items.ToList();
                invindb.isActive = true;
                foreach (var item in itemsindb)
                {
                    var itemi = invoice.Items.SingleOrDefault(i => i.InvoiceItemID == item.InvoiceItemID);
                    if (itemi != null)
                        db.Entry(item).CurrentValues.SetValues(itemi);
                }
                var cindb = db.customers.SingleOrDefault(i => i.CustomerID == invoice.customer.CustomerID);


                db.Entry(cindb).CurrentValues.SetValues(invoice.customer);
                invindb.isActive = true;
                db.SaveChanges();
                db.Dispose();
                return RedirectToAction("Index");
            }
            return View(invoice);
        }

        //
        // GET: /Invoice/Delete/5
        [Authorize(Users = "Admin")]
        public ActionResult Delete(int id = 0)
        {
            Oinvoice invoice = db.oInvoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        //
        // POST: /Invoice/Delete/5
        [Authorize(Users = "Admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var invoice = db.oInvoices.Include(dc => dc.Items).Include(dc => dc.customer).FirstOrDefault(dc => dc.DeliveryChalanID == id);
            if (invoice != null)
            {
                invoice.isActive = false;
                db.Entry(invoice).State = EntityState.Modified;
                var a = db.odeliveryChalans.Where(i => i.DCNo == invoice.DCNo && i.IsActive).FirstOrDefault();
                if (a != null)
                {
                    a.invoiceCreated = false;
                    db.Entry(a).State = EntityState.Modified;
                }
             db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}