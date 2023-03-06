using System.ComponentModel.DataAnnotations;

namespace Intern.NTQ.Manager.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Required]
        [RegularExpression(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$",
            ErrorMessage = "Email format not match")]
        public string? PassWord { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? DeleteAt { get; set; }
        public string ? Role { get; set; }
        public int Status { get; set; }
    }
    public class UserEditViewModel
    {
        public int?Id { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PassWord { get; set; }
        /*[DisplayFormat(DataFormatString = "{dd MMM yyyy}")]*/
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? DeleteAt { get; set; }
        public int ?Status { get; set; }
    }
    public class UserAddViewModel
    {
        [EmailAddress]
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [RegularExpression(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$", 
            ErrorMessage = "Email format not match")]
        public string? PassWord { get; set; }
        /*[DisplayFormat(DataFormatString = "{dd MMM yyyy}")]*/
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? DeleteAt { get; set; }
        public int? Status { get; set; }
    }
}
