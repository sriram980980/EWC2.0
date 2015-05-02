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
    public class DBCustomerController : Controller
    {
        private DCDBset db = new DCDBset();

        //
        // GET: /DBCustomer/

        public ActionResult Index()
        {
            return View(db.customersdb.ToList());
        }

        //
        // GET: /DBCustomer/Details/5

        public ActionResult Details(int id = 0)
        {
            CustomerDb customerdb = db.customersdb.Find(id);
            if (customerdb == null)
            {
                return HttpNotFound();
            }
            return View(customerdb);
        }

        //
        // GET: /DBCustomer/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /DBCustomer/Create

        [HttpPost]
        public ActionResult Create(CustomerDb customerdb)
        {
            if (ModelState.IsValid)
            {
                db.customersdb.Add(customerdb);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customerdb);
        }

        //
        // GET: /DBCustomer/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CustomerDb customerdb = db.customersdb.Find(id);
            if (customerdb == null)
            {
                return HttpNotFound();
            }
            return View(customerdb);
        }

        //
        // POST: /DBCustomer/Edit/5

        [HttpPost]
        public ActionResult Edit(CustomerDb customerdb)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerdb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customerdb);
        }

        //
        // GET: /DBCustomer/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CustomerDb customerdb = db.customersdb.Find(id);
            if (customerdb == null)
            {
                return HttpNotFound();
            }
            return View(customerdb);
        }

        //
        // POST: /DBCustomer/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomerDb customerdb = db.customersdb.Find(id);
            db.customersdb.Remove(customerdb);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}