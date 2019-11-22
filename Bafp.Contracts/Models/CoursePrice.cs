namespace Bafp.Contracts.Models
{
    public class CoursePrice
    {
        public Course Course { get; set; }
        public PricingCategory PricingCategory { get; set; }
        public decimal Price { get; set; }
    }
}
