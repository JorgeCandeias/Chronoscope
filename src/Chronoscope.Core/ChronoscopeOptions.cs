using System.ComponentModel.DataAnnotations;

namespace Chronoscope
{
    public class ChronoscopeOptions
    {
        [Required]
        public string DefaultTaskScopeNameFormat { get; set; } = "Scope {0}";
    }
}