using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intern.NTQ.Infrastructure.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string?Email { get; set; }
        public string?FirstName { get; set; }
        public string?LastName { get; set; }
        public string?PassWord { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
        public int Status { get; set; }
        public string ?Role { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}
