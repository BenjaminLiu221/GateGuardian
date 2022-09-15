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

        // Build PasswordValidationResult Object Here

        public PasswordValidationResults BuildPasswordValidationResult(Password _password)
        {
            PasswordValidationResults passwordValidationResults = new PasswordValidationResults()
            {
                Password = _password,
                LengthValidation = "Failed."
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
            return View(passwordValidationResults);
        }
    }
}
