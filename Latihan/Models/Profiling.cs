using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Latihan.Models
{
    public class Profiling
    {
        [Key]
        [ForeignKey("Account")]
        public string? NIK { get; set; }

        public virtual Education? Education { get; set; }
        [ForeignKey(nameof(Education))]
        public string? Education_Id { get; set; }

        public virtual Account? Account { get; set; }
    }
}
