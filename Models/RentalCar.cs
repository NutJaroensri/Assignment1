using System.ComponentModel.DataAnnotations;

namespace GBC_Travel_Group83.Models
{
    public class RentalCar
    {
        
        public int RentalCarID { get; set; }

        [Required]
        public string PlateNumber { get; set; }
        [Required]
        public string RentalCompany { get; set; }
        [Required]
        public string ModelName { get; set; }

        public string? ModelDescription { get; set; }

        public string? ModelType { get; set; }

        public decimal Price { get; set; }

        public bool IsAvaiable { get; set; }

        public int BookingId { get; set; }
        public Booking? Booking { get; set; }

    }
}
