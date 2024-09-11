using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Latihan.Models
{
    public enum Degree
    {
        D3,D4,S1,S2,S3
    }
    public class Education
    {
        [Key]
        public string? Education_Id { get; set; }
        //public string? Degree { get; set; }
        public Degree Degree { get; set; }
        public float? GPA { get; set; }

        public virtual University University { get; set; }
        [ForeignKey(nameof(University))]
        public string? University_Id { get; set; }
    }
}
