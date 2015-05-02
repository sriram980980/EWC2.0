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
    [Authorize]
    public class ODCController : Controller
    {
        private DCDBset db = new DCDBset();

        //
        // GET: /ODC/

        public ActionResult Index()
        {
           return View(db.odeliveryChalans.Include(i => i.customer).Where(dn => dn.IsActive != false ));
        }

        //
        // GET: /ODC/Details/5

        public ActionResult Details(int id = 0)
        {
            OdeliveryChalan deliverychalan = db.odeliveryChalans.Where(x => x.OdeliveryChalanID == id).Include("customer").Include("Items").FirstOrDefault();
            if (deliverychalan == null)
            {

                return HttpNotFound();
            }
            return View(deliverychalan);
        }

        //
        // GET: /ODC/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ODC/Create

        [HttpPost]
        public ActionResult Create(OdeliveryChalan odeliverychalan)
        {
            if (ModelState.IsValid)
            {
                db.odeliveryChalans.Add(odeliverychalan);
                odeliverychalan.IsActive = true;
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(odeliverychalan);
        }

        //
        // GET: /ODC/Edit/5

        public ActionResult Edit(int id = 0)
        {
            OdeliveryChalan odeliverychalan = db.odeliveryChalans.Where(x => x.OdeliveryChalanID == id).Include("customer").Include("Items").FirstOrDefault(); 
            if (odeliverychalan == null)
            {
                return HttpNotFound();
            }
            return View(odeliverychalan);
        }

        //
        // POST: /ODC/Edit/5

        [HttpPost]
        public ActionResult Edit(OdeliveryChalan odeliverychalan)
        {

            if (ModelState.IsValid)
            {
                OdeliveryChalan dcindb = db.odeliveryChalans.Include(dc => dc.customer).Include(dc => dc.Items).SingleOrDefault(dc => dc.OdeliveryChalanID == odeliverychalan.OdeliveryChalanID);
                db.Entry(dcindb).CurrentValues.SetValues(odeliverychalan);
                var itemsindb = dcindb.Items.ToList();
                foreach (var item in itemsindb)
                {
                    var itemi = odeliverychalan.Items.SingleOrDefault(i => i.DcItemsID == item.DcItemsID);
                    if (itemi != null)
                        db.Entry(item).CurrentValues.SetValues(itemi);
                }
                var cindb = db.customers.SingleOrDefault(i => i.CustomerID == odeliverychalan.customer.CustomerID);
                dcindb.IsActive = true;
                db.Entry(cindb).CurrentValues.SetValues(odeliverychalan.customer);
                db.SaveChanges();
                db.Dispose();
                return RedirectToAction("Index");
            }
            return View(odeliverychalan);

        }

        //
        // GET: /ODC/Delete/5

        public ActionResult Delete(int id = 0)
        {
            OdeliveryChalan odeliverychalan = db.odeliveryChalans.Find(id);
            if (odeliverychalan == null)
            {
                return HttpNotFound();
            }
            return View(odeliverychalan);
        }

        //
        // POST: /ODC/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            OdeliveryChalan odeliverychalan = db.odeliveryChalans.Find(id);

            odeliverychalan.IsActive = false;
            db.Entry(odeliverychalan).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Print(int id = 0)
        {
            OdeliveryChalan deliverychalan = db.odeliveryChalans.Where(x => x.OdeliveryChalanID == id).Include("customer").Include("Items").FirstOrDefault();
            if (deliverychalan == null)
            {
                return HttpNotFound();
            }
            return View(deliverychalan);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}