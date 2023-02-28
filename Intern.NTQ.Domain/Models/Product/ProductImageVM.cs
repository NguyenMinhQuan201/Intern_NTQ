using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intern.NTQ.Domain.Models.Product
{
    public class ProductImageVM
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string ImagePath { get; set; } = string.Empty;

        public string Caption { get; set; } = string.Empty;

        public bool IsDefault { get; set; }

        public DateTime DateCreated { get; set; }


        public long FileSize { get; set; }
    }
}
