using System.ComponentModel.DataAnnotations;

namespace GBC_Travel_Group83.Models
{
    public class Hotel
    {
        
        public int HotelId { get; set; }

        [Required]
        public string HotelName { get; set; }

        public string? Location { get; set; }

        public decimal Price { get; set; }

        public int Capacity { get; set; }
        public string? Amenities { get; set; }

        public TimeOnly CheckInTime { get; set; }

        public TimeOnly CheckOutTime { get; set; }

        public string? Description { get; set; }

        public int BookingId { get; set; }
        public Booking? Booking { get; set; }
    }
}
