namespace Bafp.Contracts.Models
{
    public class Course : IIdentifiable<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
