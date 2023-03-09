using Intern.NTQ.Domain.Models.Product;
using System.ComponentModel.DataAnnotations;

namespace Intern.NTQ.Manager.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public int ? ShopId { get; set; }
        public string? ProductDetail { get; set; }
        [Required]
        public decimal? Price { get; set; }
        public bool Trending { get; set; }
        public ICollection<ProductImageVM>? ProductImageVMs { get; set; }
        public int Status { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
    public class ProductCreateRequest
    {
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public string? ProductDetail { get; set; }
        [Required]
        public decimal? Price { get; set; }
        public bool Trending { get; set; }
        public int? ShopId { get; set; }
        [Required]
        public List<IFormFile> Images { get; set; }
    }
    public class ProductEditRequest
    {
        public int ? Id { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public string? ProductDetail { get; set; }
        [Required]
        public decimal? Price { get; set; }
        public bool? Trending { get; set; }
        public int? ShopId { get; set; }
        public List<IFormFile>? Images { get; set; }
    }
    public class AddImageRequest
    {
        public int Id { get; set; }
        public List<IFormFile> ProductImageVMs { get; set; }
    }
}
