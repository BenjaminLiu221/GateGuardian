using System.ComponentModel.DataAnnotations;

namespace GateGuardianWeb.Models
{
    public class Password
    {
        [Key]
        public int Id { get; set; }
        public string? Characters { get; set; }

    }
}
