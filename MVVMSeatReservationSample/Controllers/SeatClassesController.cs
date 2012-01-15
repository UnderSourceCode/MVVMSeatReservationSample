using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVVMSeatReservationSample.Models;

namespace MVVMSeatReservationSample.Controllers
{ 
    public class SeatClassesController : Controller
    {
        private StoreContext db = new StoreContext();

        public JsonResult IndexJson()
        {
            var list = from p in db.SeatClasses
                       orderby p.Price
                        select p;
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /SeatClasses/

        public ViewResult Index()
        {
            return View(db.SeatClasses.ToList());
        }

        //
        // GET: /SeatClasses/Details/5

        public ViewResult Details(string id)
        {
            SeatClass seatclass = db.SeatClasses.Find(id);
            return View(seatclass);
        }

        //
        // GET: /SeatClasses/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /SeatClasses/Create

        [HttpPost]
        public ActionResult Create(SeatClass seatclass)
        {
            if (ModelState.IsValid)
            {
                db.SeatClasses.Add(seatclass);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(seatclass);
        }
        
        //
        // GET: /SeatClasses/Edit/5
 
        public ActionResult Edit(string id)
        {
            SeatClass seatclass = db.SeatClasses.Find(id);
            return View(seatclass);
        }

        //
        // POST: /SeatClasses/Edit/5

        [HttpPost]
        public ActionResult Edit(SeatClass seatclass)
        {
            if (ModelState.IsValid)
            {
                db.Entry(seatclass).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(seatclass);
        }

        //
        // GET: /SeatClasses/Delete/5
 
        public ActionResult Delete(string id)
        {
            SeatClass seatclass = db.SeatClasses.Find(id);
            return View(seatclass);
        }

        //
        // POST: /SeatClasses/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {            
            SeatClass seatclass = db.SeatClasses.Find(id);
            db.SeatClasses.Remove(seatclass);
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