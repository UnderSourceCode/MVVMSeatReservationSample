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
    public class ReservationsController : Controller
    {
        private StoreContext db = new StoreContext();

        [HttpPost]
        public JsonResult Save(ICollection<ReservationSeat> seats)
        {
            var errorMessage = validateReservation(seats);

            if (errorMessage == null)
            {
                var rsrv = new Reservation();
                rsrv.Name = User.Identity.Name;
                rsrv.ReserveDateTime = System.DateTime.Now;
                db.Reservations.Add(rsrv);

                foreach(var seat in seats)
                {
                    var rsrvSt = new ReservationSeat();
                    rsrvSt.Name = seat.Name;
                    rsrvSt.SeatLevel = seat.SeatLevel;
                    db.ReservationSeats.Add(rsrvSt);
                }
            
                db.SaveChanges();

                return Json("登録完了しました。", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(errorMessage, JsonRequestBehavior.AllowGet);
            }
            
        }

        private string validateReservation(ICollection<ReservationSeat> seats)
        {
            if (seats == null) return "最低一つの予約をして下さい。";

            foreach(var seat in seats)
            {
                if(seat.Name == null) return "名前を入力して下さい。";
            }
            return null;
        }

        //
        // GET: /Reservations/

        public ViewResult Index()
        {
            return View(db.Reservations.ToList());
        }

        //
        // GET: /Reservations/Details/5

        public ViewResult Details(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            return View(reservation);
        }

        //
        // GET: /Reservations/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Reservations/Create

        [HttpPost]
        public ActionResult Create(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Reservations.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(reservation);
        }
        
        //
        // GET: /Reservations/Edit/5
 
        public ActionResult Edit(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            return View(reservation);
        }

        //
        // POST: /Reservations/Edit/5

        [HttpPost]
        public ActionResult Edit(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reservation);
        }

        //
        // GET: /Reservations/Delete/5
 
        public ActionResult Delete(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            return View(reservation);
        }

        //
        // POST: /Reservations/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
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