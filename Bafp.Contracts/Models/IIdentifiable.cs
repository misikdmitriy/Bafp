namespace Bafp.Contracts.Models
{
    public interface IIdentifiable<TId>
    {
        TId Id { get; set; }
    }
}