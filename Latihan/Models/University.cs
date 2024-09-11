using System.ComponentModel.DataAnnotations;

namespace Latihan.Models
{
    public class University
    {
        [Key]
        public string? Univ_Id { get; set; }
        public string? Univ_Name { get; set; }
    }
}
