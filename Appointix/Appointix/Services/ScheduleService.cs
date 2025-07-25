using Appointix.Models;
using Appointix.Services.Interfaces;

namespace Appointix.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IGenericRepository<Schedule> _scheduleRepo;

        public ScheduleService(IGenericRepository<Schedule> scheduleRepo)
        {
            _scheduleRepo = scheduleRepo;
        }

        public async Task<Schedule?> GetSchedule(int scheduleId)
        {
            return await _scheduleRepo.GetByIdAsync(scheduleId);
        }

        public async Task<List<Schedule>> GetSchedules()
        {
            var schedules = await _scheduleRepo.FindAsync(s => !s.IsDeleted);
            return schedules.OrderBy(s => s.ScheduleStartTime).ToList();
        }

        public async Task CreateSchedule(Schedule schedule)
        {
            await _scheduleRepo.AddAsync(schedule);
            await _scheduleRepo.SaveChangesAsync();
        }

        public async Task UpdateSchedule(Schedule schedule)
        {
            _scheduleRepo.Update(schedule);
            await _scheduleRepo.SaveChangesAsync();
        }

        public async Task DeleteSchedule(int scheduleId)
        {
            var schedule = await _scheduleRepo.GetByIdAsync(scheduleId);
            if (schedule != null)
            {
                schedule.IsDeleted = true;
                _scheduleRepo.Update(schedule);
                await _scheduleRepo.SaveChangesAsync();
            }
        }

        public async Task<bool> ValidateSchedule(Schedule schedule)
        {
            var overlapping = await _scheduleRepo.FindAsync(s =>
                !s.IsDeleted &&
                schedule.ScheduleStartTime < s.ScheduleEndTime &&
                schedule.ScheduleEndTime > s.ScheduleStartTime);

            return overlapping.Any();
        }
    }
}
