namespace Bafp.Contracts.Models
{
    public class PricingCategory : IIdentifiable<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
