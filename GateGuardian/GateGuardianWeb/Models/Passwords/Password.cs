using System.ComponentModel.DataAnnotations;

namespace GateGuardianWeb.Models.Passwords
{
    public class Password
    {
        [Key]
        public int Id { get; set; }
        public string? Characters { get; set; }
    }
}
