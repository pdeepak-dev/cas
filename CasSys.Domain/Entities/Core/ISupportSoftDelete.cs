namespace CasSys.Domain.Entities.Core
{
    /// <summary>
    /// Implement this interface for entity which need to be soft-deleted
    /// </summary>
    public interface ISupportSoftDelete
    {
        /// <summary>
        /// Indicate if the entity has been deleted from the system
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}