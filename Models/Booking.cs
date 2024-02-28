using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GBC_Travel_Group83.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        [Required]
        [DisplayName("Your Name:")]
        //Customer's name
        public string Name { get; set; }

        // nullable ?
        public string? Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        // nullable ?
        public string? Status { get; set; }

        public List<Flight>? Flights { get; set; }
        public List<RentalCar>? RentaiCars { get; set; }
        public List<Hotel>? Hotels { get; set; }
        public decimal? TotalPrice { get; set; }
    }
}
