using System.ComponentModel.DataAnnotations;

namespace Latihan.Models
{
    public class Employee
    {
        [Key]
        public string? NIK { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? BirthDate { get; set; }
        public string? Email { get; set; }

        public virtual Account? Account { get; set; }
    }
}
