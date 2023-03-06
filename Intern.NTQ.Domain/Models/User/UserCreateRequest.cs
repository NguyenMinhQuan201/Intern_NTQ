using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intern.NTQ.Domain.Models.User
{
    public class UserCreateRequest
    {
        [EmailAddress]
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [RegularExpression(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$",
            ErrorMessage = "Email format not match")]
        public string? PassWord { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public DateTime DeleteAt { get; set; }
        public string?role { get; set; }
        public int Status { get; set; }
    }
}
