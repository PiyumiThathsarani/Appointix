using Appointix.Models;

namespace Appointix.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<List<Appointment>> GetAppointments();
        Task<List<Appointment>> GetUserAppointments(string email);
        Task<bool> CreateAppointment(int scheduleId, string appointeeName, string appointeeEmail, string? notes);
        Task CancelAppointment(int appointmentId);
    }
}
