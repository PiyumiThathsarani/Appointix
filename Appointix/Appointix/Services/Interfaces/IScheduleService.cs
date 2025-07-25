using Appointix.Models;

namespace Appointix.Services.Interfaces
{
    public interface IScheduleService
    {

        Task<Schedule> GetSchedule(int id);
        Task<List<Schedule>> GetSchedules();
        Task<bool> ValidateSchedule(Schedule schedule);
        Task CreateSchedule(Schedule schedule);
        Task UpdateSchedule(Schedule schedule);
        Task DeleteSchedule(int id);

    }
}
