using System.ComponentModel.DataAnnotations;

namespace Chronoscope
{
    public class ChronoscopeOptionsX
    {
        [Required]
        public string DefaultTaskScopeNameFormat { get; } = "Scope {0}";
    }
}