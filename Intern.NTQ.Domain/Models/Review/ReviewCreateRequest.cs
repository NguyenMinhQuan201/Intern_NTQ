using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intern.NTQ.Domain.Models.Review
{
    public class ReviewCreateRequest
    {
        public string Title { get; set; }
        public string Prize { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

    }
}
