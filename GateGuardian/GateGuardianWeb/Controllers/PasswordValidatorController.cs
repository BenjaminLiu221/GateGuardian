using GateGuardianWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace GateGuardianWeb.Controllers
{
    public class PasswordValidatorController : Controller
    {
        // LengthValidation
        public bool LengthValidation(string input)
        {
            if (input == null || input.Length < 10)
            {
                return false;
            }
            return true;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Validate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Validate(Password password)
        {
            // Build PasswordValidationResult Object

            PasswordValidationResults passwordValidationResults = new PasswordValidationResults()
            {
                Password = password,
                LengthValidation = "Failed."
            };

            if (LengthValidation(password.Characters).Equals(true))
            {
                passwordValidationResults.LengthValidation = "Passed.";
            }
            return View(passwordValidationResults);
        }
    }
}
