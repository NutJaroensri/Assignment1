using System.ComponentModel.DataAnnotations;

namespace GBC_Travel_Group83.Models
{
    public class Flight
    {
        
        public int FlightId { get; set; }

        [Required]
        public string FlightNumber { get; set; }
        [Required]
        public string Airline { get; set; }
        [Required]
        public string Origin { get; set; }
        [Required]
        public string Destination { get; set; }

        public TimeOnly DepartureTime { get; set; }

        public TimeOnly ArrivalTime { get; set; }

        public int FlightDurationMin { get; set; }

        public decimal Price { get; set; }

        public int BookingId { get; set; }
        public Booking? Booking { get; set; }
    }
}
