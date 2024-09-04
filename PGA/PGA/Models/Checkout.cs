using System;

namespace PGA.Models
{
    public class Checkout
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PaymentInfo { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
