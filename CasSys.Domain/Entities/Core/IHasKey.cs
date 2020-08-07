namespace CasSys.Domain.Entities.Core
{
    public interface IHasKey<TKey>
    {
        TKey Id { get; set; }
    }
}