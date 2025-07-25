using Appointix.Models;
using Appointix.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Appointix.Controllers
{
    //[Authorize(Roles = "Client")]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly UserManager<User> _userManager;

        public AppointmentController(IAppointmentService appointmentService, UserManager<User> userManager)
        {
            _appointmentService = appointmentService;
            _userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> Index(DateTime? date, string? name, string? email, int pageNumber = 1)
        {
            var allAppointments = await _appointmentService.GetAppointments();

            var filtered = allAppointments
                .Where(a =>
                    (date == null || a.Schedule.ScheduleStartTime.Date == date.Value.Date) &&
                    (string.IsNullOrEmpty(name) || a.AppointeeName.Contains(name, StringComparison.OrdinalIgnoreCase)) &&
                    (string.IsNullOrEmpty(email) || a.AppointeeEmail.Contains(email, StringComparison.OrdinalIgnoreCase))
                ).AsQueryable();

            int pageSize = 10;
            var paginatedList = await PaginatedList<Appointment>.CreateAsync(filtered, pageNumber, pageSize);

            ViewBag.FilterDate = date?.ToString("yyyy-MM-dd") ?? "";
            ViewBag.FilterName = name;
            ViewBag.FilterEmail = email;

            return View(paginatedList);
        }

        [HttpGet]
        public async Task<IActionResult> Book()
        {
            // Optionally inject IScheduleService to get available slots
            return View(); // You will bind available schedules in the View
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Book(int scheduleId, string? notes)
        {
            var user = await _userManager.GetUserAsync(User);
            var success = await _appointmentService.CreateAppointment(scheduleId, user.FullName, user.Email, notes);

            if (!success)
            {
                TempData["Error"] = "Failed to book appointment. The time slot may already be taken.";
                return RedirectToAction(nameof(Book));
            }

            TempData["Success"] = "Appointment booked successfully!";
            return RedirectToAction(nameof(GetAppointmentsByUser));
        }

        [HttpGet]
        public async Task<IActionResult> GetAppointmentsByUser()
        {
            var user = await _userManager.GetUserAsync(User);
            var appointments = await _appointmentService.GetUserAppointments(user.Email);
            return View(appointments);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelAppointment(int appointmentId)
        {
            await _appointmentService.CancelAppointment(appointmentId);
            TempData["Success"] = "Appointment canceled.";
            return RedirectToAction(nameof(Index));
        }

    }
}
