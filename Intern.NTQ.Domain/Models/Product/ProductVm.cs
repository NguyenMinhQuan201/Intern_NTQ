using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intern.NTQ.Domain.Models.Product
{
    public class ProductVm
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public string? ProductDetail { get; set; }
        public decimal? Price { get; set; }
        public bool? Trending { get; set; }
        public ICollection<ProductImageVM>? ProductImageVMs { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
