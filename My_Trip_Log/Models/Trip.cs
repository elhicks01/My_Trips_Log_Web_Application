using System;
using System.ComponentModel.DataAnnotations;

namespace My_Trip_Log.Models
{
    public class Trip
    {
        public int TripID { get; set; }

        [Required(ErrorMessage = "Destination is required.")]
        public string? Destination { get; set; }

        [Required(ErrorMessage = "Start Date is required.")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required.")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public string? Accommodations { get; set; }

        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        public string? Phone { get; set; }

        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string? Email { get; set; }

        public string? Activities { get; set; }
    }
}
