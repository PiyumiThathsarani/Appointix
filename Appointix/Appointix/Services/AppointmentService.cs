using Appointix.Models;
using Appointix.Services.Interfaces;

namespace Appointix.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IGenericRepository<Appointment> _appointmentRepo;
        private readonly IGenericRepository<Schedule> _scheduleRepo;

        public AppointmentService(
            IGenericRepository<Appointment> appointmentRepo,
            IGenericRepository<Schedule> scheduleRepo)
        {
            _appointmentRepo = appointmentRepo;
            _scheduleRepo = scheduleRepo;
        }

        public async Task<List<Appointment>> GetAppointments()
        {
            var list = await _appointmentRepo.FindAsync(a => !a.IsDeleted);
            return list.OrderByDescending(a => a.BookedDateTime).ToList();
        }

        public async Task<List<Appointment>> GetUserAppointments(string appointeeEmail)
        {
            var list = await _appointmentRepo.FindAsync(a => a.AppointeeEmail == appointeeEmail && !a.IsDeleted);
            return list.OrderByDescending(a => a.BookedDateTime).ToList();
        }

        public async Task<bool> CreateAppointment(int scheduleId, string name, string email, string? notes)
        {
            var schedule = await _scheduleRepo.GetByIdAsync(scheduleId);
            if (schedule == null || schedule.IsBooked) return false;

            var appointment = new Appointment
            {
                AppointeeName = name,
                AppointeeEmail = email,
                ScheduleId = scheduleId,
                Notes = notes,
                BookedDateTime = DateTime.Now
            };

            schedule.IsBooked = true;

            await _appointmentRepo.AddAsync(appointment);
            _scheduleRepo.Update(schedule);
            await _appointmentRepo.SaveChangesAsync();
            await _scheduleRepo.SaveChangesAsync();

            return true;
        }

        public async Task CancelAppointment(int appointmentId)
        {
            var appointment = await _appointmentRepo.GetByIdAsync(appointmentId);
            if (appointment == null) return;

            appointment.IsDeleted = true;
            _appointmentRepo.Update(appointment);
            await _appointmentRepo.SaveChangesAsync();
        }
    }
}
