﻿using GateGuardianWeb.Models;
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

        // CapitalizationValidation

        public bool CapitalizationValidation(string input)
        {
            if (input == null || input.ToLower().Equals(input))
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
                NumberValidation = "Failed.",
                CapitalizationValidation = "Failed.",
            };

            if (LengthValidation(_password.Characters).Equals(true))
            {
                passwordValidationResults.LengthValidation = "Passed.";
            }
            if (NumberValidation(_password.Characters).Equals(true))
            {
                passwordValidationResults.NumberValidation = "Passed.";
            }
            if (CapitalizationValidation(_password.Characters).Equals(true))
            {
                passwordValidationResults.CapitalizationValidation = "Passed.";
            }

            return passwordValidationResults;
        }

        [HttpPost]
        public IActionResult Validate(Password password)
        {
            var passwordValidationResults = BuildPasswordValidationResult(password);
            return View(passwordValidationResults);
        }
    }
}
