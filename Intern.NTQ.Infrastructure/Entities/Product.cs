using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intern.NTQ.Infrastructure.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string?Name { get; set; }
        public string?Slug { get; set; }
        public string? ProductDetail { get; set; }
        public decimal? Price { get; set; }
        public bool? Trending { get; set; }
        public ICollection<ProductImg>? ProductImgs { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
        public int ? View { get; set; }
        public int ? ShopId { get; set; }
    }
}
