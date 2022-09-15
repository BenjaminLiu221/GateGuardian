namespace GateGuardianWeb.Models
{
    public class PasswordValidationResults
    {
        public Password Password { get; set; }
        public string LengthValidation { get; set; }
        public string NumberValidation { get; set; }
    }
}
