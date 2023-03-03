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
        public string? PassWord { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? DeleteAt { get; set; }
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
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? DeleteAt { get; set; }
        public int ?Status { get; set; }
    }
}
