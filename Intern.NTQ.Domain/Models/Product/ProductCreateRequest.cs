using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intern.NTQ.Domain.Models.Product
{
    public class ProductCreateRequest
    {
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public string? ProductDetail { get; set; }
        public decimal? Price { get; set; }
        public bool? Trending { get; set; }
        public int? ShopId { get; set; }
        public List<IFormFile>?Images { get; set; }
    }
}
