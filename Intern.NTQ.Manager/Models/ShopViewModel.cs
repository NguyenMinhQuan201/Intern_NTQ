namespace Intern.NTQ.Manager.Models
{
    public class ShopViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
        public int Status { get; set; }
    }
}
