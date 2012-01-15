using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVVMSeatReservationSample.Models
{
    public class Reservation
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.Identity)]
        public int ReservationID {get;set;}
        [Required]
        public string Name {get;set;}
        [Required]
        public DateTime ReserveDateTime {get;set;}

        public virtual ICollection<ReservationSeat> ReservationSeats {get;set;}
    }

    public class ReservationSeat
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.Identity)]
        public int SeatID { get; set; }
        [Required]
        public string Name {get;set;}
        [Required]
        [ForeignKey("Reservation")]
        public int ReservationID { get; set; }
        [Required]
        [ForeignKey("SeatClass")]
        public string SeatLevel { get; set; }
        
        public virtual Reservation Reservation {get;set;}
        public virtual SeatClass SeatClass {get;set;}    
    }

    public class SeatClass
    {
        [Key]
        public string SeatLevel {get;set;}
        [Required]
        public string Name {get;set;}
        [Required]
        public int Price {get;set;}
    }
}