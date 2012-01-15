using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MVVMSeatReservationSample.Models
{
    public class StoreContext : DbContext
    {
        public DbSet<Reservation> Reservations {get;set;}
        public DbSet<ReservationSeat> ReservationSeats { get; set; }
        public DbSet<SeatClass> SeatClasses {get;set;}
    }

    public class StoreInitializer : DropCreateDatabaseIfModelChanges<StoreContext>
    {
        protected override void Seed(StoreContext context)
        {
            var classes = new List<SeatClass>
            {
                new SeatClass() { SeatLevel = "E", Name = "エコノミー", Price = 50000 },
                new SeatClass() { SeatLevel = "B", Name = "ビジネス", Price = 200000 },
                new SeatClass() { SeatLevel = "F", Name = "ファースト", Price = 1000000 }
            };
            classes.ForEach(s => context.SeatClasses.Add(s));
            context.SaveChanges();
        }
    }
}