using System.ComponentModel.DataAnnotations;

namespace DutchTreat.ViewModels
{
    public class OrderItemViewModel
    {
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        public int ProductId { get; set; }
        public string ProductCategory { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductTitle { get; set; }

    }
}