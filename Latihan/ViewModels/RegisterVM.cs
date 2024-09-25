using System.ComponentModel.DataAnnotations;

namespace Latihan.ViewModels
{
    public class RegisterVM
    {
        public string? NIK { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? BirthDate { get; set; }
        public string? Email { get; set; }
        public string? Degree { get; set; }
        public float? GPA { get; set; }
        public string? Univ_Id { get; set; }
    }

    public class LoginVM
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }

    public class ShowDataVM
    {
        public string? NIK { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //public DateTime? BirthDate { get; set; }
        public string? BirthDate { get; set; }
        public string? Email { get; set; }
        public string? Degree { get; set; }
        public float? GPA { get; set; }
        public string? Univ_Name { get; set; }
    }

    public class CountDegreeVM
    {
        public string? Degree { get; set; }
        public int? Count { get; set; }
    }
}
