namespace CasSys.Domain.Entities.Core
{
    public interface ITrackable
    {
        byte[] RowVersion { get; set; }
    }
}