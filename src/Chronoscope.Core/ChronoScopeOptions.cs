using System.ComponentModel.DataAnnotations;

namespace Chronoscope
{
    public class ChronoScopeOptions
    {
        [Required]
        public string DefaultTaskScopeNameFormat { get; } = "Scope {0}";
    }
}