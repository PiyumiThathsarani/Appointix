using System.ComponentModel.DataAnnotations;

namespace Appointix.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }

        public int? AppointeeUserId { get; set; }

        public int ScheduleId { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Client Name")]
        public string AppointeeName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Client Email")]
        public string AppointeeEmail { get; set; }

        [StringLength(300)]
        public string? Notes { get; set; }

        [Display(Name = "Booked At")]
        public DateTime BookedDateTime { get; set; }

        public bool IsDeleted { get; set; }


        #region Navigation 

        public User Appointee { get; set; }
        public Schedule Schedule { get; set; }

        #endregion
    }
}
