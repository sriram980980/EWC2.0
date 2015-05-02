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
    public class DCController : Controller
    {
        private DCDBset db = new DCDBset();

        //
        // GET: /DC/

        [Authorize]
        public ActionResult Index()
        {
            return View(db.deliveryChalans.Include(i => i.customer).Where(dn => dn.IsActive != false ));
        }

        //
        // GET: /DC/Details/5

        public ActionResult Details(int id = 0)
        {
            DeliveryChalan deliverychalan = db.deliveryChalans.Where(x=>x.DeliveryChalanID==id).Include("customer").Include("Items").FirstOrDefault();
            if (deliverychalan == null)
            {
                
                return HttpNotFound();
            }
            return View(deliverychalan);
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
        public ActionResult Create(DeliveryChalan deliverychalan)
        {
            if (ModelState.IsValid)
            {
                deliverychalan.IsActive = true;
                db.deliveryChalans.Add(deliverychalan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(deliverychalan);
        }

        //
        // GET: /DC/Edit/5

        public ActionResult Edit(int id = 0)
        {
            DeliveryChalan deliverychalan = db.deliveryChalans.Where(x => x.DeliveryChalanID == id).Include("customer").Include("Items").FirstOrDefault();
            if (deliverychalan == null)
            {
                return HttpNotFound();
            }
            return View(deliverychalan);
        }

        //
        // POST: /DC/Edit/5
        [Authorize(Users="Admin")]
        [HttpPost]
        public ActionResult Edit(DeliveryChalan deliverychalan)
        {
            if (ModelState.IsValid)
            {
                DeliveryChalan dcindb = db.deliveryChalans.Include(dc => dc.customer).Include(dc => dc.Items).SingleOrDefault(dc => dc.DeliveryChalanID == deliverychalan.DeliveryChalanID);
                db.Entry(dcindb).CurrentValues.SetValues(deliverychalan);
                var itemsindb = dcindb.Items.ToList();
                foreach (var item in itemsindb) {
                    var itemi = deliverychalan.Items.SingleOrDefault(i => i.DcItemsID == item.DcItemsID);
                    if (itemi != null)
                        db.Entry(item).CurrentValues.SetValues(itemi);
                }
                var cindb = db.customers.SingleOrDefault(i => i.CustomerID == deliverychalan.customer.CustomerID);
                dcindb.IsActive = true;
                db.Entry(cindb).CurrentValues.SetValues(deliverychalan.customer);
                db.SaveChanges();
                db.Dispose();
                return RedirectToAction("Index");
            }
            return View(deliverychalan);
        }

        public ActionResult Print(int id=0){
            DeliveryChalan deliverychalan = db.deliveryChalans.Where(x => x.DeliveryChalanID == id).Include("customer").Include("Items").FirstOrDefault();
            if (deliverychalan == null)
            {
                return HttpNotFound();
            }
            return View(deliverychalan);
        }

        //
        // GET: /DC/Delete/5

        public ActionResult Delete(int id = 0)
        {
            DeliveryChalan deliverychalan = db.deliveryChalans.Find(id);
            if (deliverychalan == null)
            {
                return HttpNotFound();
            }
            return View(deliverychalan);
        }

        //
        // POST: /DC/Delete/5
        [Authorize(Users = "Admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            DeliveryChalan deliverychalan = db.deliveryChalans.Include(dc => dc.Items).Include(dc => dc.Items).FirstOrDefault(dc=>dc.DeliveryChalanID==id);
            if (deliverychalan != null)
            {
                deliverychalan.IsActive = false;
                db.Entry(deliverychalan).State = EntityState.Modified;
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