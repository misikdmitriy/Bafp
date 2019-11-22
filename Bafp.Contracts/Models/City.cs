namespace Bafp.Contracts.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PricingCategory Category { get; set; }
        public Course[] Courses { get; set; }
    }
}
