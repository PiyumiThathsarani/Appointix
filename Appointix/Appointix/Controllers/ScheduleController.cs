using Appointix.Models;
using Appointix.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Appointix.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var schedules = await _scheduleService.GetSchedules();
            return View(schedules);
        }

        [HttpGet]
        public async Task<IActionResult> GetSchedulesJson()
        {
            var schedules = await _scheduleService.GetSchedules();

            var calendarEvents = schedules.Select(s => new
            {
                id = s.ScheduleId,
                title = s.IsBooked ? "Booked" : "Available",
                start = s.ScheduleStartTime.ToString("s"), 
                end = s.ScheduleEndTime.ToString("s"),
                color = s.IsBooked ? "#ff6b6b" : "#51d88a"
            });

            return Json(calendarEvents);
        }

        [HttpGet]
        public async Task<IActionResult> GetSchedulesByDate(DateTime date)
        {
            var schedules = await _scheduleService.GetSchedules();
            var filtered = schedules.Where(s => s.Date.Date == date.Date).ToList();
            return PartialView("_ViewScheduleModal", filtered);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Schedule schedule)
        //{
        //    if (await _scheduleService.ValidateSchedule(schedule))
        //    {
        //        ModelState.AddModelError("Sorry!", "This time slot overlaps with an existing one.");
        //        return View(schedule);
        //    }

        //    await _scheduleService.CreateSchedule(schedule);
        //    return RedirectToAction(nameof(Index));
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Schedule schedule)
        {
            if (await _scheduleService.ValidateSchedule(schedule))
            {
                ModelState.AddModelError(string.Empty, "This time slot overlaps with an existing one.");

                // If AJAX request, return validation errors as JSON
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
                }

                // Otherwise return the view for fallback
                return View(schedule);
            }

            await _scheduleService.CreateSchedule(schedule);

            // If AJAX, return success json
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(new { success = true });
            }

            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int scheduleId)
        {
            var schedule = await _scheduleService.GetSchedule(scheduleId);
            if (schedule == null)
            {
                return NotFound();
            }
            return PartialView("_EditScheduleModal", schedule); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int scheduleId, Schedule schedule)
        {
            if (scheduleId != schedule.ScheduleId)
            {
                return NotFound();
            }
            await _scheduleService.UpdateSchedule(schedule);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int scheduleId)
        {
            var schedule = await _scheduleService.GetSchedule(scheduleId);
            if (schedule == null)
            {
                return NotFound();
            }
            return View(schedule);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSchedule(int scheduleId)
        {
            await _scheduleService.DeleteSchedule(scheduleId);
            return RedirectToAction(nameof(Index));
        }
    }

}
