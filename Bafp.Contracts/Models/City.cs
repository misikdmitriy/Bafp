namespace Bafp.Contracts.Models
{
    public class City : IIdentifiable<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
    }
}
