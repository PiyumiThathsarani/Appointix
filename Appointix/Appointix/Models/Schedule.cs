using System.ComponentModel.DataAnnotations;

namespace Appointix.Models
{
    public class Schedule
    {
        public int ScheduleId { get; set; }

        [Required]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Start Time")]
        public DateTime ScheduleStartTime { get; set; }

        [Required]
        [Display(Name = "End Time")]
        public DateTime ScheduleEndTime { get; set; }

        public bool IsBooked { get; set; }

        public string? Note { get; set; }

        public bool IsDeleted { get; set; }


        #region Navigation 

        public ICollection<Appointment> Appointments { get; set; }

        #endregion
    }
}
