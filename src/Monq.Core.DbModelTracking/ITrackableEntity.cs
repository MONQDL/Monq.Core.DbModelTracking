using Monq.Core.DbModelTracking.Models;

namespace Monq.Core.DbModelTracking
{
    /// <summary>
    /// Отслеживаемая сущность
    /// </summary>
    public interface ITrackableEntity
    {
        /// <summary>
        /// Мета-информация по сущности.
        /// </summary>
        TrackedEntity? EntityInfo { get; set; }
    }
}
