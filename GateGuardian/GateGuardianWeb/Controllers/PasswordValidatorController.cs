using GateGuardianWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace GateGuardianWeb.Controllers
{
    public class PasswordValidatorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Validate()
        {
            return View();
        }

        // LengthValidation
        public bool LengthValidation(string input)
        {
            if (input == null || input.Length < 10)
            {
                return false;
            }
            return true;
        }

        // NumberValidation
        public bool NumberValidation(string input)
        {
            if (input == null || input.Any(char.IsDigit).Equals(false))
            {
                return false;
            }
            return true;
        }

        // Build PasswordValidationResult Object Here

        public PasswordValidationResults BuildPasswordValidationResult(Password _password)
        {
            PasswordValidationResults passwordValidationResults = new PasswordValidationResults()
            {
                Password = _password,
                LengthValidation = "Failed.",
                NumberValidation = "Failed."
            };

            return passwordValidationResults;
        }


        [HttpPost]
        public IActionResult Validate(Password password)
        {
            // Build PasswordValidationResult Object

            var passwordValidationResults = BuildPasswordValidationResult(password);

            if (LengthValidation(password.Characters).Equals(true))
            {
                passwordValidationResults.LengthValidation = "Passed.";
            }
            if (NumberValidation(password.Characters).Equals(true))
            {
                passwordValidationResults.NumberValidation = "Passed.";
            }
            return View(passwordValidationResults);
        }
    }
}
