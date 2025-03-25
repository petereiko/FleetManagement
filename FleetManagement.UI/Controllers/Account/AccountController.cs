using FleetManagement.UI.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FleetManagement.UI.Controllers.Account
{
    public class AccountController : Controller
    {
        private const string UsersSessionKey = "RegisteredUsers";

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Get the current list of users from session (or create a new list)
            List<RegisterDto> users = new List<RegisterDto>();
            var sessionData = HttpContext.Session.GetString(UsersSessionKey);
            if (!string.IsNullOrEmpty(sessionData))
            {
                users = JsonSerializer.Deserialize<List<RegisterDto>>(sessionData);
            }

            // Check if the username or email already exists (for simplicity, case-insensitive)
            if (users.Any(u => string.Equals(u.UserName, model.UserName, StringComparison.OrdinalIgnoreCase)
                            || string.Equals(u.Email, model.Email, StringComparison.OrdinalIgnoreCase)))
            {
                ModelState.AddModelError("", "User with the same username or email already exists.");
                return View(model);
            }

            // Add new user and save back to session
            users.Add(model);
            HttpContext.Session.SetString(UsersSessionKey, JsonSerializer.Serialize(users));

            // Optionally, you can log the user in after registration
            TempData["SuccessMessage"] = "Registration successful! Please log in.";
            return RedirectToAction("Login");
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Retrieve registered users from session
            List<RegisterDto> users = new List<RegisterDto>();
            var sessionData = HttpContext.Session.GetString(UsersSessionKey);
            if (!string.IsNullOrEmpty(sessionData))
            {
                users = JsonSerializer.Deserialize<List<RegisterDto>>(sessionData);
            }

            // Check credentials (case-insensitive check for username)
            var user = users.FirstOrDefault(u =>
                string.Equals(u.UserName, model.UserName, StringComparison.OrdinalIgnoreCase)
                && u.Password == model.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View(model);
            }

            // On successful login, set session value for the logged-in user (for example, username)
            HttpContext.Session.SetString("LoggedInUser", user.UserName);
            return RedirectToAction("Index", "Driver"); // Redirect to driver's dashboard (update as needed)
        }

        // GET: /Account/ForgotPassword
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        // (Optional) POST: /Account/ForgotPassword
        // Implement your forgot password logic here if needed.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ForgotPassword(string email)
        {
            // For session-based simulation, simply return a message.
            TempData["InfoMessage"] = "If an account with that email exists, a password reset link has been sent.";
            return RedirectToAction("Login");
        }
    }
}
