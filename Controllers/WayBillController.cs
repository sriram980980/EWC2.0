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
    public class WayBillController : Controller
    {
        private DCDBset db = new DCDBset();

        [Authorize]
        public ActionResult Index(int year = 0)
        {
           
            return View(db.waybills.Include(i => i.customer).Where(dn => dn.IsActive != false));
        }

        //
        // GET: /DC/Details/5

        public ActionResult Details(int id = 0)
        {
            WayBill waybill = db.waybills.Where(x => x.DeliveryChalanID == id).Include("customer").Include("Items").FirstOrDefault();
            if (waybill == null)
            {

                return HttpNotFound();
            }
            return View(waybill);
        }

        //
        // GET: /DC/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /DC/Create

        [HttpPost]
        public ActionResult Create(WayBill waybill)
        {
            if (ModelState.IsValid)
            {
                waybill.IsActive = true;
                db.waybills.Add(waybill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(waybill);
        }

        //
        // GET: /DC/Edit/5

        public ActionResult Edit(int id = 0)
        {
            WayBill waybill = db.waybills.Where(x => x.DeliveryChalanID == id).Include("customer").Include("Items").FirstOrDefault();
            if (waybill == null)
            {
                return HttpNotFound();
            }
            return View(waybill);
        }

        //
        // POST: /DC/Edit/5
        [Authorize(Users = "Admin")]
        [HttpPost]
        public ActionResult Edit(WayBill waybill)
        {
            if (ModelState.IsValid)
            {
                WayBill dcindb = db.waybills.Include(dc => dc.customer).Include(dc => dc.Items).SingleOrDefault(dc => dc.DeliveryChalanID == waybill.DeliveryChalanID);
                db.Entry(dcindb).CurrentValues.SetValues(waybill);
                var itemsindb = dcindb.Items.ToList();
                foreach (var item in itemsindb)
                {
                    var itemi = waybill.Items.SingleOrDefault(i => i.DcItemsID == item.DcItemsID);
                    if (itemi != null)
                        db.Entry(item).CurrentValues.SetValues(itemi);
                }
                var cindb = db.customers.SingleOrDefault(i => i.CustomerID == waybill.customer.CustomerID);
                dcindb.IsActive = true;
                db.Entry(cindb).CurrentValues.SetValues(waybill.customer);
                db.SaveChanges();
                db.Dispose();
                return RedirectToAction("Index");
            }
            return View(waybill);
        }

        public ActionResult Print(int id = 0)
        {
            WayBill waybill = db.waybills.Where(x => x.DeliveryChalanID == id).Include("customer").Include("Items").FirstOrDefault();
            if (waybill == null)
            {
                return HttpNotFound();
            }
            return View(waybill);
        }

        //
        // GET: /DC/Delete/5
        [Authorize(Users = "Admin")]
        public ActionResult Delete(int id = 0)
        {
            WayBill waybill = db.waybills.Find(id);
            if (waybill == null)
            {
                return HttpNotFound();
            }
            return View(waybill);
        }

        //
        // POST: /DC/Delete/5
        [Authorize(Users = "Admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            WayBill waybill = db.waybills.Include(dc => dc.Items).Include(dc => dc.Items).FirstOrDefault(dc => dc.DeliveryChalanID== id);
            if (waybill != null)
            {
                waybill.IsActive = false;
                db.Entry(waybill).State = EntityState.Modified;
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