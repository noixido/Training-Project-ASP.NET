using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Latihan.Models
{
    public class Account
    {
        [Key]
        [ForeignKey("Employee")]
        public string? NIK { get; set; }
        public string? Password { get; set; }

        public virtual Employee? Employee { get; set; }
        public virtual Profiling? Profiling { get; set; }
    }
}
